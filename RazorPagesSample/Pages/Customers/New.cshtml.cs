using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesSample.Data;
using System;
using System.Threading.Tasks;

namespace RazorPagesSample.Pages.Customers
{
    public class NewModel : PageModel
    {
        private readonly AppDbContext _db;

        public NewModel(AppDbContext db)
        {
            _db = db;
        }
        
        [BindProperty]
        public Customer Customer { get; set; }

        [TempData]
        public string Message { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(500));

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(500));

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Customers.Add(Customer);
            await _db.SaveChangesAsync();

            Message = "New customer created successfully!";

            return RedirectToPage("./Index");
        }
    }
}
