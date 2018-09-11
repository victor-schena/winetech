using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Models
{
  public class RoleViewModel
  {
    public string Id { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "O campo nome é obrigatório.")]
    [Display(Name = "Nome")]
    public string Name { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "O campo descrição é obrigatório.")]
    [Display(Name = "Descrição")]
    public string Description { get; set; }
  }

  public class EditUserViewModel
  {
    public string Id { get; set; }

    [Display(Name = "Foto")]
    public string Img { get; set; }

    public string PostedImg { get; set; }

    [Required(ErrorMessage = "O campo nome é obrigatório.")]
    [Display(Name = "Nome")]
    public string Name { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "O campo email é obrigatório.")]
    [Display(Name = "Email")]
    [EmailAddress]
    public string Email { get; set; }

    public IEnumerable<SelectListItem> RolesList { get; set; }
  }
}