using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.ViewModels
{
    public class ResetPasswordVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name="Unesite ponovo lozinku")]
        [Compare("Password", ErrorMessage = "Lozinke moraju biti iste")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}
