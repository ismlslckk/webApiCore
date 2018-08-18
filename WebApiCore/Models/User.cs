using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.Models
{
    public class User
    {
        [Required]
        [StringLength(50,ErrorMessage = "Firstname length max 50 and min 3",MinimumLength = 3)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Firstname length max 50 and min 3", MinimumLength = 3)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required] 
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{4,15}$", ErrorMessage = "Password en az 1 küçük harf,1büyük harf,1 sayı ve bir eöl karakter içermelidir.")]
        public string Password { get; set; }
        [Required]
        [Compare("Password",ErrorMessage = "passwords do not match")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Range(18,70,ErrorMessage = "age range 18 to 70")]
        public int BirthYear { get; set; }         
        [CreditCard]
        public string CreditCart { get; set; }
        [Url]
        public string FacebookProfilUrl { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }         
     }
}
