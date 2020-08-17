using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SurveyApiApplication.Data;

namespace ClinicalSurveyApp.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "Admin")]
    public class AdminModel : PageModel
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly DbContextOptions<ApplicationDbContext> _contextOptions;

        [BindProperty]
        public InputModel Input { get; set; }
        public AdminModel(UserManager<IdentityUser> userManager, DbContextOptions<ApplicationDbContext> contextOptions)
        {
            _userManager = userManager;
            _contextOptions = contextOptions;
        }

        public class InputModel
        {
            public List<IdentityUser> Users { get; set; }
            public List<String> Roles { get; set; }


        }
    }
    
}
