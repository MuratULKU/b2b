using System.ComponentModel.DataAnnotations;

namespace B2C.Data
{
    public class UserRegisterModel
    {
        [Required (ErrorMessage = "İsinizi Yazın")]
        [MinLength(3, ErrorMessage = "En az 3 Karakter")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyadınızı Yazın")]
        [MinLength(2, ErrorMessage = "En az 2 Karakter")]
        public string SurName { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "En az 6 Karakter")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Şifrenizi Yazın")]
        [Compare(nameof(UserRegisterModel.Password), ErrorMessage = "Şifreler Aynı Değil")]
        public string Paswword2 {  get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
