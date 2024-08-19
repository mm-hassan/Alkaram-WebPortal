using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebPortal.Models;
using WebPortal.Data;

namespace WebPortal.Models
{
    public class DocumentViewModelFilter : IActionFilter
    {
        private readonly PortalContext _context;

        // Inject the DbContext via the constructor
        public DocumentViewModelFilter(PortalContext context)
        {
            _context = context;
        }

        // This method runs before the action executes
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as Controller;
            if (controller != null)
            {
                // Fetch the data needed for the DocumentViewModel
                var viewModel = new DocumentViewModel
                {
                    Divisions = _context.Divisions.ToList(),
                    DocumentTypes = _context.DocumentTypes.ToList(),
                    Certificates = new List<Certificate>() // Initialize to avoid null reference
                };

                // Make the viewModel available in ViewData
                controller.ViewData["DocumentViewModel"] = viewModel;
            }
        }

        // This method runs after the action executes (not needed in this case)
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // No action needed after the execution
        }



    }
}
