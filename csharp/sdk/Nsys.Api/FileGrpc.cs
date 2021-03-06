// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: nsys.io/api/file/file.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Nsys.Api.File {
  public static partial class FileProcessing
  {
    static readonly string __ServiceName = "nsys.api.file.FileProcessing";

    static readonly grpc::Marshaller<global::Nsys.Api.File.UploadGenericFileRequest> __Marshaller_nsys_api_file_UploadGenericFileRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Nsys.Api.File.UploadGenericFileRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Nsys.Api.File.UploadGenericFileResponse> __Marshaller_nsys_api_file_UploadGenericFileResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Nsys.Api.File.UploadGenericFileResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Google.LongRunning.Operation> __Marshaller_google_longrunning_Operation = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Google.LongRunning.Operation.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Nsys.Api.File.CreateFilesetRequest> __Marshaller_nsys_api_file_CreateFilesetRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Nsys.Api.File.CreateFilesetRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Nsys.Api.File.CreateFilesetResponse> __Marshaller_nsys_api_file_CreateFilesetResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Nsys.Api.File.CreateFilesetResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Nsys.Api.File.InitiateProcessingRequest> __Marshaller_nsys_api_file_InitiateProcessingRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Nsys.Api.File.InitiateProcessingRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Google.Protobuf.WellKnownTypes.Empty> __Marshaller_google_protobuf_Empty = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Google.Protobuf.WellKnownTypes.Empty.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Nsys.Api.File.ProcessEmailRequest> __Marshaller_nsys_api_file_ProcessEmailRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Nsys.Api.File.ProcessEmailRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Nsys.Api.File.ProcessEmailResponse> __Marshaller_nsys_api_file_ProcessEmailResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Nsys.Api.File.ProcessEmailResponse.Parser.ParseFrom);

    static readonly grpc::Method<global::Nsys.Api.File.UploadGenericFileRequest, global::Nsys.Api.File.UploadGenericFileResponse> __Method_UploadGenericFile = new grpc::Method<global::Nsys.Api.File.UploadGenericFileRequest, global::Nsys.Api.File.UploadGenericFileResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "UploadGenericFile",
        __Marshaller_nsys_api_file_UploadGenericFileRequest,
        __Marshaller_nsys_api_file_UploadGenericFileResponse);

    static readonly grpc::Method<global::Nsys.Api.File.UploadGenericFileRequest, global::Google.LongRunning.Operation> __Method_UploadGenericFileLRO = new grpc::Method<global::Nsys.Api.File.UploadGenericFileRequest, global::Google.LongRunning.Operation>(
        grpc::MethodType.Unary,
        __ServiceName,
        "UploadGenericFileLRO",
        __Marshaller_nsys_api_file_UploadGenericFileRequest,
        __Marshaller_google_longrunning_Operation);

    static readonly grpc::Method<global::Nsys.Api.File.CreateFilesetRequest, global::Nsys.Api.File.CreateFilesetResponse> __Method_CreateFileset = new grpc::Method<global::Nsys.Api.File.CreateFilesetRequest, global::Nsys.Api.File.CreateFilesetResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "CreateFileset",
        __Marshaller_nsys_api_file_CreateFilesetRequest,
        __Marshaller_nsys_api_file_CreateFilesetResponse);

    static readonly grpc::Method<global::Nsys.Api.File.CreateFilesetRequest, global::Google.LongRunning.Operation> __Method_CreateFilesetLRO = new grpc::Method<global::Nsys.Api.File.CreateFilesetRequest, global::Google.LongRunning.Operation>(
        grpc::MethodType.Unary,
        __ServiceName,
        "CreateFilesetLRO",
        __Marshaller_nsys_api_file_CreateFilesetRequest,
        __Marshaller_google_longrunning_Operation);

    static readonly grpc::Method<global::Nsys.Api.File.InitiateProcessingRequest, global::Google.Protobuf.WellKnownTypes.Empty> __Method_InitiateProcessing = new grpc::Method<global::Nsys.Api.File.InitiateProcessingRequest, global::Google.Protobuf.WellKnownTypes.Empty>(
        grpc::MethodType.Unary,
        __ServiceName,
        "InitiateProcessing",
        __Marshaller_nsys_api_file_InitiateProcessingRequest,
        __Marshaller_google_protobuf_Empty);

    static readonly grpc::Method<global::Nsys.Api.File.InitiateProcessingRequest, global::Google.LongRunning.Operation> __Method_InitiateProcessingLRO = new grpc::Method<global::Nsys.Api.File.InitiateProcessingRequest, global::Google.LongRunning.Operation>(
        grpc::MethodType.Unary,
        __ServiceName,
        "InitiateProcessingLRO",
        __Marshaller_nsys_api_file_InitiateProcessingRequest,
        __Marshaller_google_longrunning_Operation);

    static readonly grpc::Method<global::Nsys.Api.File.ProcessEmailRequest, global::Nsys.Api.File.ProcessEmailResponse> __Method_ProcessEmail = new grpc::Method<global::Nsys.Api.File.ProcessEmailRequest, global::Nsys.Api.File.ProcessEmailResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "ProcessEmail",
        __Marshaller_nsys_api_file_ProcessEmailRequest,
        __Marshaller_nsys_api_file_ProcessEmailResponse);

    static readonly grpc::Method<global::Nsys.Api.File.ProcessEmailRequest, global::Google.LongRunning.Operation> __Method_ProcessEmailLRO = new grpc::Method<global::Nsys.Api.File.ProcessEmailRequest, global::Google.LongRunning.Operation>(
        grpc::MethodType.Unary,
        __ServiceName,
        "ProcessEmailLRO",
        __Marshaller_nsys_api_file_ProcessEmailRequest,
        __Marshaller_google_longrunning_Operation);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Nsys.Api.File.FileReflection.Descriptor.Services[0]; }
    }

    /// <summary>Client for FileProcessing</summary>
    public partial class FileProcessingClient : grpc::ClientBase<FileProcessingClient>
    {
      /// <summary>Creates a new client for FileProcessing</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public FileProcessingClient(grpc::Channel channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for FileProcessing that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public FileProcessingClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected FileProcessingClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected FileProcessingClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      /// <summary>
      /// Upload a generic file for further processing later.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::Nsys.Api.File.UploadGenericFileResponse UploadGenericFile(global::Nsys.Api.File.UploadGenericFileRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return UploadGenericFile(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Upload a generic file for further processing later.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::Nsys.Api.File.UploadGenericFileResponse UploadGenericFile(global::Nsys.Api.File.UploadGenericFileRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_UploadGenericFile, null, options, request);
      }
      /// <summary>
      /// Upload a generic file for further processing later.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::Nsys.Api.File.UploadGenericFileResponse> UploadGenericFileAsync(global::Nsys.Api.File.UploadGenericFileRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return UploadGenericFileAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Upload a generic file for further processing later.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::Nsys.Api.File.UploadGenericFileResponse> UploadGenericFileAsync(global::Nsys.Api.File.UploadGenericFileRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_UploadGenericFile, null, options, request);
      }
      public virtual global::Google.LongRunning.Operation UploadGenericFileLRO(global::Nsys.Api.File.UploadGenericFileRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return UploadGenericFileLRO(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Google.LongRunning.Operation UploadGenericFileLRO(global::Nsys.Api.File.UploadGenericFileRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_UploadGenericFileLRO, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Google.LongRunning.Operation> UploadGenericFileLROAsync(global::Nsys.Api.File.UploadGenericFileRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return UploadGenericFileLROAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Google.LongRunning.Operation> UploadGenericFileLROAsync(global::Nsys.Api.File.UploadGenericFileRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_UploadGenericFileLRO, null, options, request);
      }
      /// <summary>
      /// Group any number of previously-uploaded files together into a 
      /// single fileset. Files may have membership in multiple filesets.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::Nsys.Api.File.CreateFilesetResponse CreateFileset(global::Nsys.Api.File.CreateFilesetRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CreateFileset(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Group any number of previously-uploaded files together into a 
      /// single fileset. Files may have membership in multiple filesets.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::Nsys.Api.File.CreateFilesetResponse CreateFileset(global::Nsys.Api.File.CreateFilesetRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_CreateFileset, null, options, request);
      }
      /// <summary>
      /// Group any number of previously-uploaded files together into a 
      /// single fileset. Files may have membership in multiple filesets.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::Nsys.Api.File.CreateFilesetResponse> CreateFilesetAsync(global::Nsys.Api.File.CreateFilesetRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CreateFilesetAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Group any number of previously-uploaded files together into a 
      /// single fileset. Files may have membership in multiple filesets.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::Nsys.Api.File.CreateFilesetResponse> CreateFilesetAsync(global::Nsys.Api.File.CreateFilesetRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_CreateFileset, null, options, request);
      }
      public virtual global::Google.LongRunning.Operation CreateFilesetLRO(global::Nsys.Api.File.CreateFilesetRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CreateFilesetLRO(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Google.LongRunning.Operation CreateFilesetLRO(global::Nsys.Api.File.CreateFilesetRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_CreateFilesetLRO, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Google.LongRunning.Operation> CreateFilesetLROAsync(global::Nsys.Api.File.CreateFilesetRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CreateFilesetLROAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Google.LongRunning.Operation> CreateFilesetLROAsync(global::Nsys.Api.File.CreateFilesetRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_CreateFilesetLRO, null, options, request);
      }
      /// <summary>
      /// Begin processing the fileset in the specified way.
      /// This is a temporary hack (therefore this will be here forever ;-)
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::Google.Protobuf.WellKnownTypes.Empty InitiateProcessing(global::Nsys.Api.File.InitiateProcessingRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return InitiateProcessing(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Begin processing the fileset in the specified way.
      /// This is a temporary hack (therefore this will be here forever ;-)
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::Google.Protobuf.WellKnownTypes.Empty InitiateProcessing(global::Nsys.Api.File.InitiateProcessingRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_InitiateProcessing, null, options, request);
      }
      /// <summary>
      /// Begin processing the fileset in the specified way.
      /// This is a temporary hack (therefore this will be here forever ;-)
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::Google.Protobuf.WellKnownTypes.Empty> InitiateProcessingAsync(global::Nsys.Api.File.InitiateProcessingRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return InitiateProcessingAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Begin processing the fileset in the specified way.
      /// This is a temporary hack (therefore this will be here forever ;-)
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::Google.Protobuf.WellKnownTypes.Empty> InitiateProcessingAsync(global::Nsys.Api.File.InitiateProcessingRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_InitiateProcessing, null, options, request);
      }
      public virtual global::Google.LongRunning.Operation InitiateProcessingLRO(global::Nsys.Api.File.InitiateProcessingRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return InitiateProcessingLRO(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Google.LongRunning.Operation InitiateProcessingLRO(global::Nsys.Api.File.InitiateProcessingRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_InitiateProcessingLRO, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Google.LongRunning.Operation> InitiateProcessingLROAsync(global::Nsys.Api.File.InitiateProcessingRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return InitiateProcessingLROAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Google.LongRunning.Operation> InitiateProcessingLROAsync(global::Nsys.Api.File.InitiateProcessingRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_InitiateProcessingLRO, null, options, request);
      }
      /// <summary>
      /// Parses an email *.eml file stored as a generic file. The rendered html
      /// and other attachments are stored as new generic files tied to the source.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::Nsys.Api.File.ProcessEmailResponse ProcessEmail(global::Nsys.Api.File.ProcessEmailRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ProcessEmail(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Parses an email *.eml file stored as a generic file. The rendered html
      /// and other attachments are stored as new generic files tied to the source.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::Nsys.Api.File.ProcessEmailResponse ProcessEmail(global::Nsys.Api.File.ProcessEmailRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_ProcessEmail, null, options, request);
      }
      /// <summary>
      /// Parses an email *.eml file stored as a generic file. The rendered html
      /// and other attachments are stored as new generic files tied to the source.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::Nsys.Api.File.ProcessEmailResponse> ProcessEmailAsync(global::Nsys.Api.File.ProcessEmailRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ProcessEmailAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Parses an email *.eml file stored as a generic file. The rendered html
      /// and other attachments are stored as new generic files tied to the source.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::Nsys.Api.File.ProcessEmailResponse> ProcessEmailAsync(global::Nsys.Api.File.ProcessEmailRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_ProcessEmail, null, options, request);
      }
      public virtual global::Google.LongRunning.Operation ProcessEmailLRO(global::Nsys.Api.File.ProcessEmailRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ProcessEmailLRO(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Google.LongRunning.Operation ProcessEmailLRO(global::Nsys.Api.File.ProcessEmailRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_ProcessEmailLRO, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Google.LongRunning.Operation> ProcessEmailLROAsync(global::Nsys.Api.File.ProcessEmailRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ProcessEmailLROAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Google.LongRunning.Operation> ProcessEmailLROAsync(global::Nsys.Api.File.ProcessEmailRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_ProcessEmailLRO, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override FileProcessingClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new FileProcessingClient(configuration);
      }
    }

  }
}
#endregion
