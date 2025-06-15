    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.ComponentModel.DataAnnotations;

    namespace DataEntity.ViewModels
    {

        public class ResetPasswordViewModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public string Token { get; set; }

            [Required]
            [MinLength(6)]
            public string NewPassword { get; set; }

            [Required]
            [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
            public string ConfirmPassword { get; set; }
        }


    }
