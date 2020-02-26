// Package SDK provides a simple wrapper for gRPC's Dial function. It arranges
// for the application's API key to be sent with each RPC, that TLS is used,
// the setting of the appropriate connection endpoint and server name, and the
// configuration of any other parameters that are deemed necessary to work well
// with the nsys.io server complex and services.
//
package sdk

import (
	"context"
	"crypto/tls"
	"fmt"
	"log"
	"net"
	"os"
	"sync"

	"google.golang.org/grpc"
	"google.golang.org/grpc/credentials"
)

const (
	// Endpoint for host TLS connections
	EndpointDefault = "api.nsys.io:39111"
	EndpointKey     = "NSYS_ENDPOINT"

	// The TLS server we expect to find there.
	ServerNameDefault = "api.nsys.io"
	ServerNameKey     = "NSYS_SERVER_NAME"

	// API Key environmental var
	ApiKeyKey = "NSYS_API_KEY"

	// Maximum message size over gRPC
	MaxMessageSize = 200 * 1024 * 1024
)

var (
	// These may be set prior to calling NewConn.
	// Environmental variables override anything set here except for ApiKey.
	Endpoint   = EndpointDefault
	ServerName = ServerNameDefault
	ApiKey     string
)

var (
	mFetchOnce sync.Once
)

// NewInput supplies input parameters for the New() function.
type NewInput struct {
	Endpoint       string
	ServerName     string
	ApiKey         string
	MaxMessageSize int
	DialOptions    []grpc.DialOption
}

// New returns a grpc.ClientConn instance after having performed a grpc.Dial to
// the nsys.io host complex with options set such TLS is used, a connection-specific
// API key (aka ticket) is supplied with every RPC, and so on. Supplied dial options
// are added to the base set. Parameters not set in the input structure are set to
// package default values (or those zapped into the package's public globals) with
// those defaults being overridden by anything found in the environment.
func New(ctx context.Context, ni *NewInput) (*grpc.ClientConn, error) {
	mFetchOnce.Do(fetchEnvironmentalOverrides)
	if ni == nil {
		ni = &NewInput{}
	}
	apiKey := ni.ApiKey
	if apiKey == "" {
		apiKey = ApiKey
		if apiKey == "" {
			return nil, fmt.Errorf("no API key (aka ticket) set")
		}
	}
	endPoint := ni.Endpoint
	if endPoint == "" {
		endPoint = Endpoint
		if endPoint == "" {
			return nil, fmt.Errorf("no network endpoint set")
		}
	}
	serverName := ni.ServerName
	if serverName == "" {
		serverName = ServerName
		if serverName == "" {
			return nil, fmt.Errorf("no server name set")
		}
	}
	maxMessageSize := ni.MaxMessageSize
	if maxMessageSize <= 0 {
		maxMessageSize = MaxMessageSize
	}
	creds := credentials.NewTLS(
		&tls.Config{
			ServerName: serverName,
		},
	)
	dopts := []grpc.DialOption{
		grpc.WithDefaultCallOptions(
			grpc.MaxCallRecvMsgSize(maxMessageSize),
			grpc.MaxCallSendMsgSize(maxMessageSize),
		),
		grpc.WithTransportCredentials(creds),
		grpc.WithPerRPCCredentials(tokenAuth{token: apiKey}),
	}
	return grpc.DialContext(ctx, endPoint, append(dopts, ni.DialOptions...)...)
}

// NewConn is a legacy function which is now a simple wrapper on New().
// It is only appropriate for use when the application is using a single API key.
func NewConn(ctx context.Context, xopts ...grpc.DialOption) (*grpc.ClientConn, error) {
	return New(ctx, &NewInput{DialOptions: xopts})
}

// Per RPC credentials we use to get the API key into the headers.
type tokenAuth struct {
	token string
}

func (t tokenAuth) GetRequestMetadata(
	ctx context.Context, in ...string) (map[string]string, error) {
	return map[string]string{"x-api-key": t.token}, nil
}

func (tokenAuth) RequireTransportSecurity() bool {
	return true
}

func fetchEnvironmentalOverrides() {
	// Replace default or preset values for endpoint, server, and api key with
	// any such values found in the environment.
	s, ok := os.LookupEnv(EndpointKey)
	if ok {
		_, _, err := net.SplitHostPort(s)
		if err != nil {
			log.Fatalf("FATAL: Invalid endpoint in %s; need host:port", EndpointKey)
		}
		Endpoint = s
	}
	s, ok = os.LookupEnv(ServerNameKey)
	if ok && len(s) > 1 {
		ServerName = s
	}
	if ApiKey == "" {
		ApiKey, _ = os.LookupEnv(ApiKeyKey)
	}
}
