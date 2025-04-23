using Dicom;
using Dicom.Log;
using Dicom.Network;
using MEDIXRAYS.Modules;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MEDIXRAYS
{
    internal class DicomPrintServer : DicomService, IDicomServiceProvider, IDicomNServiceProvider
    {
        public DicomPrintServer(INetworkStream stream, Encoding fallbackEncoding)
     : base(stream, fallbackEncoding, LogManager.GetLogger("DicomPrintServer"))
        { }

        // IDicomServiceProvider methods
        public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            // Handle abort
        }

        public void OnConnectionClosed(Exception exception)
        {
            // Handle connection closed
        }

        public DicomCEchoResponse OnCEchoRequest(DicomCEchoRequest request)
        {
            return new DicomCEchoResponse(request, DicomStatus.Success);
        }

        public async Task OnReceiveAssociationRequestAsync(DicomAssociation association)
        {
            Console.WriteLine($"[ASSOCIATION] Received from {association.CallingAE}");

            if (association.CalledAE == "MEDIXRAYS") // Customize your AE Title
            {
                foreach (var context in association.PresentationContexts)
                {
                    context.AcceptTransferSyntaxes(DicomTransferSyntax.ImplicitVRLittleEndian);
                }

                await SendAssociationAcceptAsync(association);
            }
            else
            {
                await SendAssociationRejectAsync(
                    DicomRejectResult.Permanent,
                    DicomRejectSource.ServiceUser,
                    DicomRejectReason.CalledAENotRecognized);
            }
        }
        public Task OnReceiveAssociationReleaseRequestAsync()
        {
            Console.WriteLine("[ASSOCIATION] Release requested");
            return SendAssociationReleaseResponseAsync();
        }

        // IDicomNServiceProvider methods (for N-CREATE, N-SET, N-ACTION, etc.)

        public DicomNCreateResponse OnNCreateRequest(DicomNCreateRequest request)
        {
            // Basic success response
            return new DicomNCreateResponse(request, DicomStatus.Success);
        }

        public DicomNSetResponse OnNSetRequest(DicomNSetRequest request)
        {
            return new DicomNSetResponse(request, DicomStatus.Success);
        }
        public DicomNActionResponse OnNActionRequest(DicomNActionRequest request)
        {
            string savedDicomPath = "C:\\DICOM\\received.dcm";
            var image = DicomToImageConverter.ConvertToImage(savedDicomPath);
            PrinterService.PrintImage(image);

            return new DicomNActionResponse(request, DicomStatus.Success);
        }
        public DicomNDeleteResponse OnNDeleteRequest(DicomNDeleteRequest request)
        {
            return new DicomNDeleteResponse(request, DicomStatus.Success);
        }

        public DicomNGetResponse OnNGetRequest(DicomNGetRequest request)
        {
            return new DicomNGetResponse(request, DicomStatus.Success);
        }

        public DicomNEventReportResponse OnNEventReportRequest(DicomNEventReportRequest request)
        {
            return new DicomNEventReportResponse(request, DicomStatus.Success);
        }
        public Task OnReceiveAbortAsync(DicomAbortSource source, DicomAbortReason reason)
        {
            Console.WriteLine($"[ABORT] Source: {source}, Reason: {reason}");
            return Task.CompletedTask;
        }

        public Task OnConnectionClosedAsync(Exception exception)
        {
            Console.WriteLine($"[DISCONNECTED] {exception?.Message ?? "Connection closed"}");
            return Task.CompletedTask;
        }

    }
}
