using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Admin.Models
{
  public class IndexViewModel
  {
    public bool HasPassword { get; set; }
    public IList<UserLoginInfo> Logins { get; set; }
    public string PhoneNumber { get; set; }
    public bool TwoFactor { get; set; }
    public bool BrowserRemembered { get; set; }
  }

  public class ChangePasswordViewModel
  {
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Senha atual")]
    public string OldPassword { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "A senha deve ter no mínimo 6 dígitos.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Nova senha")]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirmar nova senha")]
    [Compare("NewPassword", ErrorMessage = "A nova senha e a confirmação devem ser iguais.")]
    public string ConfirmPassword { get; set; }
  }
}