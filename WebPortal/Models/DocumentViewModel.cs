using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;

namespace WebPortal.Models
{
    public class DocumentViewModel
    {
        public List<Division> Divisions { get; set; }
        public List<DocumentType> DocumentTypes { get; set; }
        public List<Certificate> Certificates { get; set; }

        public List<Format> Formats { get; set; }

        public List<Procedure> Procedures { get; set; }

        public List<CertificateViewModel> CertificatesViewModel { get; set; } = new List<CertificateViewModel>();

    }
}
