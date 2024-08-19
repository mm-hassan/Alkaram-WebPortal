using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPortal.Models;

namespace WebPortal.Controllers
{
    public class FormatController : Controller
    {
        private readonly PortalContext context;
        private readonly IWebHostEnvironment env;

        public FormatController(PortalContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }

        public IActionResult Index(string division)
        {
            //List<Format> Formats;
            var Format = context.Formats.ToList();
            if (string.IsNullOrEmpty(division) || division == "All")
            {
                Format = context.Formats.ToList();
                ViewBag.Division = "All";
            }
            else
            {
                Format = context.Formats.Where(d => d.Division == division).ToList();
                ViewBag.Division = division;
            }

            var viewModel = new DocumentViewModel
            {
                Formats = Format,
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Admin,Super,QMS_ADMIN,BT_ADMIN")]
        public IActionResult AddProduct(string division)
        {
            // Check if the user is a Super Admin
            if (User.IsInRole("Super") || User.IsInRole("Admin"))
            {
                // Super Admins have access to all divisions, no need to check further
                ViewBag.Division = division;
                return View();
            }
            else
            {
                var roles = User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Role)
                                       .Select(c => c.Value)
                                       .ToList();

                // Assuming roles are in the format QMS_ADMIN, QMS_USER, etc.
                bool hasAccess = roles.Any(r => r.Split('_')[0] == division);

                if (!hasAccess)
                {
                    return Forbid();
                }
            }
            ViewBag.Division = division;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Super,QMS_ADMIN,BT_ADMIN")]
        public IActionResult AddProduct(FormatViewModel prod, string division)
        {
            string userEmail = User.Identity.Name;
            DateTime currentTime = DateTime.Now;

            var roles = User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Role)
                                       .Select(c => c.Value)
                                       .ToList();

            if (User.IsInRole("Super") || User.IsInRole("Admin") || roles.Any(r => r.Split('_')[0] == division))
            {
                if (ModelState.IsValid)
                {
                    string fileName = "";
                    if (prod.Path != null)
                    {
                        string folder = Path.Combine(env.WebRootPath, "images");
                        fileName = Guid.NewGuid().ToString() + "_" + prod.Path.FileName;
                        string filePath = Path.Combine(folder, fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            prod.Path.CopyTo(fileStream);
                        }

                        Format p = new Format
                        {
                            Name = prod.Name,
                            Division = prod.Division,
                            FormatPath = fileName,
                            LastUpdatedBy = userEmail,
                            LastUpdatedOn = currentTime
                        };
                        context.Formats.Add(p);
                        context.SaveChanges();
                        TempData["Success"] = "File Added...";
                        return RedirectToAction("Index", new { division });
                    }
                }
            }
            else
            {
                return Forbid();
            }

            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await context.Formats.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Super,QMS_ADMIN,BT_ADMIN")]
        public async Task<IActionResult> Edit(int id, string division)
        {
            var roles = User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Role)
                                       .Select(c => c.Value)
                                       .ToList();

            if (User.IsInRole("Super") || User.IsInRole("Admin") || roles.Any(r => r.Split('_')[0] == division))
            {
                var product = await context.Formats.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                var FormatViewModel = new FormatViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Division = product.Division,

                };
                ViewBag.Division = division;
                return View(FormatViewModel);
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Super,QMS_ADMIN,BT_ADMIN")]
        public async Task<IActionResult> Edit(FormatViewModel formatViewModel, string division)
        {
            string userEmail = User.Identity.Name;
            DateTime currentTime = DateTime.Now;

            var roles = User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Role)
                                       .Select(c => c.Value)
                                       .ToList();

            if (User.IsInRole("Super") || User.IsInRole("Admin") || roles.Any(r => r.Split('_')[0] == division))
            {
                if (ModelState.IsValid)
                {
                    var product = await context.Formats.FindAsync(formatViewModel.Id);
                    if (product == null)
                    {
                        return NotFound();
                    }

                    if (formatViewModel.Path != null)
                    {
                        // Delete the old image if exists
                        if (!string.IsNullOrEmpty(product.FormatPath))
                        {
                            var oldFilePath = Path.Combine(env.WebRootPath, "images", product.FormatPath);
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                try
                                {
                                    System.IO.File.Delete(oldFilePath);
                                }
                                catch (IOException ex)
                                {
                                    // Log the exception or handle it as needed
                                    TempData["Error"] = $"Error deleting old file: {ex.Message}";
                                    return View(formatViewModel);
                                }
                            }
                        }

                        // Save the new image
                        string folder = Path.Combine(env.WebRootPath, "images");
                        string fileName = Guid.NewGuid().ToString() + "_" + formatViewModel.Path.FileName;
                        string filePath = Path.Combine(folder, fileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await formatViewModel.Path.CopyToAsync(fileStream);
                        }

                        product.FormatPath = fileName;
                    }

                    product.Name = formatViewModel.Name;
                    product.Division = formatViewModel.Division;
                    product.LastUpdatedBy = userEmail;
                    product.LastUpdatedOn = currentTime;
                    // other properties if any

                    try
                    {
                        context.Update(product);
                        await context.SaveChangesAsync();
                        TempData["Success"] = "Product Updated...";
                        return RedirectToAction("Index", new { division = product.Division });
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProductExists(formatViewModel.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return View(formatViewModel);
            }
            else
            {
                return Forbid();
            }
        }

        private bool ProductExists(int id)
        {
            return context.Formats.Any(e => e.Id == id);
        }

        [Authorize(Roles = "Admin,Super,QMS_ADMIN,BT_ADMIN")]
        public async Task<IActionResult> Delete(int id, string division)
        {
            var roles = User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Role)
                                       .Select(c => c.Value)
                                       .ToList();

            if (User.IsInRole("Super") || User.IsInRole("Admin") || roles.Any(r => r.Split('_')[0] == division))
            {
                ViewBag.Division = division;
                var product = await context.Formats.FirstOrDefaultAsync(p => p.Id == id);
                if (product == null)
                {
                    ViewBag.Division = division;
                    return NotFound();
                }
                return View(product);
            }
            else
            {
                return Forbid();
            }

        }


        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin,Super,QMS_ADMIN,BT_ADMIN")]
        public async Task<IActionResult> DeleteConfirmed(int id, string division)
        {
            var roles = User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Role)
                                      .Select(c => c.Value)
                                      .ToList();

            if (User.IsInRole("Super") || User.IsInRole("Admin") || roles.Any(r => r.Split('_')[0] == division))
            {
                var product = await context.Formats.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                // Delete the associated file from the folder
                if (!string.IsNullOrEmpty(product.FormatPath))
                {
                    var filePath = Path.Combine(env.WebRootPath, "images", product.FormatPath);
                    if (System.IO.File.Exists(filePath))
                    {
                        try
                        {
                            System.IO.File.Delete(filePath);
                            Console.WriteLine($"File {filePath} deleted successfully.");
                        }
                        catch (IOException ex)
                        {
                            // Log the exception or handle it as needed
                            Console.WriteLine($"Error deleting file: {ex.Message}");
                            TempData["Error"] = $"Error deleting file: {ex.Message}";
                            return RedirectToAction("Index", new { division = product.Division });
                        }
                    }
                    else
                    {
                        Console.WriteLine($"File {filePath} does not exist.");
                    }
                }
                else
                {
                    Console.WriteLine("No file path provided.");
                }

                context.Formats.Remove(product);
                await context.SaveChangesAsync();
                TempData["Success"] = "Product Deleted...";
                return RedirectToAction("Index", new { division = product.Division });
            }
            else
            {
                return Forbid();
            }
        }

        public IActionResult ViewFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return NotFound();
            }

            var filePath = Path.Combine(env.WebRootPath, "images", fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var contentType = "application/pdf"; // Set the content type based on your file type
            return PhysicalFile(filePath, contentType);
        }




    }
}
