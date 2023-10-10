using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesSample.Data;
using System;
using System.Threading.Tasks;

namespace RazorPagesSample.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _db;

        public EditModel(AppDbContext db)
        {
            _db = db;
        }

        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(500));

            Customer = await _db.Customers.FindAsync(id);

            if (Customer == null)
            {
                Message = $"Customer {id} not found!";
                return RedirectToPage("./Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(500));

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Attach(Customer).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
                Message = "Customer updated successfully!";
            }
            catch (DbUpdateConcurrencyException)
            {
                Message = $"Customer {Customer.Id} not found!";
            }

            return RedirectToPage("./Index");
        }
    }
}
