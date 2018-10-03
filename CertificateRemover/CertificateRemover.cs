using System;
using System.Security.Cryptography.X509Certificates;

namespace CertificateRemover
{
    public class CertificateRemover : IDisposable
    {
        X509Store store;
        public CertificateRemover()
        {
            store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadWrite);
        }

        public bool TryDelete(string certificate)
        {
            X509Certificate2Collection col = store.Certificates.Find(X509FindType.FindBySubjectName, certificate, false);

            if (col.Count == 0)
                return false;

            foreach (var cert in col)
            {
                Console.WriteLine(cert.SubjectName.Name);
                //store.Remove(cert);
            }

            return true;
        }

        public void Dispose()
        {
            store.Dispose();
        }
    }
}
