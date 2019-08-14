// This is an example of asynchronously calling the Diagnostic service's Echo method.
//
// 		Usage: go run echo_async.go 'something to echo'
//
// The environmental variable NSYS_API_KEY must be available. If need be,
// also set the environmental variable NSYS_ENDPOINT to the proper value.
// These are in hostName:portNumber format.
//
package main

import (
	"context"
	"log"
	"os"
	"strings"
	"time"

	"github.com/golang/protobuf/ptypes"
	lro "google.golang.org/genproto/googleapis/longrunning"
	"google.golang.org/grpc/status"

	"nsys.io/api"
	"nsys.io/golang/sdk"
)

func main() {
	ctx, _ := context.WithTimeout(context.Background(), time.Second*10)
	conn, err := sdk.NewConn(ctx)
	if err != nil {
		log.Fatalf("did not connect: %v", err)
	}
	oc := lro.NewOperationsClient(conn)
	dc := api.NewDiagnosticClient(conn)
	echoRequest := &api.EchoRequest{Text: strings.Join(os.Args[1:], " ")}

	// LRO means "long running operation". Methods with this suffix always return
	// a google.longrunning.Operation structure if there was no issue in initiating
	// or querying the operation's status. Errors returned at this level are just
	// like those of the non-LRO method variants except that they always reflect
	// some sort of system issue and not an application issue. Application level
	// errors and responses are encoded within the Operation structure once they
	// are marked as being Done.
	op, err := dc.EchoLRO(ctx, echoRequest)
	if err != nil {
		log.Fatalf("echo request failed: %v", err)
	}

	// Now we know that the operation has at least been successfully initiated.
	// Depending on its implementation, it may even be done already.  The normal
	// idiom here is to poll the Operation's status using GetOperation using some
	// function-specific jittered exponential backoff scheme.
	// Alternatively, one may simple call the WaitOperation method and the server
	// will do all the waiting for you. That's what we're going to do here.
	woreq := &lro.WaitOperationRequest{
		Name:    op.Name,
		Timeout: ptypes.DurationProto(time.Second * 4),
	}
	op, err = oc.WaitOperation(ctx, woreq)

	if !op.Done {
		log.Fatal("Oops! The echo operation did not complete in the time we allowed!")
	}

	// At this point there WILL either be an error status or a response.
	if err := op.GetError(); err != nil {
		// This error is a google.golang.org/genproto/googleapis/rpc/status.Status.
		// Here we convert it to the friendlier google.golang.org/grpc/status.Status
		// although one could simply output as a straight up error.Error() as well.
		// However, one generally wants to access the code within the status struct
		// do determine whether retries are appropriate or not.
		stat := status.FromProto(err)
		log.Fatalf("code = %s; message = %s", stat.Code().String(), stat.Message())
	}
	eresp := &api.EchoResponse{}
	if err := ptypes.UnmarshalAny(op.GetResponse(), eresp); err != nil {
		log.Printf("Any unmarshal error: %v", err)
	} else {
		log.Printf("Response: %s", eresp.Text)
	}
}
