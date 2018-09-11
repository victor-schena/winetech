using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Admin.Models;
using Admin.Attributes;
using Admin.Functions;
using System.IO;

namespace Admin.Controllers
{
  [Authorize]
  [Restrict]
  public class UsersController : Controller
  {
    #region Constructor

    public UsersController()
    {
    }

    public UsersController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
    {
      UserManager = userManager;
      RoleManager = roleManager;
    }

    #endregion

    #region Properties

    private ApplicationUserManager _userManager;
    public ApplicationUserManager UserManager
    {
      get
      {
        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
      }
      private set
      {
        _userManager = value;
      }
    }

    private ApplicationRoleManager _roleManager;
    public ApplicationRoleManager RoleManager
    {
      get
      {
        return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
      }
      private set
      {
        _roleManager = value;
      }
    }

    #endregion

    #region Actions

    [Route("users")]
    public async Task<ActionResult> Index()
    {
      return View(await UserManager.Users.OrderBy(u => u.Name).ToListAsync());
    }

    [Route("users/details/{id}")]
    public async Task<ActionResult> Details(string id)
    {
      if (id == null) // BadRequest
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      var user = await UserManager.FindByIdAsync(id);
      if (user == null) // NotFound
      {
        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
      }

      ViewBag.RoleNames = await UserManager.GetRolesAsync(user.Id);
      ViewBag.User = user.UserName;

      return View(user);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("users/create")]
    public async Task<ActionResult> Create()
    {
      // Lista de Roles
      ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
      return View();
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("users/create")]
    public async Task<ActionResult> Create(RegisterViewModel userViewModel, params string[] selectedRoles)
    {
      if (ModelState.IsValid) // Model Válida
      {
        if (Request.Files.Count > 0)
        {
          var img = Request.Files["Img"];
          if (img != null && img.ContentLength > Admin.Functions.Constants.ImageFileSize)
          {
            ModelState.AddModelError("Img", string.Format("Tamanho máximo da imagem: {0} KB", Admin.Functions.Constants.ImageFileSize / 1024));
          }

          if (!ModelState.IsValid)
          {
            ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
            return View();
          }

          if (img != null && img.ContentLength > 0)
            userViewModel.Img = FileManager.UploadSingleFile(img, Path.Combine(Server.MapPath("~/Uploads/Users")));
        }

        // Username e email
        var user = new ApplicationUser
        {
          UserName = userViewModel.Email,
          Email = userViewModel.Email,
          Name = userViewModel.Name,
          Img = userViewModel.Img
        };

        // Cria o usuário
        var createResult = await UserManager.CreateAsync(user, userViewModel.Password);
        
        // Adiciona o usuário criado as roles selecionadas, caso existam
        if (createResult.Succeeded)
        {
          if (selectedRoles != null)
          {
            var result = await UserManager.AddToRolesAsync(user.Id, selectedRoles);
            string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);

            var callbackUrl = Url.Action("ConfirmEmail", "Account",
              new { userId = user.Id, code = code },
              protocol: Request.Url.Scheme);
            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
            // Send an email with this link
            await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
            if (!result.Succeeded)
            {
              ModelState.AddModelError("", "Erro ao adicionar o usuário aos perfis selecionados.");
              ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
              return View();
            }
          }
        }
        else // Erro ao criar usuário
        {
          string errorMessage;
          if (createResult.Errors.First().Contains("taken")) // tradução das mensagens
            errorMessage = "O Email/Nome De Usuário já está sendo utilizado.";
          else
            errorMessage = "As senhas devem ter pelo menos uma letra maiúscula e uma minúscula [A-Z], um caractere especial [#-@] e um número [0-9].";

          ModelState.AddModelError("", errorMessage);
          ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
          return View();
        }

        // Mensagem de sucesso
    
        TempData["Success"] = "Usuário Salvo.";
        return RedirectToAction("Index");
      }

      ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
      return View();
    }

    [HttpGet]
    [Route("users/edit/{id}")]
    public async Task<ActionResult> Edit(string id)
    {
      if (id == null) // BadRequest
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      var user = await UserManager.FindByIdAsync(id);
      if (user == null) // NotFound
      {
        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
      }

      // Recupera as roles do usuário
      var userRoles = await UserManager.GetRolesAsync(user.Id);

      return View(new EditUserViewModel()
      {
        Id = user.Id,
        Email = user.Email,
        Name = user.Name,
        Img = user.Img,
        RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
        {
          Selected = userRoles.Contains(x.Name),
          Text = x.Name,
          Value = x.Name
        })
      });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("users/edit")]
    public async Task<ActionResult> EditPost([Bind(Include = "Email,Id,Name,Img")] EditUserViewModel editUser, params string[] selectedRoles)
    {
      var user = await UserManager.FindByIdAsync(editUser.Id);
      if (user == null) // NotFound
      {
        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
      }

      /* roles do usuário */
      var userRoles = await UserManager.GetRolesAsync(user.Id);

      if (ModelState.IsValid)
      {
        if (Request.Files.Count > 0)
        {
          var img = Request.Files["PostedImg"];
          if (img != null && img.ContentLength > Admin.Functions.Constants.ImageFileSize)
          {
            ModelState.AddModelError("Img", string.Format("Tamanho máximo da imagem: {0} KB", Admin.Functions.Constants.ImageFileSize / 1024));
          }

          if (!ModelState.IsValid)
          {
            return View("Edit", new EditUserViewModel()
            {
              Id = editUser.Id,
              Email = editUser.Email,
              Name = editUser.Name,
              Img = editUser.Img,
              RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
              {
                Selected = userRoles.Contains(x.Name),
                Text = x.Name,
                Value = x.Name
              })
            });
          }

          if (img != null && img.ContentLength > 0)
            user.Img = FileManager.UploadSingleFile(img, Path.Combine(Server.MapPath("~/Uploads/Users")), user.Img);
          else
            user.Img = editUser.Img;
        }

        user.Name = editUser.Name;
        user.UserName = editUser.Email;
        user.Email = editUser.Email;

        selectedRoles = selectedRoles ?? new string[] { };

        var result = await UserManager.AddToRolesAsync(user.Id, selectedRoles.Except(userRoles).ToArray<string>());
        if (!result.Succeeded)
        {
          TempData["Error"] = string.Format("O Email/Username <{0}> já está sendo utilizado.", editUser.Email);
          return RedirectToAction("Index");
        }

        result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRoles).ToArray<string>());
        if (!result.Succeeded)
        {
          TempData["Error"] = string.Format("O Email/Username <{0}> já está sendo utilizado.", editUser.Email);
          return RedirectToAction("Index");
        }

        TempData["Success"] = "Usuário Editado.";
        return RedirectToAction("Index");
      }

      ModelState.AddModelError("", "Erro ao salvar dados.");
      return View("Edit", new EditUserViewModel()
      {
        Id = editUser.Id,
        Email = editUser.Email,
        Name = editUser.Name,
        Img = editUser.Img,
        RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
        {
          Selected = userRoles.Contains(x.Name),
          Text = x.Name,
          Value = x.Name
        })
      });
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Route("users/delete")]
    public async Task<ActionResult> DeleteConfirmed(string id)
    {
      if (ModelState.IsValid)
      {
        if (id == null) // BadRequest
        {
          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        var user = await UserManager.FindByIdAsync(id);
        if (user == null) // NotFound
        {
          return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        // verifica se o usuário a ser excluído é o usuário logado
        if (user.UserName.Equals(User.Identity.GetUserName()))
        {
          TempData["Error"] = "Não é possível excluir o usuário logado.";
          return RedirectToAction("Index");
        }

        var result = await UserManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
          TempData["Error"] = "Erro ao excluir usuário.";
          return RedirectToAction("Index");
        }

        if (!string.IsNullOrEmpty(user.Img))
          FileManager.Delete(user.Img, Path.Combine(Server.MapPath("~/Uploads/Users")));

        TempData["Success"] = "Usuário Excluído.";
        return RedirectToAction("Index");
      }

      return View();
    }

    #endregion
  }
}
