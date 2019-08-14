all:
	protoc --go_out=plugins=grpc:${GOPATH}/src -I. -I${GOPATH}/src/nsys/proto -I${GOPATH}/src exemplar.proto
