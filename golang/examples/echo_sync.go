// This is an example of synchronously calling the Diagnostic service's Echo
// method but doing so in parallel. It also shows some connection state tracking.
//
// 		Usage: go run echo_sync.go 'something to echo'
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
	"sync"
	"time"

	"google.golang.org/grpc"
	"google.golang.org/grpc/codes"
	"google.golang.org/grpc/status"

	"nsys.io/api"
	"nsys.io/golang/sdk"
)

func main() {
	ctx, _ := context.WithTimeout(context.Background(), time.Second*30)
	conn, err := sdk.NewConn(ctx)
	if err != nil {
		log.Fatal(err)
	}

	go func() {
		// A demonstration of connection state tracking.
		for {
			state := conn.GetState()
			log.Printf("connection state = %v\n", state)
			conn.WaitForStateChange(context.Background(), state)
		}
	}()

	dc := api.NewDiagnosticClient(conn)
	echoRequest := &api.EchoRequest{Text: strings.Join(os.Args[1:], " ")}

	reps := 10
	var wg sync.WaitGroup
	wg.Add(reps)

	echoFunc := func(n int) {
		echoResponse, err := dc.Echo(ctx, echoRequest, grpc.WaitForReady(true))
		if err != nil {
			// Every error returned by these services can be converted into
			// a status.Status and then have its code interrogated and acted
			// upon. This is key information for those who are trying to
			// determine if a request should be retried.
			stat := status.Convert(err)
			if stat.Code() == codes.Unavailable {
				log.Fatal("Diagnostic service is unavailable")
			}
			log.Fatal(err)
		}
		if echoResponse.Text != echoRequest.Text {
			log.Fatalf("Sent '%s' but recvd '%s'", echoRequest.Text, echoResponse.Text)
		}
		wg.Done()
	}
	for i := 0; i < reps; i++ {
		go echoFunc(i)
	}
	wg.Wait()
	log.Printf("All %d echo goroutines have completed.", reps)
	log.Printf("Closing connection and waiting a couple seconds..")
	conn.Close()
	time.Sleep(time.Second * 2)
	log.Printf("Exiting...")
}
