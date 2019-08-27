using System;
using System.IO;
using System.Collections.Generic;
using Grpc.Core;
using Nsys.Api.Ntypes;
using Nsys.Api.Image;
using Nsys.Api.Exemplar;

namespace ScanDemo
{
    /// <summary>
    /// A command line utility program that can manage exemplars, upload images and solve fields.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Usage displays the command line usage and exits with code 1
        /// </summary>
        static void Usage()
        {
            var usage = @"Usage:
  [command] [command args...]

  The API key must be in the NSYS_TICKET environment variable.

    Available Commands:

      listExemplars
      addExemplar    [pdf_filename] [page_num] [opt name] [opt description]
      deleteExemplar [exemplar_name]

      listFields     [exemplar_name]
      addSigField    [exemplar_name] [field_name] [x] [y] [w] [h]
      deleteField    [exemplar_name] [field_name]

      uploadAndSolve [image_filename] [optional name]
      uploadImage    [image_filename] [optional name]
      registerImage  [image_name]
      solveFields    [image_name]";

            Console.WriteLine(usage);
            Environment.Exit(1);
        }

        /// <summary>
        /// Main parses only the first command line argument and runs the given command.
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length < 1) Usage();
            if (GetApiKey() == "") Usage();

            switch (args[0])
            {
                case "listExemplars":
                    RunListExemplars(args);
                    break;
                case "addExemplar":
                    RunAddExemplar(args);
                    break;
                case "deleteExemplar":
                    RunDeleteExemplar(args);
                    break;
                case "listFields":
                    RunListFields(args);
                    break;
                case "addSigField":
                    RunAddSigField(args);
                    break;
                case "deleteField":
                    RunDeleteField(args);
                    break;
                case "uploadImage":
                    RunUploadImage(args);
                    break;
                case "registerImage":
                    RunRegisterImage(args);
                    break;
                case "solveFields":
                    RunSolveFields(args);
                    break;
                case "uploadAndSolve":
                    RunScanDemo(args);
                    break;
                default:
                    Usage();
                    break;
            }
        }

        /// <summary>
        /// GetGrpcChannel returns a grpc channel connected to the nsys server.
        /// It uses tls signed by the server side and sets the message length limits.
        /// </summary>
        static Channel GetGrpcChannel()
        {
            var host = "api.nsys.io";
            var port = 39111;
            var channelOptions = new List<ChannelOption>
            {
                new ChannelOption(ChannelOptions.SslTargetNameOverride, host),
                new ChannelOption(ChannelOptions.MaxReceiveMessageLength, 50*1024*1024),
                new ChannelOption(ChannelOptions.MaxSendMessageLength, 50*1024*1024),
            };
            return new Channel(host, port, new SslCredentials(), channelOptions);
        }

        static void RunListFields(string[] args)
        {
            if (args.Length < 2) Usage();
            var name = args[1];
            var chan = GetGrpcChannel();
            var client = new Exemplars.ExemplarsClient(chan);
            var list = client.ListFields(new ListFieldsRequest { ExemplarName = name }, ReqHead());
            foreach (var item in list.Fields) Console.WriteLine(item);
            chan.ShutdownAsync().Wait();
        }

        static void RunAddSigField(string[] args)
        {
            if (args.Length < 7) Usage();
            var exemplarName = args[1];
            var fieldName = args[2];
            var x = double.Parse(args[3]);
            var y = double.Parse(args[4]);
            var w = double.Parse(args[5]);
            var h = double.Parse(args[6]);

            var chan = GetGrpcChannel();
            var client = new Exemplars.ExemplarsClient(chan);

            var req = new CreateFieldRequest
            {
                ExemplarName = exemplarName,
                Field = new Field
                {
                    Name = fieldName,
                    SignaturePresent = new SignaturePresentPuzzle
                    {
                        Rect = new Rectangle
                        {
                            MinX = x,
                            MinY = y,
                            MaxX = x + w,
                            MaxY = y + h
                        }
                    }
                }
            };

            var resp = client.CreateField(req, ReqHead());
            Console.WriteLine("CreateFieldResponse: " + resp);

            var listReq = new ListFieldsRequest { ExemplarName = exemplarName };
            var listResp = client.ListFields(listReq, ReqHead());
            foreach (var item in listResp.Fields) Console.WriteLine(item);
            chan.ShutdownAsync().Wait();
        }

        static void RunDeleteField(string[] args)
        {
            if (args.Length < 3) Usage();
            var exemplarName = args[1];
            var fieldName = args[2];

            var chan = GetGrpcChannel();
            var client = new Exemplars.ExemplarsClient(chan);
            var req = new DeleteFieldRequest
            {
                ExemplarName = exemplarName,
                Name = fieldName
            };
            client.DeleteField(req, ReqHead());

            var listReq = new ListFieldsRequest { ExemplarName = exemplarName };
            var listResp = client.ListFields(listReq, ReqHead());
            foreach (var item in listResp.Fields) Console.WriteLine(item);
            chan.ShutdownAsync().Wait();
        }

        static void RunListExemplars(string[] args)
        {
            var chan = GetGrpcChannel();
            var client = new Exemplars.ExemplarsClient(chan);
            var list = client.ListExemplars(new ListExemplarsRequest { }, ReqHead());
            foreach (var item in list.Exemplars) Console.WriteLine(item);
            chan.ShutdownAsync().Wait();
        }

        static void RunDeleteExemplar(string[] args)
        {
            if (args.Length < 2) Usage();
            var chan = GetGrpcChannel();
            var client = new Exemplars.ExemplarsClient(chan);
            client.DeleteExemplar(new DeleteExemplarRequest { Name = args[1] }, ReqHead());

            var list = client.ListExemplars(new ListExemplarsRequest { }, ReqHead());
            foreach (var item in list.Exemplars) Console.WriteLine(item);
            chan.ShutdownAsync().Wait();
        }

        static void RunAddExemplar(string[] args)
        {
            if (args.Length < 3) Usage();
            var filename = args[1];
            var pageNum = int.Parse(args[2]);
            var name = "";
            if (args.Length > 3) name = args[3];
            var desc = "";
            if (args.Length > 4) desc = args[4];

            var chan = GetGrpcChannel();
            AddExemplar(chan, filename, pageNum, name, desc);
            chan.ShutdownAsync().Wait();
        }

        static void AddExemplar(Channel chan, string filename, int pageNum, string name, string desc)
        {
            var client = new Exemplars.ExemplarsClient(chan);
            var fileBytes = File.ReadAllBytes(filename);

            // upload the pdf
            var uploadPdfReq = new UploadPdfRequest
            {
                Data = Google.Protobuf.ByteString.CopyFrom(fileBytes),
                Language = Language.English,
                PageNumbers = { pageNum }
            };
            var uploadPdfRes = client.UploadPdf(uploadPdfReq, ReqHead());
            Console.WriteLine("Uploaded pdf:");
            Console.WriteLine("  " + uploadPdfRes);

            // create the exemplar
            var createExemplarReq = new CreateExemplarRequest
            {
                Name = name,
                ImageOrPdfName = uploadPdfRes.Name,
                PageNumber = pageNum
            };
            var createExemplarRes = client.CreateExemplar(createExemplarReq, ReqHead());
            Console.WriteLine("Created exemplar:");
            Console.WriteLine("  " + createExemplarRes);
        }

        static void RunUploadImage(string[] args)
        {
            if (args.Length < 2) Usage();
            var filename = args[1];
            var name = "";
            if (args.Length > 2) name = args[2];

            var chan = GetGrpcChannel();
            UploadImg(chan, filename, name);
            chan.ShutdownAsync().Wait();
        }

        static void RunRegisterImage(string[] args)
        {
            if (args.Length < 2) Usage();
            var name = args[1];

            var chan = GetGrpcChannel();
            RegisterImage(chan, name);
            chan.ShutdownAsync().Wait();
        }

        static void RunSolveFields(string[] args)
        {
            if (args.Length < 2) Usage();
            var name = args[1];

            var chan = GetGrpcChannel();
            SolveFields(chan, name);
            chan.ShutdownAsync().Wait();
        }

        static void RunScanDemo(string[] args)
        {
            if (args.Length < 2) Usage();
            var filename = args[1];
            var name = "";
            if (args.Length > 2) name = args[2];

            var chan = GetGrpcChannel();
            name = UploadImg(chan, filename, name);
            RegisterImage(chan, name);
            SolveFields(chan, name);
            chan.ShutdownAsync().Wait();
        }

        /// <summary>
        /// Uploads an image then downloads the original base image and deskewed image.
        /// Returns the image name.
        /// </summary>
        static string UploadImg(Channel chan, string filename, string name)
        {
            var fileBytes = File.ReadAllBytes(filename);
            var client = new ImageProcessing.ImageProcessingClient(chan);
            // setup the upload image request
            var uploadImgReq = new UploadImageRequest
            {
                Name = name,
                Image = new Image
                {
                    Data = Google.Protobuf.ByteString.CopyFrom(fileBytes),
                    Encoding = ImageEncodingFromFilename(filename)
                },
                ImageSource = ImageSource.Scanner,
            };

            // upload the image:
            var uploadImgRes = client.UploadImage(uploadImgReq, ReqHead());
            Console.WriteLine("UploadImageResponse:");
            Console.WriteLine("  " + uploadImgRes);

            // get base image
            var getBaseReq = new GetBaseImageRequest
            {
                ImageName = uploadImgRes.Name
            };
            var getBaseResp = client.GetBaseImage(getBaseReq, ReqHead());
            var baseImg = getBaseResp.BaseImage; // the base image in the response
            var baseFN = AddExt(name + "-baseImage", baseImg.Encoding);
            File.WriteAllBytes(baseFN, baseImg.Data.ToByteArray());
            Console.WriteLine("Base Image Filename:     " + baseFN);

            var getDeskewedReq = new GetDeskewedImageRequest
            {
                ImageName = uploadImgRes.Name
            };
            var getDeskewedResp = client.GetDeskewedImage(getDeskewedReq, ReqHead());
            var deskewedImg = getDeskewedResp.DeskewedImage; // the base image in the response
            var deskewedFN = AddExt(name + "-deskewedImage", deskewedImg.Image.Encoding);
            File.WriteAllBytes(deskewedFN, deskewedImg.Image.Data.ToByteArray());
            Console.WriteLine("Deskewed Image Filename: " + deskewedFN);

            return uploadImgRes.Name;
        }

        /// <summary>
        /// RegisterImage registers an image of the given name, then downloads the 
        /// registered image along with the snapshot of its exemplar.
        /// </summary>
        static void RegisterImage(Channel chan, string name)
        {
            var client = new ImageProcessing.ImageProcessingClient(chan);
            var regImgReq = new RegisterImageRequest { ImageName = name };
            var regImgRes = client.RegisterImage(regImgReq, ReqHead());
            Console.WriteLine("RegisterImageResponse:");
            Console.WriteLine("  " + regImgRes);

            var getRegImgReq = new GetRegisteredImageRequest { ImageName = name };
            var getRegImgResp = client.GetRegisteredImage(getRegImgReq, ReqHead());
            var regImg = getRegImgResp.Image.Image; // the base image in the response
            var regFN = AddExt(name + "-registeredImage", regImg.Encoding);
            File.WriteAllBytes(regFN, regImg.Data.ToByteArray());
            Console.WriteLine("Registered Image Filename:                   " + regFN);
            var getSnapshotReq = new GetExemplarSnapshotRequest
            {
                ImageName = name,
                ReturnImageData = true
            };
            var getSnapshotRes = client.GetExemplarSnapshot(getSnapshotReq, ReqHead());
            var snapFN = name + "-exemplarImage.png";
            File.WriteAllBytes(snapFN, getSnapshotRes.ExemplarSnapshot.PngData.ToByteArray());
            Console.WriteLine("Registered Image Exemplar Snapshot Filename: " + snapFN);
        }

        static void SolveFields(Channel chan, string name)
        {
            var client = new ImageProcessing.ImageProcessingClient(chan);

            var getSnapshotReq = new GetExemplarSnapshotRequest { ImageName = name };
            var getSnapshotRes = client.GetExemplarSnapshot(getSnapshotReq, ReqHead());

            foreach (var field in getSnapshotRes.ExemplarSnapshot.Fields)
            {
                var solveReq = new SolveFieldRequest
                {
                    ImageName = name,
                    FieldName = field.Name
                };

                var solveResp = client.SolveField(solveReq, ReqHead());
                Console.WriteLine(solveResp.FieldSolution);

                GetFieldCut(chan, name, field.Name);
            }
        }

        /// <summary>
        /// GetFieldCut gets a field cut for the given image and field name.
        /// It saves the field cuts as images files.
        /// </summary>
        static void GetFieldCut(Channel chan, string imageName, string fieldName)
        {
            var client = new ImageProcessing.ImageProcessingClient(chan);
            var getCutReq = new GetFieldCutRequest
            {
                ImageName = imageName,
                FieldName = fieldName
            };
            var getCutRes = client.GetFieldCut(getCutReq, ReqHead());
            Console.WriteLine("GetFieldCutResponse: ");
            Console.WriteLine("  FieldCutCase:  " + getCutRes.FieldCut.FieldCutCase);
            switch (getCutRes.FieldCut.FieldCutCase)
            {
                case FieldCut.FieldCutOneofCase.SignaturePresent:
                    // Signature present puzzles have two cuts, a "raw" cut and a "context" cut,
                    // let's save both of them:
                    var sigcut = getCutRes.FieldCut.SignaturePresent;
                    var rawCutFN = AddExt(imageName + "-" + fieldName + "-sig-raw",
                                          sigcut.RawCut.Image.Encoding);
                    var ctxCutFN = AddExt(imageName + "-" + fieldName + "-sig-ctx",
                                          sigcut.ContextCut.Image.Encoding);
                    File.WriteAllBytes(rawCutFN, sigcut.RawCut.Image.Data.ToByteArray());
                    File.WriteAllBytes(ctxCutFN, sigcut.ContextCut.Image.Data.ToByteArray());
                    Console.WriteLine("  RawCut Output: " + rawCutFN);
                    Console.WriteLine("  RawCut Offset: " + sigcut.RawCut.Offset);
                    Console.WriteLine("  CxtCut Output: " + ctxCutFN);
                    Console.WriteLine("  CtxCut Offset: " + sigcut.ContextCut.Offset);
                    break;
                case FieldCut.FieldCutOneofCase.HandwritingPresent:
                    // Handwriting Present puzzles have a single cut, we'll save it to disk
                    var handcut = getCutRes.FieldCut.HandwritingPresent;
                    rawCutFN = AddExt(imageName + "-" + fieldName + "-hndwrt-raw",
                                      handcut.RawCut.Image.Encoding);
                    File.WriteAllBytes(rawCutFN, handcut.RawCut.Image.Data.ToByteArray());
                    Console.WriteLine("  RawCut Output: " + rawCutFN);
                    break;
                case FieldCut.FieldCutOneofCase.None: // fallthrough to default case
                default:
                    Console.WriteLine("  ERROR, FieldCut not available");
                    break;
            }
        }



        /// <summary>
        /// ImageEncodingFromFilename is a simple helper function to get the image encoding from a
        /// filename extension.
        /// </summary>
        static ImageEncoding ImageEncodingFromFilename(string filename)
        {
            var ext = Path.GetExtension(filename).ToLower();
            if (ext == ".png") return ImageEncoding.Png;
            if (ext == ".jpg" || ext == ".jpeg") return ImageEncoding.Jpeg;
            if (ext == ".tif" || ext == ".tiff") return ImageEncoding.Tiff;
            return ImageEncoding.Invalid;
        }

        /// <summary>
        /// AddExt is a simple helper function that adds the appropriate file extension to a
        /// filename given the image encoding.
        /// </summary>
        static string AddExt(string filename, ImageEncoding encoding)
        {
            if (encoding == ImageEncoding.Jpeg) return filename + ".jpg";
            if (encoding == ImageEncoding.Tiff) return filename + ".tif";
            if (encoding == ImageEncoding.Png) return filename + ".png";
            return filename + ".invalid.img";
        }

        /// <summary>
        /// Returns the ApiKey from the environment
        /// </summary>
        static string GetApiKey()
        {
            return Environment.GetEnvironmentVariable("NSYS_TICKET");
        }

        /// <summary>
        /// Returns a Metadata request header with the api key
        /// </summary>
        static Metadata ReqHead()
        {
            return new Metadata { { "x-api-key", GetApiKey() } };
        }
    }
}
