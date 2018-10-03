using System;
using System.Threading;
using System.Threading.Tasks;

namespace CertificateRemover
{
    class Program
    {
        static void Main(string[] args)
        {
            var certificate = args[0];
            var remover = new CertificateRemover();
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            Console.WriteLine("Press any key to exit...");
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        token.ThrowIfCancellationRequested();
                    }
                    remover.TryDelete(certificate);
                    Thread.Sleep(1000);
                }
            }, token);

            Console.ReadKey();
            cts.Cancel();
            remover.Dispose();
        }
    }
}
