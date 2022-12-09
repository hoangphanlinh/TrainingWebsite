using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrainingWebsite.Areas.Identity.Data;
using TrainingWebsite.Data;
using TrainingWebsite.Models;

namespace TrainingWebsite.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _dataContext;



        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, ApplicationDbContext dataContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dataContext = dataContext;
          
           
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }



        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            [DataType(DataType.Text)]
            [Display(Name ="Full Name")]
            public string FullName { get; set; }
            [DataType(DataType.ImageUrl)]
            [Display(Name = "Profile Image")]
            public byte[] Picture { get; set; }
            [DataType(DataType.Text)]
            [Display(Name = "Address")]
            public string Address { get; set; }
            [DataType(DataType.DateTime)]
            [Display(Name = "Birth Date")]
            public DateTime DateBirth { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Experence Level")]
            public int? LevelID { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Job Position")] 
            public int? OccuptionID { get; set; }




        }

     
        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var fullName = user.FullName;
            var picture = user.Image;
            var address = user.Address;
            var datebirth = user.BirthDate;
            var level = user.LevelID;
            var occuption = user.OccuptionID;

            var LevelList = (from level1 in _dataContext.Levels
                             select new SelectListItem()
                             {
                                 Text = level1.LevelName,
                                 Value = level1.ID.ToString()


                             }).ToList();
            LevelList.Insert(0, new SelectListItem()
            {
                Text = "-----Select-----",
                Value = string.Empty
            });
            ViewData["ListOfLevel"] = LevelList;

            var OccuptionList = (from s in _dataContext.Occuptions
                             select new SelectListItem()
                             {
                                 Text = s.OccuptionName,
                                 Value = s.OccuptionID.ToString()


                             }).ToList();
            OccuptionList.Insert(0, new SelectListItem()
            {
                Text = "-----Select-----",
                Value = string.Empty
            });
            ViewData["ListOfOccuption"] = OccuptionList;


            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                FullName = fullName,
                Picture = picture,
                Address = address,
                DateBirth = datebirth,
                LevelID = level,
                OccuptionID = occuption,
               
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
          
            await LoadAsync(user);
            return Page();
        }
        

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }
            var LevelList = (from level1 in _dataContext.Levels
                             select new SelectListItem()
                             {
                                 Text = level1.LevelName,
                                 Value = level1.ID.ToString()


                             }).ToList();
            LevelList.Insert(0, new SelectListItem()
            {
                Text = "-----Select-----",
                Value = string.Empty
            });
            ViewData["ListOfLevel"] = LevelList;
            
            ////List of Occuption for drop down
            var OccuptionList = (from s in _dataContext.Occuptions
                                 select new SelectListItem()
                                 {
                                     Text = s.OccuptionName,
                                     Value = s.OccuptionID.ToString()


                                 }).ToList();
            OccuptionList.Insert(0, new SelectListItem()
            {
                Text = "-----Select-----",
                Value = string.Empty
            });
            ViewData["ListOfOccuption"] = OccuptionList;


            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
            var fullName = user.FullName;
            var address = user.Address;
            var datebirth = user.BirthDate;
            var level = user.LevelID;
            var occuption = user.OccuptionID;
            if (Input.FullName != fullName)
            {
                user.FullName = Input.FullName;
                await _userManager.UpdateAsync(user);
            }
            if (Input.Address != address)
            {
                user.Address = Input.Address;
                await _userManager.UpdateAsync(user);
            }
            if (Input.DateBirth != datebirth)
            {
                user.BirthDate = Input.DateBirth;
                await _userManager.UpdateAsync(user);
            }
            if (Input.LevelID != level)
            {
                user.LevelID = Input.LevelID;
                await _userManager.UpdateAsync(user);
            }
            if (Input.OccuptionID != occuption)
            {
                user.OccuptionID = Input.OccuptionID;
                await _userManager.UpdateAsync(user);
            }

            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    user.Image = dataStream.ToArray();
                }
                await _userManager.UpdateAsync(user);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
