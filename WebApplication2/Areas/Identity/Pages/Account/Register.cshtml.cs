using MaintenanceServiceMVC.Data;
using MaintenanceServiceMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

public class RegisterModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly AppDbContext _context;

    public RegisterModel(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        AppDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public string? ReturnUrl { get; set; }

    public class InputModel
    {
        [Required(ErrorMessage = "User type is required")]
        public string UserType { get; set; } = string.Empty; // Customer or Professional

        [Required, StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).+$",
            ErrorMessage = "Password must have uppercase, lowercase, number, and special character")]
        public string Password { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required, Phone, StringLength(25)]
        public string Phone { get; set; } = string.Empty;

        // Customer fields
        public string? CustomerAddress { get; set; }

        // Professional fields
        public string? Specialty { get; set; }
        //public int? ExperienceYears { get; set; }
        //public decimal? HourlyRate { get; set; }
    }

    public IActionResult OnGet(string? returnUrl = null)
    {
        if (_signInManager.IsSignedIn(User))
        {
            return RedirectToPage("/Index"); // or "/Account/Profile"
        }

        ReturnUrl = returnUrl;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        ReturnUrl = returnUrl;

        if (ModelState.IsValid)
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToPage("/Index");
            }


            var user = new ApplicationUser
            {
                UserName = Input.Email,
                Email = Input.Email,
                FullName = Input.FullName,
                PhoneNumber = Input.Phone
            };

            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                // Assign role based on UserType
                if (Input.UserType == "Customer")
                {
                    await _userManager.AddToRoleAsync(user, "Customer");

                    _context.Customers.Add(new Customer
                    {
                        Name = Input.FullName,
                        Email = Input.Email,
                        Phone = Input.Phone,
                        Address = Input.CustomerAddress,
                        UserId = user.Id
                    });
                }
                else if (Input.UserType == "Professional")
                {
                    await _userManager.AddToRoleAsync(user, "Professional");

                    _context.Professionals.Add(new Professional
                    {
                        Name = Input.FullName,
                        Email = Input.Email,
                        Phone = Input.Phone,
                        Specialty = Input.Specialty ?? "",
                        //ExperienceYears = Input.ExperienceYears ?? 0,
                        //HourlyRate = Input.HourlyRate ?? 0,
                        UserId = user.Id
                    });
                }

                await _context.SaveChangesAsync();

                // Default MS Identity: just sign in directly
                await _signInManager.SignInAsync(user, isPersistent: false);

                return LocalRedirect(returnUrl ?? "/");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        // If we got this far, something failed.
        return Page();
    }
}


