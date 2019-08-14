// This is an example of issuing synchronous echo requests round robin over a bank
// of connections.
//
// 		Usage: go run echo_parallel.go
//
// The environmental variable NSYS_API_KEY must be available. If need be,
// also set the environmental variable NSYS_ENDPOINT to the proper value.
// These are in hostName:portNumber format.
//
package main

import (
	"context"
	"log"
	"strconv"
	"time"

	"google.golang.org/grpc"
	"google.golang.org/grpc/connectivity"

	"nsys.io/api"
	"nsys.io/golang/sdk"
)

func main() {
	var err error
	// Allowing 100 seconds to get everything done.
	ctx, _ := context.WithTimeout(context.Background(), time.Second*100)

	// We are going to operate with 10 parallel connections.
	conn := make([]*grpc.ClientConn, 10)
	dc := make([]api.DiagnosticClient, cap(conn))

	for i := 0; i < cap(conn); i++ {
		// Populate our connection structs and matching diagnostic clients.
		// Note that this only initiates the formation of physical connections
		// since we have not provided NewConn() with a dial option that would
		// have things be otherwise.
		conn[i], err = sdk.NewConn(ctx)
		if err != nil {
			log.Fatal(err)
		}
		dc[i] = api.NewDiagnosticClient(conn[i])
	}

	log.Printf("Waiting for all connections to become ready.")
	// We are only doing this waiting for the sake of timing stats below.
	// One would not normally do this.
	for {
		n := 0
		for i := 0; i < cap(conn); i++ {
			if conn[i].GetState() == connectivity.Ready {
				n++
			}
		}
		if n == cap(conn) {
			break
		}
		time.Sleep(time.Millisecond * 250)
	}

	// We are going to send/receive 1000 echo requests and responses.
	req_C := make(chan *api.EchoRequest, 1000)
	type rr struct {
		req  string
		resp string
	}
	resp_C := make(chan *rr, cap(req_C))

	echoWorker := func(c api.DiagnosticClient, n int) {
		tkt_C := make(chan bool, n)
		for i := 0; i < n; i++ {
			tkt_C <- true
		}
		for {
			<-tkt_C
			req := <-req_C
			go func(req *api.EchoRequest) {
				echoResponse, err := c.Echo(ctx, req, grpc.WaitForReady(true))
				if err != nil {
					log.Fatal(err)
				}
				resp_C <- &rr{req: req.Text, resp: echoResponse.Text}
				tkt_C <- true
			}(req)
		}
	}
	for i := 0; i < cap(dc); i++ {
		// We are going to allow each worker to use 10 processing goroutines.
		go echoWorker(dc[i], 10)
	}
	log.Printf("Beginning test...")
	st := time.Now()
	for i := 0; i < cap(req_C); i++ {
		req_C <- &api.EchoRequest{Text: strconv.Itoa(i)}
	}
	for i := 0; i < cap(req_C); i++ {
		resp := <-resp_C
		if resp.req != resp.resp {
			log.Fatalf("echo request / response mismatch: %v", resp)
		}
	}
	tt := time.Now().Sub(st)
	log.Printf("%d echos in %v", cap(req_C), tt)
}
