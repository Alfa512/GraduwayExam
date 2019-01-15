using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using GraduwayExam.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GraduwayExam.Data
{
    //public class UserManager : UserManager<User>
    //{
    //    public UserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor,
    //        IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators,
    //        IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer,
    //        IdentityErrorDescriber errors, IEnumerable<IUserTokenProvider<User>> tokenProviders,
    //        Logger logger)
    //        : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors,
    //            tokenProviders, logger)
    //    {
    //        this.Store = store;
    //    }

    //    public static UserManager Create(IdentityFactoryOptions<UserManager> options, IOwinContext context)
    //    {
    //        var manager = new UserManager(new UserStore<User>(context.Get<ApplicationContext>()));
    //        // Configure validation logic for usernames
    //        manager.UserValidator = new Microsoft.AspNetCore.Identity.UserValidator<User>(manager)
    //        {
    //            AllowOnlyAlphanumericUserNames = true,
    //            RequireUniqueEmail = true
    //        };

    //        // Configure validation logic for passwords
    //        manager.PasswordValidators.Add(new PasswordValidator<User>()
    //        {
    //            /*
    //            RequiredLength = 6,
    //            RequireNonLetterOrDigit = false,
    //            RequireDigit = true,
    //            RequireLowercase = false,
    //            RequireUppercase = false,*/
    //        });

    //        // Configure user lockout defaults
    //        manager.UserLockoutEnabledByDefault = true;
    //        manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
    //        manager.MaxFailedAccessAttemptsBeforeLockout = 5;

    //        // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
    //        // You can write your own provider and plug it in here.
    //        manager.RegisterTwoFactorProvider("Phone Code", new Microsoft.AspNetCore.Identity.PhoneNumberTokenProvider<User>
    //        {
    //            MessageFormat = "Your security code is {0}"
    //        });
    //        manager.RegisterTwoFactorProvider("Email Code", new Microsoft.AspNetCore.Identity.EmailTokenProvider<User>
    //        {
    //            Subject = "Security Code",
    //            BodyFormat = "Your security code is {0}"
    //        });
    //        manager.EmailService = new EmailService();
    //        manager.SmsService = new SmsService();
    //        var dataProtectionProvider = options.DataProtectionProvider;
    //        if (dataProtectionProvider != null)
    //        {
    //            manager.UserTokenProvider =
    //                new DataProtectorTokenProvider<User>(dataProtectionProvider.Create("ASP.NET Identity"));
    //        }
    //        return manager;
    //    }
    //}
    
    // Configure the application sign-in manager which is used in this application.
    //public class ApplicationSignInManager : SignInManager<User, string>
    //{
    //    public ApplicationSignInManager(UserManager<User> userManager, IAuthenticationManager authenticationManager)
    //        : base(userManager, authenticationManager)
    //    {
    //    }

    //    public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
    //    {
    //        return user.GenerateUserIdentityAsync((UserManager<User>)UserManager);
    //    }

    //    public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
    //    {
    //        return new ApplicationSignInManager(context.GetUserManager<UserManager>(), context.Authentication);
    //    }
    //}
}