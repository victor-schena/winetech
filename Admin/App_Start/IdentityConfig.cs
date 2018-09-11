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
      await configSendGridasync(message);
      //await Execute();
      //return Task.FromResult(0);
    }
    static async Task Execute()
    {
      var apiKey = Environment.GetEnvironmentVariable("sendGridApiKey");
      var client = new SendGridClient(apiKey);
      var from = new EmailAddress("winetechsmtp@gmail.com", "Vinhos Schemberg");
      var subject = "Sending with SendGrid is Fun";
      var to = new EmailAddress("victorschena5@gmail.com", "Victor");
      var plainTextContent = "and easy to do anywhere, even with C#";
      var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
      var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
      var response = await client.SendEmailAsync(msg);
    }
    private async Task configSendGridasync(IdentityMessage message)
    {
      try
      {
        var client = new SendGridClient("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");
        var from = new EmailAddress("winetechsmtp@gmail.com", "Winetech");
        var subject = message.Subject;
        var to = new EmailAddress(message.Destination, message.Destination);
        var plainTextContent = message.Body;
        var htmlContent = message.Body;
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);
        //var myMessage = new SendGrid.SendGridMessage();
        //myMessage.AddTo(message.Destination);
        //myMessage.From = new System.Net.Mail.MailAddress("winetechsmtp@gmail.com", "Vinhos Schemberg");
        //myMessage.Subject = message.Subject;
        //myMessage.Text = message.Body;
        //myMessage.Html = message.Body;
        //var credentials = new NetworkCredential(
        //          ConfigurationManager.AppSettings["mailAccount"],
        //           ConfigurationManager.AppSettings["mailPassword"]
        //           );
        //// Create a Web transport for sending email.
        //var transportWeb = new Web(credentials);
        //// Send the email.
        //if (transportWeb != null)
        //{
        //  await transportWeb.DeliverAsync(myMessage);
        //}
        //else
        //{
        //  Trace.TraceError("Failed to create Web transport.");
        //  await Task.FromResult(0);
        //}
      }
      catch (Exception ex)
      {

        throw ex;
      }
      
    }
    //public Task sendMail(IdentityMessage message)
    //{
    //  //#region formatter
    //  //string text = string.Format("Please click on this link to {0}: {1}", message.Subject, message.Body);
    //  //string html = "Please confirm your account by clicking this link: <a href=\"" + message.Body + "\">link</a><br/>";

    //  //html += HttpUtility.HtmlEncode(@"Or click on the copy the following link on the browser:" + message.Body);
    //  //#endregion

    //  MailMessage msg = new MailMessage();
    //  msg.From = new MailAddress("winetechsmtp@gmail.com");
    //  msg.To.Add(new MailAddress(message.Destination));
    //  msg.Subject = message.Subject;
    //  msg.Body = message.Body;
    //  //msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
    //  //msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

    //  SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(465));
    // //SmtpClient smtpClient = new SmtpClient();

    //  System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("winetechsmtp@gmail.com", "Senha@12");
    //  smtpClient.Credentials = credentials;
    //  smtpClient.EnableSsl = true;
    //  smtpClient.UseDefaultCredentials = false;
    //  smtpClient.Send(msg);
    //  return Task.FromResult(0);
    //}
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
