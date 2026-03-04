using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using task_tracker.Data;
using task_tracker.Models;

namespace MyApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        // Optional: show any database error on the page
        public string? ErrorMessage { get; set; }

        // Optional: list of users to display
        public List<User> Users { get; set; } = new();

        public async Task OnGetAsync()
        {
            try
            {
                // Insert a default user if table is empty
                if (!await _context.Users.AnyAsync())
                {
                    _context.Users.Add(new User
                    {
                        Name = "Alice",
                        Email = "alice@example.com"
                    });

                    await _context.SaveChangesAsync();
                }

                // Load all users to display
                Users = await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log and show errors
                ErrorMessage = ex.Message;
                Console.WriteLine($"Database error: {ex}");
            }
        }
    }
}