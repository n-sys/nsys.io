// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: nsys.io/api/file/file.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Nsys.Api.Image {

  /// <summary>Holder for reflection information generated from nsys.io/api/file/file.proto</summary>
  public static partial class FileReflection {

    #region Descriptor
    /// <summary>File descriptor for nsys.io/api/file/file.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static FileReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Chtuc3lzLmlvL2FwaS9maWxlL2ZpbGUucHJvdG8SDm5zeXMuYXBpLmltYWdl",
            "GiNnb29nbGUvbG9uZ3J1bm5pbmcvb3BlcmF0aW9ucy5wcm90bxofZ29vZ2xl",
            "L3Byb3RvYnVmL3RpbWVzdGFtcC5wcm90bxokbnN5cy5pby9hcGkvbnR5cGVz",
            "L2ZpbGVfbnR5cGVzLnByb3RvIn8KGFVwbG9hZEdlbmVyaWNGaWxlUmVxdWVz",
            "dBIyCgxnZW5lcmljX2ZpbGUYASABKAsyHC5uc3lzLmFwaS5udHlwZXMuR2Vu",
            "ZXJpY0ZpbGUSLwoLZXhwaXJlX3RpbWUYAiABKAsyGi5nb29nbGUucHJvdG9i",
            "dWYuVGltZXN0YW1wIloKGVVwbG9hZEdlbmVyaWNGaWxlUmVzcG9uc2USDAoE",
            "TmFtZRgBIAEoCRIvCgtleHBpcmVfdGltZRgCIAEoCzIaLmdvb2dsZS5wcm90",
            "b2J1Zi5UaW1lc3RhbXAy3wEKDkZpbGVQcm9jZXNzaW5nEmoKEVVwbG9hZEdl",
            "bmVyaWNGaWxlEigubnN5cy5hcGkuaW1hZ2UuVXBsb2FkR2VuZXJpY0ZpbGVS",
            "ZXF1ZXN0GikubnN5cy5hcGkuaW1hZ2UuVXBsb2FkR2VuZXJpY0ZpbGVSZXNw",
            "b25zZSIAEmEKFFVwbG9hZEdlbmVyaWNGaWxlTFJPEigubnN5cy5hcGkuaW1h",
            "Z2UuVXBsb2FkR2VuZXJpY0ZpbGVSZXF1ZXN0Gh0uZ29vZ2xlLmxvbmdydW5u",
            "aW5nLk9wZXJhdGlvbiIAQhJaEG5zeXMuaW8vYXBpL2ZpbGViBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Google.LongRunning.OperationsReflection.Descriptor, global::Google.Protobuf.WellKnownTypes.TimestampReflection.Descriptor, global::Nsys.Api.Ntypes.FileNtypesReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Nsys.Api.Image.UploadGenericFileRequest), global::Nsys.Api.Image.UploadGenericFileRequest.Parser, new[]{ "GenericFile", "ExpireTime" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Nsys.Api.Image.UploadGenericFileResponse), global::Nsys.Api.Image.UploadGenericFileResponse.Parser, new[]{ "Name", "ExpireTime" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class UploadGenericFileRequest : pb::IMessage<UploadGenericFileRequest> {
    private static readonly pb::MessageParser<UploadGenericFileRequest> _parser = new pb::MessageParser<UploadGenericFileRequest>(() => new UploadGenericFileRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<UploadGenericFileRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Nsys.Api.Image.FileReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UploadGenericFileRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UploadGenericFileRequest(UploadGenericFileRequest other) : this() {
      genericFile_ = other.genericFile_ != null ? other.genericFile_.Clone() : null;
      expireTime_ = other.expireTime_ != null ? other.expireTime_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UploadGenericFileRequest Clone() {
      return new UploadGenericFileRequest(this);
    }

    /// <summary>Field number for the "generic_file" field.</summary>
    public const int GenericFileFieldNumber = 1;
    private global::Nsys.Api.Ntypes.GenericFile genericFile_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Nsys.Api.Ntypes.GenericFile GenericFile {
      get { return genericFile_; }
      set {
        genericFile_ = value;
      }
    }

    /// <summary>Field number for the "expire_time" field.</summary>
    public const int ExpireTimeFieldNumber = 2;
    private global::Google.Protobuf.WellKnownTypes.Timestamp expireTime_;
    /// <summary>
    /// Requested file expiration time. The time is capped to comply with 
    /// account-specific minimums and maximums. A zero value requests the 
    /// maximum allowable expiration time be used.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Google.Protobuf.WellKnownTypes.Timestamp ExpireTime {
      get { return expireTime_; }
      set {
        expireTime_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as UploadGenericFileRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(UploadGenericFileRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(GenericFile, other.GenericFile)) return false;
      if (!object.Equals(ExpireTime, other.ExpireTime)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (genericFile_ != null) hash ^= GenericFile.GetHashCode();
      if (expireTime_ != null) hash ^= ExpireTime.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (genericFile_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(GenericFile);
      }
      if (expireTime_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(ExpireTime);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (genericFile_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(GenericFile);
      }
      if (expireTime_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(ExpireTime);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(UploadGenericFileRequest other) {
      if (other == null) {
        return;
      }
      if (other.genericFile_ != null) {
        if (genericFile_ == null) {
          GenericFile = new global::Nsys.Api.Ntypes.GenericFile();
        }
        GenericFile.MergeFrom(other.GenericFile);
      }
      if (other.expireTime_ != null) {
        if (expireTime_ == null) {
          ExpireTime = new global::Google.Protobuf.WellKnownTypes.Timestamp();
        }
        ExpireTime.MergeFrom(other.ExpireTime);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            if (genericFile_ == null) {
              GenericFile = new global::Nsys.Api.Ntypes.GenericFile();
            }
            input.ReadMessage(GenericFile);
            break;
          }
          case 18: {
            if (expireTime_ == null) {
              ExpireTime = new global::Google.Protobuf.WellKnownTypes.Timestamp();
            }
            input.ReadMessage(ExpireTime);
            break;
          }
        }
      }
    }

  }

  public sealed partial class UploadGenericFileResponse : pb::IMessage<UploadGenericFileResponse> {
    private static readonly pb::MessageParser<UploadGenericFileResponse> _parser = new pb::MessageParser<UploadGenericFileResponse>(() => new UploadGenericFileResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<UploadGenericFileResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Nsys.Api.Image.FileReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UploadGenericFileResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UploadGenericFileResponse(UploadGenericFileResponse other) : this() {
      name_ = other.name_;
      expireTime_ = other.expireTime_ != null ? other.expireTime_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UploadGenericFileResponse Clone() {
      return new UploadGenericFileResponse(this);
    }

    /// <summary>Field number for the "Name" field.</summary>
    public const int NameFieldNumber = 1;
    private string name_ = "";
    /// <summary>
    /// Identifier for the uploaded file.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "expire_time" field.</summary>
    public const int ExpireTimeFieldNumber = 2;
    private global::Google.Protobuf.WellKnownTypes.Timestamp expireTime_;
    /// <summary>
    /// The actual expiration time for the file.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Google.Protobuf.WellKnownTypes.Timestamp ExpireTime {
      get { return expireTime_; }
      set {
        expireTime_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as UploadGenericFileResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(UploadGenericFileResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Name != other.Name) return false;
      if (!object.Equals(ExpireTime, other.ExpireTime)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (expireTime_ != null) hash ^= ExpireTime.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Name.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Name);
      }
      if (expireTime_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(ExpireTime);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (expireTime_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(ExpireTime);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(UploadGenericFileResponse other) {
      if (other == null) {
        return;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      if (other.expireTime_ != null) {
        if (expireTime_ == null) {
          ExpireTime = new global::Google.Protobuf.WellKnownTypes.Timestamp();
        }
        ExpireTime.MergeFrom(other.ExpireTime);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Name = input.ReadString();
            break;
          }
          case 18: {
            if (expireTime_ == null) {
              ExpireTime = new global::Google.Protobuf.WellKnownTypes.Timestamp();
            }
            input.ReadMessage(ExpireTime);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
