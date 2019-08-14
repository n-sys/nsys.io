// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: nsys.io/api/ntypes/exemplar_ntypes.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Nsys.Api.Ntypes {

  /// <summary>Holder for reflection information generated from nsys.io/api/ntypes/exemplar_ntypes.proto</summary>
  public static partial class ExemplarNtypesReflection {

    #region Descriptor
    /// <summary>File descriptor for nsys.io/api/ntypes/exemplar_ntypes.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ExemplarNtypesReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cihuc3lzLmlvL2FwaS9udHlwZXMvZXhlbXBsYXJfbnR5cGVzLnByb3RvEg9u",
            "c3lzLmFwaS5udHlwZXMaIW5zeXMuaW8vYXBpL250eXBlcy9nZW9tZXRyeS5w",
            "cm90bxoebnN5cy5pby9hcGkvbnR5cGVzL2ZpZWxkLnByb3RvGh9nb29nbGUv",
            "cHJvdG9idWYvdGltZXN0YW1wLnByb3RvGhlnb29nbGUvcHJvdG9idWYvYW55",
            "LnByb3RvIm4KB1BkZlBhZ2USEwoLcGFnZV9udW1iZXIYASABKAUSIwoEc2l6",
            "ZRgCIAEoCzIVLm5zeXMuYXBpLm50eXBlcy5TaXplEhcKD2RlZ3JlZXNfcm90",
            "YXRlZBgDIAEoBRIQCghwbmdfZGF0YRgEIAEoDCLlAQoIRXhlbXBsYXISDAoE",
            "bmFtZRgBIAEoCRITCgtkZXNjcmlwdGlvbhgCIAEoCRIvCgtjcmVhdGVfdGlt",
            "ZRgDIAEoCzIaLmdvb2dsZS5wcm90b2J1Zi5UaW1lc3RhbXASIwoEc2l6ZRgE",
            "IAEoCzIVLm5zeXMuYXBpLm50eXBlcy5TaXplEiYKCG1ldGFkYXRhGAUgASgL",
            "MhQuZ29vZ2xlLnByb3RvYnVmLkFueRImCgZmaWVsZHMYBiADKAsyFi5uc3lz",
            "LmFwaS5udHlwZXMuRmllbGQSEAoIcG5nX2RhdGEYByABKAxCFFoSbnN5cy5p",
            "by9hcGkvbnR5cGVzYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Nsys.Api.Ntypes.GeometryReflection.Descriptor, global::Nsys.Api.Ntypes.FieldReflection.Descriptor, global::Google.Protobuf.WellKnownTypes.TimestampReflection.Descriptor, global::Google.Protobuf.WellKnownTypes.AnyReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Nsys.Api.Ntypes.PdfPage), global::Nsys.Api.Ntypes.PdfPage.Parser, new[]{ "PageNumber", "Size", "DegreesRotated", "PngData" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Nsys.Api.Ntypes.Exemplar), global::Nsys.Api.Ntypes.Exemplar.Parser, new[]{ "Name", "Description", "CreateTime", "Size", "Metadata", "Fields", "PngData" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class PdfPage : pb::IMessage<PdfPage> {
    private static readonly pb::MessageParser<PdfPage> _parser = new pb::MessageParser<PdfPage>(() => new PdfPage());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<PdfPage> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Nsys.Api.Ntypes.ExemplarNtypesReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PdfPage() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PdfPage(PdfPage other) : this() {
      pageNumber_ = other.pageNumber_;
      size_ = other.size_ != null ? other.size_.Clone() : null;
      degreesRotated_ = other.degreesRotated_;
      pngData_ = other.pngData_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PdfPage Clone() {
      return new PdfPage(this);
    }

    /// <summary>Field number for the "page_number" field.</summary>
    public const int PageNumberFieldNumber = 1;
    private int pageNumber_;
    /// <summary>
    /// The 1-based page number within the source PDF
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int PageNumber {
      get { return pageNumber_; }
      set {
        pageNumber_ = value;
      }
    }

    /// <summary>Field number for the "size" field.</summary>
    public const int SizeFieldNumber = 2;
    private global::Nsys.Api.Ntypes.Size size_;
    /// <summary>
    /// Size of the page in centimeters after it has been oriented such that the bulk 
    /// of the text appearing on it is "right side up". 
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Nsys.Api.Ntypes.Size Size {
      get { return size_; }
      set {
        size_ = value;
      }
    }

    /// <summary>Field number for the "degrees_rotated" field.</summary>
    public const int DegreesRotatedFieldNumber = 3;
    private int degreesRotated_;
    /// <summary>
    /// Pages in the original PDF may need to be rotated by 90, 180, or 270 degrees 
    /// by the system so that the bulk of the printed text is "right side up". This 
    /// value reports what clockwise rotation was applied in order to achieve this.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int DegreesRotated {
      get { return degreesRotated_; }
      set {
        degreesRotated_ = value;
      }
    }

    /// <summary>Field number for the "png_data" field.</summary>
    public const int PngDataFieldNumber = 4;
    private pb::ByteString pngData_ = pb::ByteString.Empty;
    /// <summary>
    /// The PNG image data of the rendered and oriented page. Not always present.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb::ByteString PngData {
      get { return pngData_; }
      set {
        pngData_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as PdfPage);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(PdfPage other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (PageNumber != other.PageNumber) return false;
      if (!object.Equals(Size, other.Size)) return false;
      if (DegreesRotated != other.DegreesRotated) return false;
      if (PngData != other.PngData) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (PageNumber != 0) hash ^= PageNumber.GetHashCode();
      if (size_ != null) hash ^= Size.GetHashCode();
      if (DegreesRotated != 0) hash ^= DegreesRotated.GetHashCode();
      if (PngData.Length != 0) hash ^= PngData.GetHashCode();
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
      if (PageNumber != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(PageNumber);
      }
      if (size_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(Size);
      }
      if (DegreesRotated != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(DegreesRotated);
      }
      if (PngData.Length != 0) {
        output.WriteRawTag(34);
        output.WriteBytes(PngData);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (PageNumber != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(PageNumber);
      }
      if (size_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Size);
      }
      if (DegreesRotated != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(DegreesRotated);
      }
      if (PngData.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(PngData);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(PdfPage other) {
      if (other == null) {
        return;
      }
      if (other.PageNumber != 0) {
        PageNumber = other.PageNumber;
      }
      if (other.size_ != null) {
        if (size_ == null) {
          Size = new global::Nsys.Api.Ntypes.Size();
        }
        Size.MergeFrom(other.Size);
      }
      if (other.DegreesRotated != 0) {
        DegreesRotated = other.DegreesRotated;
      }
      if (other.PngData.Length != 0) {
        PngData = other.PngData;
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
          case 8: {
            PageNumber = input.ReadInt32();
            break;
          }
          case 18: {
            if (size_ == null) {
              Size = new global::Nsys.Api.Ntypes.Size();
            }
            input.ReadMessage(Size);
            break;
          }
          case 24: {
            DegreesRotated = input.ReadInt32();
            break;
          }
          case 34: {
            PngData = input.ReadBytes();
            break;
          }
        }
      }
    }

  }

  public sealed partial class Exemplar : pb::IMessage<Exemplar> {
    private static readonly pb::MessageParser<Exemplar> _parser = new pb::MessageParser<Exemplar>(() => new Exemplar());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Exemplar> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Nsys.Api.Ntypes.ExemplarNtypesReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Exemplar() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Exemplar(Exemplar other) : this() {
      name_ = other.name_;
      description_ = other.description_;
      createTime_ = other.createTime_ != null ? other.createTime_.Clone() : null;
      size_ = other.size_ != null ? other.size_.Clone() : null;
      metadata_ = other.metadata_ != null ? other.metadata_.Clone() : null;
      fields_ = other.fields_.Clone();
      pngData_ = other.pngData_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Exemplar Clone() {
      return new Exemplar(this);
    }

    /// <summary>Field number for the "name" field.</summary>
    public const int NameFieldNumber = 1;
    private string name_ = "";
    /// <summary>
    /// The name / id of the exemplar.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "description" field.</summary>
    public const int DescriptionFieldNumber = 2;
    private string description_ = "";
    /// <summary>
    /// Client-provided optional description.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Description {
      get { return description_; }
      set {
        description_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "create_time" field.</summary>
    public const int CreateTimeFieldNumber = 3;
    private global::Google.Protobuf.WellKnownTypes.Timestamp createTime_;
    /// <summary>
    /// The time the exemplar was registered aka created.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Google.Protobuf.WellKnownTypes.Timestamp CreateTime {
      get { return createTime_; }
      set {
        createTime_ = value;
      }
    }

    /// <summary>Field number for the "size" field.</summary>
    public const int SizeFieldNumber = 4;
    private global::Nsys.Api.Ntypes.Size size_;
    /// <summary>
    /// The size of the exemplar in centimeters.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Nsys.Api.Ntypes.Size Size {
      get { return size_; }
      set {
        size_ = value;
      }
    }

    /// <summary>Field number for the "metadata" field.</summary>
    public const int MetadataFieldNumber = 5;
    private global::Google.Protobuf.WellKnownTypes.Any metadata_;
    /// <summary>
    /// The metadata supplied by the client when the exemplar was created. 
    /// Not always present.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Google.Protobuf.WellKnownTypes.Any Metadata {
      get { return metadata_; }
      set {
        metadata_ = value;
      }
    }

    /// <summary>Field number for the "fields" field.</summary>
    public const int FieldsFieldNumber = 6;
    private static readonly pb::FieldCodec<global::Nsys.Api.Ntypes.Field> _repeated_fields_codec
        = pb::FieldCodec.ForMessage(50, global::Nsys.Api.Ntypes.Field.Parser);
    private readonly pbc::RepeatedField<global::Nsys.Api.Ntypes.Field> fields_ = new pbc::RepeatedField<global::Nsys.Api.Ntypes.Field>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Nsys.Api.Ntypes.Field> Fields {
      get { return fields_; }
    }

    /// <summary>Field number for the "png_data" field.</summary>
    public const int PngDataFieldNumber = 7;
    private pb::ByteString pngData_ = pb::ByteString.Empty;
    /// <summary>
    /// The exemplar image in PNG format. Not always present.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb::ByteString PngData {
      get { return pngData_; }
      set {
        pngData_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Exemplar);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Exemplar other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Name != other.Name) return false;
      if (Description != other.Description) return false;
      if (!object.Equals(CreateTime, other.CreateTime)) return false;
      if (!object.Equals(Size, other.Size)) return false;
      if (!object.Equals(Metadata, other.Metadata)) return false;
      if(!fields_.Equals(other.fields_)) return false;
      if (PngData != other.PngData) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (Description.Length != 0) hash ^= Description.GetHashCode();
      if (createTime_ != null) hash ^= CreateTime.GetHashCode();
      if (size_ != null) hash ^= Size.GetHashCode();
      if (metadata_ != null) hash ^= Metadata.GetHashCode();
      hash ^= fields_.GetHashCode();
      if (PngData.Length != 0) hash ^= PngData.GetHashCode();
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
      if (Description.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Description);
      }
      if (createTime_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(CreateTime);
      }
      if (size_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(Size);
      }
      if (metadata_ != null) {
        output.WriteRawTag(42);
        output.WriteMessage(Metadata);
      }
      fields_.WriteTo(output, _repeated_fields_codec);
      if (PngData.Length != 0) {
        output.WriteRawTag(58);
        output.WriteBytes(PngData);
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
      if (Description.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Description);
      }
      if (createTime_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(CreateTime);
      }
      if (size_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Size);
      }
      if (metadata_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Metadata);
      }
      size += fields_.CalculateSize(_repeated_fields_codec);
      if (PngData.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(PngData);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Exemplar other) {
      if (other == null) {
        return;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      if (other.Description.Length != 0) {
        Description = other.Description;
      }
      if (other.createTime_ != null) {
        if (createTime_ == null) {
          CreateTime = new global::Google.Protobuf.WellKnownTypes.Timestamp();
        }
        CreateTime.MergeFrom(other.CreateTime);
      }
      if (other.size_ != null) {
        if (size_ == null) {
          Size = new global::Nsys.Api.Ntypes.Size();
        }
        Size.MergeFrom(other.Size);
      }
      if (other.metadata_ != null) {
        if (metadata_ == null) {
          Metadata = new global::Google.Protobuf.WellKnownTypes.Any();
        }
        Metadata.MergeFrom(other.Metadata);
      }
      fields_.Add(other.fields_);
      if (other.PngData.Length != 0) {
        PngData = other.PngData;
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
            Description = input.ReadString();
            break;
          }
          case 26: {
            if (createTime_ == null) {
              CreateTime = new global::Google.Protobuf.WellKnownTypes.Timestamp();
            }
            input.ReadMessage(CreateTime);
            break;
          }
          case 34: {
            if (size_ == null) {
              Size = new global::Nsys.Api.Ntypes.Size();
            }
            input.ReadMessage(Size);
            break;
          }
          case 42: {
            if (metadata_ == null) {
              Metadata = new global::Google.Protobuf.WellKnownTypes.Any();
            }
            input.ReadMessage(Metadata);
            break;
          }
          case 50: {
            fields_.AddEntriesFrom(input, _repeated_fields_codec);
            break;
          }
          case 58: {
            PngData = input.ReadBytes();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
