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
	MaxMessageSize = 50 * 1024 * 1024
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
		ApiKey, ok = os.LookupEnv(ApiKeyKey)
		if !ok {
			log.Fatalf("FATAL: No value for %s found in the environment", ApiKeyKey)
		}
	}
}

// NewConn performs a grpc.Dial operation to the nsys.io host complex with appropriate
// options set such that TLS is used and the nsys.io API key is supplied with every
// RPC. Any needed additional configuration for proper operation with our service
// will also be added here as need determines.
//
// Notes: Any supplied dial options are added to the set applied by default.
// The endpoint dialed and TLS server expected there can be overridden with
// environmental variables.
//
func NewConn(ctx context.Context, xopts ...grpc.DialOption) (*grpc.ClientConn, error) {
	mFetchOnce.Do(fetchEnvironmentalOverrides)
	if ApiKey == "" {
		return nil, fmt.Errorf("missing API key; set %s", ApiKeyKey)
	}
	creds := credentials.NewTLS(
		&tls.Config{
			ServerName: ServerName,
		},
	)
	dopts := []grpc.DialOption{
		grpc.WithDefaultCallOptions(
			grpc.MaxCallRecvMsgSize(MaxMessageSize),
			grpc.MaxCallSendMsgSize(MaxMessageSize),
		),
		grpc.WithTransportCredentials(creds),
		grpc.WithPerRPCCredentials(tokenAuth{token: ApiKey}),
	}
	return grpc.DialContext(ctx, Endpoint, append(dopts, xopts...)...)
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
