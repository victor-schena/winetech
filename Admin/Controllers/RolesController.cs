using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using Admin.Models;
using Admin.DataContexts;
using Admin.Tables;
using System.Data.Entity;
using Admin.Attributes;

namespace Admin.Controllers
{
  [Authorize]
  [Restrict]
  public class RolesController : Controller
  {
    #region Constructor

    public RolesController()
    {
      _db = new IdentityDb();
    }

    public RolesController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
    {
      _db = new IdentityDb();
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
      set
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

    private IdentityDb _db;

    #endregion

    #region Actions

    [Route("roles")]
    public ActionResult Index()
    {
      return View(RoleManager.Roles.OrderBy(r => r.Name));
    }

    [Route("roles/details/{id}")]
    public async Task<ActionResult> Details(string id)
    {
      if (id == null) // BadRequest
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      var role = await RoleManager.FindByIdAsync(id);
      if (role == null) // NotFound
      {
        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
      }

      // Cria uma lista para armazenar os usuários
      var users = new List<ApplicationUser>();

      // Recupera apenas os usuários que estão na role
      foreach (var user in UserManager.Users.ToList())
      {
        if (await UserManager.IsInRoleAsync(user.Id, role.Name))
        {
          users.Add(user);
        }
      }

      ViewBag.Users = users;
      ViewBag.Role = role.Name;

      return View(role);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("roles/create")]
    public ActionResult Create()
    {
      return View();
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("roles/create")]
    public async Task<ActionResult> Create(RoleViewModel roleViewModel)
    {
      if (ModelState.IsValid)
      {
        var role = new ApplicationRole(roleViewModel.Name);

        // Seta o campo description
        role.Description = roleViewModel.Description;

        var roleresult = await RoleManager.CreateAsync(role);
        if (!roleresult.Succeeded)
        {
          ModelState.AddModelError("", "O Perfil já existe.");
          return View();
        }

        TempData["Success"] = "Perfil Salvo.";
        return RedirectToAction("Index");
      }

      return View();
    }

    [HttpGet]
    [Route("roles/edit/{id}")]
    public async Task<ActionResult> Edit(string id)
    {
      if (id == null) // BadRequest
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      var role = await RoleManager.FindByIdAsync(id);
      if (role == null) // NotFound
      {
        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
      }

      RoleViewModel roleModel = new RoleViewModel { Id = role.Id, Name = role.Name };
      roleModel.Description = role.Description;

      return View(roleModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("roles/edit/{id}")]
    public async Task<ActionResult> EditPost([Bind(Include = "Name,Id,Description")] RoleViewModel roleModel)
    {
      if (ModelState.IsValid)
      {
        var role = await RoleManager.FindByIdAsync(roleModel.Id);
        role.Name = roleModel.Name;
        role.Description = roleModel.Description;

        await RoleManager.UpdateAsync(role);

        TempData["Success"] = "Perfil Editado.";
        return RedirectToAction("Index");
      }

      return View("Edit");
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Route("roles/delete")]
    public async Task<ActionResult> DeleteConfirmed(string id)
    {
      if (ModelState.IsValid)
      {
        if (id == null) // BadRequest
        {
          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        var role = await RoleManager.FindByIdAsync(id);
        if (role == null) // NotFound
        {
          return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        IdentityResult result;
        result = await RoleManager.DeleteAsync(role);
        if (!result.Succeeded)
        {
          TempData["Success"] = "Erro ao excluir perfil.";
          return RedirectToAction("Index");
        }

        TempData["Success"] = "Perfil Excluído.";
        return RedirectToAction("Index");
      }

      return View();
    }

    [HttpGet]
    [Route("roles/credentials/{id}")]
    public async Task<ActionResult> Credentials(string id)
    {
      if (id == null) // BadRequest
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      var role = await RoleManager.FindByIdAsync(id);
      if (role == null) // NotFound
      {
        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
      }

      RoleViewModel model = new RoleViewModel
      {
        Id = role.Id,
        Name = role.Name,
        Description = role.Description
      };

      List<int> roleCredId = role.Credentials.Select(c => c.Id).ToList();
      ViewBag.Credentials = _db.Credentials.ToList().OrderBy(c => c.Controller).ThenBy(c => c.Action).Select(x => new SelectListItem
      {
        Text = string.Format("{0}  {1}", x.Descr, (x.Param != null ? " - " + x.Param : "")),
        Value = x.Id.ToString(),
        Selected = roleCredId.Contains(x.Id)
      });

      return View(model);
    }

    [HttpPost, ActionName("Credentials")]
    [ValidateAntiForgeryToken]
    [Route("roles/credentials/{id}")]
    public async Task<ActionResult> CredentialsPost([Bind(Include = "Name,Id,Description")] RoleViewModel model, params int[] selectedCredentials)
    {
      if (model == null || selectedCredentials == null || selectedCredentials.Count() == 0) // BadRequest
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      // Adiciona credenciais
      ApplicationRole entityRole = _db.ApplicationRoles.Include(c => c.Credentials).Where(r => r.Id == model.Id).Single();

      if (entityRole == null) // NotFound
      {
        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
      }

      foreach (int credId in selectedCredentials)
      {
        Credential credential = _db.Credentials.Where(c => c.Id == credId).Single();
        entityRole.Credentials.Add(credential);
      }
      _db.Entry(entityRole).State = EntityState.Modified;
      _db.SaveChanges();

      // Remove credenciais
      var role = await RoleManager.FindByIdAsync(model.Id);
      List<int> removedCredentials = role.Credentials.Select(c => c.Id).ToList().Except(selectedCredentials).ToList();
      foreach (int credId in removedCredentials)
      {
        Credential credential = role.Credentials.Where(c => c.Id == credId).Single();
        role.Credentials.Remove(credential);
      }

      TempData["Success"] = string.Format("Credenciais de {0} modificadas com sucesso.", model.Name);

      await RoleManager.UpdateAsync(role);
      return View("Index", RoleManager.Roles.OrderBy(r => r.Name));
    }

    #endregion
  }
}
