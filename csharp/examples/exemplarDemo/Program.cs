using System;
using System.IO;
using System.Collections.Generic;
using Grpc.Core;
using Nsys.Api.Ntypes;
using Nsys.Api.Exemplar;

namespace exemplarDemo
{
    /// <summary>
    /// A program which demonstrates the following calls to the Exemplar service:
    ///   UploadPdf
    ///   GetPdfPage
    ///   UploadDimensionedImage
    ///   CreateExemplar
    ///   GetExemplar
    ///   ListExemplars
    ///   DeleteExemplar
    ///   CreateField
    ///   ListFields
    ///   DeleteField
    ///
    /// It uploads a pdf or an image to the service, creates an exemplar with a field in
    /// it, then deletes the field and the exemplar, all while listing all the exemplars
    /// and fields, before and after creation and deletion.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3) 
            {
                Console.WriteLine("required arguments: " +
                    "[apikey] [pdf/img file] [pagenum] " +
                    "[opt description] [opt exemplar name] [opt pdf/img name]");
                return;
            }
            var apiKey = args[0];
            var filename = args[1];
            var pageNum = int.Parse(args[2]);
            var exemDescription = "";
            if (args.Length > 3) exemDescription = args[3];
            var exemName = "";
            if (args.Length > 4) exemName = args[4];
            var pdfImgName = "";
            if (args.Length > 5) pdfImgName = args[5];
                        
            // read in the input pdf or image
            var fileBytes = File.ReadAllBytes(filename);

            // get a grpc channel connected to the nsys server
            var channel = GetGrpcChannel();

            // create our ExemplarsClient which can make calls to the Exemplars service
            var client = new Exemplars.ExemplarsClient(channel);

            // all requests need the api key in the request headers like this:
            var reqHead = new Metadata {{ "x-api-key", apiKey }};

            // before we create an exemplar, we first need to upload a pdf, or a 
            // dimensioned image, depending on the file extension:
            var ext = Path.GetExtension(filename);
            if (ext == ".pdf")
            {
                Console.WriteLine("Uploading Pdf:");
                pdfImgName = UploadPdf(client, reqHead, fileBytes, pageNum, pdfImgName);
            }
            else
            {
                Console.WriteLine("Uploading Dimensioned Image:");
                pdfImgName = UploadDimImage(client, reqHead, fileBytes, pdfImgName, ext);
            }

            // now that the pdf or the dim-image is uploaded we can create the exemplar
            Console.WriteLine("Creating Exemplar:");
            exemName = CreateExemplar(client, reqHead, pdfImgName, pageNum,
                exemDescription, exemName);

            Console.WriteLine("List all exemplars, should see "+exemName+":");
            ListExemplars(client, reqHead);

            Console.WriteLine("Get new exemplar back:");
            GetExemplar(client, reqHead, exemName);

            Console.WriteLine("Creating field on new exemplar:");
            var fieldName = CreateField(client, reqHead, exemName, "", "test field");

            Console.WriteLine("Listing fields on new exemplar, should see "+fieldName);
            ListFields(client, reqHead, exemName);

            Console.WriteLine("Deleting field "+fieldName+" on exemplar "+exemName);
            var delFReq = new DeleteFieldRequest{ExemplarName=exemName, Name=fieldName};
            client.DeleteField(delFReq, reqHead);

            Console.WriteLine("List fields on new exemplar after delete:");
            ListFields(client, reqHead, exemName);

            Console.WriteLine("Deleting new exemplar " + exemName);
            var delEReq = new DeleteExemplarRequest{Name=exemName};
            client.DeleteExemplar(delEReq, reqHead);

            Console.WriteLine("List all exemplars after delete:");
            ListExemplars(client, reqHead);

            channel.ShutdownAsync().Wait();
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

        /// <summary>
        /// UploadPdf uploads a pdf to the system, then gets it back (just for kicks).
        /// </summary>
        static string UploadPdf(Exemplars.ExemplarsClient client, Metadata reqHead,
            byte[] fileBytes, int pageNum, string pdfName)
        {
            // Upload the pdf
            var upRequest = new UploadPdfRequest
            {
                Name = pdfName,
                Data = Google.Protobuf.ByteString.CopyFrom(fileBytes)
            };
            upRequest.PageNumbers.Add(pageNum); // Only process page we care about,
                                                // if this were commented out it would
                                                // process all pages.

            var upReply = client.UploadPdf(upRequest, reqHead);

            Console.WriteLine("  UploadPdf Reply:");
            Console.WriteLine("    Name:    "+upReply.Name);
            Console.WriteLine("    Expires: "+
                DateTimeOffset.FromUnixTimeSeconds(upReply.ExpireTime.Seconds));
            Console.WriteLine("    Pages:");
            foreach (var page in upReply.ProcessedPages)
            {
                Console.WriteLine("      PageNum:        "+page.PageNumber);
                Console.WriteLine("      Size (cm):      "+page.Size);
                Console.WriteLine("      DegreesRotated: "+page.DegreesRotated);
            }

            // For kicks we're going to re-download the rendered pdf page
            var getRequest = new GetPdfPageRequest
            {
                Name = upReply.Name,
                PageNumber = pageNum,
            };
            var getReply = client.GetPdfPage(getRequest, reqHead);
            var pdfPageFilename = upReply.Name + "-pdfpage.png";
            File.WriteAllBytes(pdfPageFilename, getReply.Page.PngData.ToByteArray());
            Console.WriteLine("  GetPdfPage Reply:");
            Console.WriteLine("    Size (cm):      "+getReply.Page.Size);
            Console.WriteLine("    DegreesRotated: "+getReply.Page.DegreesRotated);
            Console.WriteLine("    Filename:       "+pdfPageFilename);

            return upReply.Name;
        }

        /// <summary>
        /// UploadDimImage uploads a dimensioned image to the system.
        /// </summary>
        static string UploadDimImage(Exemplars.ExemplarsClient client, Metadata reqHead,
            byte[] fileBytes, string imgName, string ext)
        {
            var width = 21.59;  // width of image when printed on paper in cm
            var height = 27.94; // height of image when printed on paper in cm

            var encoding = new ImageEncoding();
            if (ext == ".jpg") encoding = ImageEncoding.Jpeg;
            if (ext == ".png") encoding = ImageEncoding.Png;
            if (ext == ".tif") encoding = ImageEncoding.Tiff;

            var upRequest = new UploadDimensionedImageRequest
            {
                Name = imgName,
                Image = new Nsys.Api.Ntypes.DimensionedImage
                {   
                    Image = new Nsys.Api.Ntypes.Image
                    {
                        Data = Google.Protobuf.ByteString.CopyFrom(fileBytes),
                        Encoding = encoding
                    },
                    Size = new Nsys.Api.Ntypes.Size
                    {
                        Width = width, 
                        Height = height
                    }
                }
            };

            var upReply = client.UploadDimensionedImage(upRequest, reqHead);

            Console.WriteLine("  UploadDimensionedImageResponse:");
            Console.WriteLine("    Name:    "+upReply.Name);
            Console.WriteLine("    Expires: "+
                DateTimeOffset.FromUnixTimeSeconds(upReply.ExpireTime.Seconds));

            return upReply.Name;
        }

        /// <summary>
        /// CreateExemplar creates an exemplar from either a pdf page, or a dimensioned
        /// image. If using a dimensioned image the pageNum must be zero.
        /// </summary>
        static string CreateExemplar(Exemplars.ExemplarsClient client, Metadata reqHead,
            string pdfImgName, int pageNum, string exemDescription, string exemName)
        {
            var createRequest = new CreateExemplarRequest
            {
                Name = exemName,
                Description = exemDescription,
                ImageOrPdfName = pdfImgName,
                PageNumber = pageNum
            };
            var createReply = client.CreateExemplar(createRequest, reqHead);
            Console.WriteLine("  CreateExemplarResponse:");
            Console.WriteLine("    "+createReply);

            return createReply.Name;
        }

        /// <summary>
        /// ListExemplars simply lists all the exemplars associated with our account.
        /// </summary>
        static void ListExemplars(Exemplars.ExemplarsClient client, Metadata reqHead)
        {
            var reply = client.ListExemplars(new ListExemplarsRequest{}, reqHead);
            Console.WriteLine("  ListExemplarsResponse: "+reply.Exemplars.Count);
            foreach(var e in reply.Exemplars)
            {
                Console.WriteLine("    "+e);
            }
        }

        /// <summary>
        /// GetExemplar gets an exemplar and saves its image to disk.
        /// </summary>
        static void GetExemplar(Exemplars.ExemplarsClient client, Metadata reqHead,
            string exemName)
        {
            var req = new GetExemplarRequest
            {
                ExemplarName = exemName,
                IncludeImage = true
            };
            var reply = client.GetExemplar(req, reqHead);
            var exemplarFileName = reply.Exemplar.Name + "-exemplar.png";
            File.WriteAllBytes(exemplarFileName, reply.Exemplar.PngData.ToByteArray());
            Console.WriteLine("  GetExemplarResponse:");
            Console.WriteLine("    Name:        " + reply.Exemplar.Name);
            Console.WriteLine("    Description: " + reply.Exemplar.Description);
            Console.WriteLine("    CreateTime:  " +
                DateTimeOffset.FromUnixTimeSeconds(reply.Exemplar.CreateTime.Seconds));
            Console.WriteLine("    Size (cm):   " + reply.Exemplar.Size);
            Console.WriteLine("    Metadata:    " + reply.Exemplar.Metadata);
            Console.WriteLine("    Filename:    " + exemplarFileName);
        }

        /// <summary>
        /// CreateField creates a signature present puzzle field in the center of the
        /// given exemplar. It puts the field in the center of what would be a 8.5x11
        /// inch page.
        /// </summary>
        static string CreateField(Exemplars.ExemplarsClient client, Metadata reqHead,
            string exemName, string fieldName, string fieldDescription)
        {
            // create a sample field in the center of the form
            var createFieldReq = new CreateFieldRequest
            {
                ExemplarName = exemName,
                Field = new Field
                {
                    Name = fieldName,
                    Description = fieldDescription,
                    SignaturePresent = new SignaturePresentPuzzle
                    {
                        Rect = new Rectangle
                        {
                            MinX = 5.3975,
                            MinY = 6.985,
                            MaxX = 16.1925,
                            MaxY = 20.955
                        }
                    }
                }
            };

            var reply = client.CreateField(createFieldReq, reqHead);

            Console.WriteLine("  CreatedFieldResponse:");
            Console.WriteLine("    "+reply);
            return reply.FieldName;
        }

        /// <summary>
        /// ListFields simply lists all the fields associated with the given exemplar.
        /// </summary>
        static void ListFields(Exemplars.ExemplarsClient client, Metadata reqHead,
            string exemName)
        {
            var req = new ListFieldsRequest{ExemplarName=exemName};
            var reply = client.ListFields(req, reqHead);
            Console.WriteLine("  ListFieldsResponse: "+reply.Fields.Count);
            foreach (var f in reply.Fields)
            {
                Console.WriteLine("    " + f);
            }
        }
    }
}
