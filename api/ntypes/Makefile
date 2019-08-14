all:
	protoc --go_out=plugins=grpc:${GOPATH}/src -I. -I${GOPATH}/src/nsys/proto -I${GOPATH}/src image_ntypes.proto
	protoc --go_out=plugins=grpc:${GOPATH}/src -I. -I${GOPATH}/src/nsys/proto -I${GOPATH}/src exemplar_ntypes.proto
	protoc --go_out=plugins=grpc:${GOPATH}/src -I. -I${GOPATH}/src/nsys/proto -I${GOPATH}/src geometry.proto
	protoc --go_out=plugins=grpc:${GOPATH}/src -I. -I${GOPATH}/src/nsys/proto -I${GOPATH}/src words.proto
	protoc --go_out=plugins=grpc:${GOPATH}/src -I. -I${GOPATH}/src/nsys/proto -I${GOPATH}/src field.proto
	protoc --go_out=plugins=grpc:${GOPATH}/src -I. -I${GOPATH}/src/nsys/proto -I${GOPATH}/src writing_present.proto
	protoc --go_out=plugins=grpc:${GOPATH}/src -I. -I${GOPATH}/src/nsys/proto -I${GOPATH}/src sig_present.proto
	protoc --go_out=plugins=grpc:${GOPATH}/src -I. -I${GOPATH}/src/nsys/proto -I${GOPATH}/src file_ntypes.proto
