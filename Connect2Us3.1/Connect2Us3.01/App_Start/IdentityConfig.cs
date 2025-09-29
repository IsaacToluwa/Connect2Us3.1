using Connect2Us3._01.DAL;
using Connect2Us3._01.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Configuration;
using System.Linq;

namespace Connect2Us3._01
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            // Configure SMTP settings from web.config
            var smtpHost = ConfigurationManager.AppSettings["SmtpHost"] ?? "smtp.gmail.com";
                var smtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"] ?? "587");
                var smtpUsername = ConfigurationManager.AppSettings["SmtpUsername"];
                var smtpPassword = ConfigurationManager.AppSettings["SmtpPassword"];
                var smtpEnableSsl = bool.Parse(ConfigurationManager.AppSettings["SmtpEnableSsl"] ?? "true");
                var fromEmail = ConfigurationManager.AppSettings["FromEmail"] ?? smtpUsername;

                // For development/testing - log email details instead of sending
                System.Diagnostics.Trace.TraceInformation($"[DEV MODE] Email would be sent to: {message.Destination}");
                System.Diagnostics.Trace.TraceInformation($"[DEV MODE] Subject: {message.Subject}");
                System.Diagnostics.Trace.TraceInformation($"[DEV MODE] Body: {message.Body}");

                // Validate email configuration
                if (string.IsNullOrEmpty(smtpUsername) || string.IsNullOrEmpty(smtpPassword) || 
                    smtpUsername == "your-email@gmail.com" || smtpPassword == "your-app-password")
                {
                    System.Diagnostics.Trace.TraceWarning("Email credentials not configured properly. Using development mode - email not actually sent.");
                    
                    // In development mode, we'll simulate successful email delivery
                    // This allows the registration process to continue without actual email sending
                    await Task.Delay(100); // Simulate network delay
                    return;
                }

            using (var smtpClient = new SmtpClient(smtpHost, smtpPort))
            {
                smtpClient.EnableSsl = smtpEnableSsl;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(smtpUsername, smtpPassword);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail, "Connect2Us Bookstore"),
                    Subject = message.Subject,
                    Body = message.Body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(message.Destination);

                try
                {
                    await smtpClient.SendMailAsync(mailMessage);
                }
                catch (Exception ex)
                {
                    // Log the error (you might want to use a proper logging framework)
                    System.Diagnostics.Trace.TraceError($"Failed to send email: {ex.Message}");
                    throw;
                }
            }
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
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
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

    // Configure the role manager used in this application. RoleManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>()));
        }
    }
}