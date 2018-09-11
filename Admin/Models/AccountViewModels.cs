using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Admin.Models
{
  public class ExternalLoginConfirmationViewModel
  {
    [Required(ErrorMessage = "O campo email é obrigatório.")]
    [Display(Name = "Email")]
    public string Email { get; set; }
  }

  public class ExternalLoginListViewModel
  {
    public string ReturnUrl { get; set; }
  }

  public class SendCodeViewModel
  {
    public string SelectedProvider { get; set; }
    public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    public string ReturnUrl { get; set; }
    public bool RememberMe { get; set; }
  }

  public class VerifyCodeViewModel
  {
    [Required]
    public string Provider { get; set; }

    [Required]
    [Display(Name = "Código")]
    public string Code { get; set; }
    public string ReturnUrl { get; set; }

    [Display(Name = "Lembrar-me nesse navegador")]
    public bool RememberBrowser { get; set; }

    public bool RememberMe { get; set; }
  }

  public class ForgotViewModel
  {
    [Required(ErrorMessage = "O campo email é obrigatório.")]
    [Display(Name = "Email")]
    public string Email { get; set; }
  }

  public class LoginViewModel
  {
    [Required(ErrorMessage = "Digite o seu endereço de email.")]
    [Display(Name = "Email")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Digite a sua senha.")]
    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    public string Password { get; set; }

    [Display(Name = "Continuar conectado")]
    public bool RememberMe { get; set; }
  }

  public class RegisterViewModel
  {
    [Display(Name = "Foto")]
    public string Img { get; set; }

    [Required(ErrorMessage = "O campo nome é obrigatório.")]
    [Display(Name = "Nome")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O campo email é obrigatório.")]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo senha é obrigatório.")]
    [StringLength(100, ErrorMessage = "A senha deve ter no mínimo 6 dígitos.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirmar senha")]
    [Compare("Password", ErrorMessage = "A senha e a confirmação devem ser iguais.")]
    public string ConfirmPassword { get; set; }
  }

  public class ResetPasswordViewModel
  {
    [Required(ErrorMessage = "O campo email é obrigatório.")]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo senha é obrigatório.")]
    [StringLength(100, ErrorMessage = "A senha deve ter no mínimo 6 dígitos.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirmar senha")]
    [Compare("Password", ErrorMessage = "A senha e a confirmação devem ser iguais.")]
    public string ConfirmPassword { get; set; }

    public string Code { get; set; }
  }

  public class ForgotPasswordViewModel
  {
    [Required(ErrorMessage = "O campo email é obrigatório.")]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }
  }
}
