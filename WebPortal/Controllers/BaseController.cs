using Microsoft.AspNetCore.Mvc;
using WebPortal.Data;
using WebPortal.Models;

namespace WebPortal.Controllers
{
    public class BaseController : Controller
    {
        private readonly PortalContext _context;

        public BaseController(PortalContext context)
        {
            _context = context;
        }

        public DocumentViewModel GetDocumentViewModel()
        {
            var divisions = _context.Divisions.ToList();
            var documentTypes = _context.DocumentTypes.ToList();

            return new DocumentViewModel
            {
                Divisions = divisions,
                DocumentTypes = documentTypes
            };
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
