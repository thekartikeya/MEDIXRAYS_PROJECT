using FellowOakDicom;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using System;
using System.Threading.Tasks;

namespace DicomNActionClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = DicomClientFactory.Create("127.0.0.1", 2001, false, "TESTSCU", "MEDIXRAYS");

            var sopInstanceUID = DicomUID.Generate();
            var sopClassUID = DicomUID.BasicFilmSession;

            var nActionRequest = new DicomNActionRequest(sopClassUID, sopInstanceUID, 1);
            nActionRequest.OnResponseReceived += (req, resp) =>
            {
                Console.WriteLine($"[CLIENT] Response Status: {resp.Status}");
            };

            await client.AddRequestAsync(nActionRequest);

            Console.WriteLine("[CLIENT] Sending N-ACTION...");
            await client.SendAsync();
            Console.WriteLine("[CLIENT] Done.");
        }
    }
}
