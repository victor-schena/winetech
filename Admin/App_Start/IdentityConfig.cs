using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Admin.Models;
using Admin.DataContexts;
using SendGrid;
using System.Net;
using System.Configuration;
using System.Diagnostics;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Net.Mime;

namespace Admin
{
  public class EmailService : IIdentityMessageService
  {
    public async Task SendAsync(IdentityMessage message)
    {
      // Plug in your email service here to send an email.
      await sendMail(message);
      //await Execute();
      //return Task.FromResult(0);
    }
   
    public Task sendMail(IdentityMessage message)
    {
      MailMessage mail = new MailMessage();
      mail.From = new System.Net.Mail.MailAddress("apps@xxxx.com");

      // The important part -- configuring the SMTP client
      SmtpClient smtp = new SmtpClient();
      smtp.Port = 587;   // [1] You can try with 465 also, I always used 587 and got success
      smtp.EnableSsl = true;
      smtp.DeliveryMethod = SmtpDeliveryMethod.Network; // [2] Added this
      smtp.UseDefaultCredentials = false; // [3] Changed this
      smtp.Credentials = new NetworkCredential("wine.hurt@gmail.com", "Senha@12");  // [4] Added this. Note, first parameter is NOT string.
      smtp.Host = "smtp.gmail.com";

      //recipient address
      mail.To.Add(new MailAddress(message.Destination));

      //Formatted mail body
      mail.IsBodyHtml = true;
     

      mail.Body = message.Body;
      smtp.Send(mail);
      return Task.FromResult(0);
    }
  }

  public class SmsService : IIdentityMessageService
  {
    public Task SendAsync(IdentityMessage message)
    {
      // Plug in your SMS service here to send a text message.
      return Task.FromResult(0);
    }
  }

  // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
  public class ApplicationUserManager : UserManager<ApplicationUser>
  {
    public ApplicationUserManager(IUserStore<ApplicationUser> store)
      : base(store)
    {
    }

    public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
    {
      var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<IdentityDb>()));
      // Configure validation logic for usernames
      manager.UserValidator = new UserValidator<ApplicationUser>(manager)
      {
        AllowOnlyAlphanumericUserNames = false,
        RequireUniqueEmail = true
      };

      // Configure validation logic for passwords
      manager.PasswordValidator = new PasswordValidator
      {
        RequiredLength = 6,
        RequireNonLetterOrDigit = true,
        RequireDigit = true,
        RequireLowercase = true,
        RequireUppercase = true,
      };

      // Configure user lockout defaults
      manager.UserLockoutEnabledByDefault = true;
      manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
      manager.MaxFailedAccessAttemptsBeforeLockout = 5;

      // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
      // You can write your own provider and plug it in here.
      manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
      {
        MessageFormat = "Your security code is {0}"
      });
      manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
      {
        Subject = "Security Code",
        BodyFormat = "Your security code is {0}"
      });
      manager.EmailService = new EmailService();
      manager.SmsService = new SmsService();
      var dataProtectionProvider = options.DataProtectionProvider;
      if (dataProtectionProvider != null)
      {
        manager.UserTokenProvider =
            new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
      }
      return manager;
    }
  }

  // Configure the RoleManager used in the application. RoleManager is defined in the ASP.NET Identity core assembly
  public class ApplicationRoleManager : RoleManager<ApplicationRole>
  {
    public ApplicationRoleManager(IRoleStore<ApplicationRole, string> roleStore)
      : base(roleStore)
    {
    }

    public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
    {
      return new ApplicationRoleManager(new RoleStore<ApplicationRole>(context.Get<IdentityDb>()));
    }
  }

  // Configure the application sign-in manager which is used in this application.
  public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
  {
    public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
      : base(userManager, authenticationManager)
    {
    }

    public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
    {
      return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
    }

    public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
    {
      return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
    }
  }
}
