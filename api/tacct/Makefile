all:
	protoc --go_out=plugins=grpc:${GOPATH}/src -I${GOPATH}/src/nsys/proto -I. *.proto
