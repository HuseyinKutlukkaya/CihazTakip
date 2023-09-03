using cihaztakip.business.Abstract;
using cihaztakip.data.Abstract;
using cihaztakip.entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cihaztakip.business.Concrete
{
    public class CustomUserManager : UserManager<User>
    {
        // Inject required services in the constructor
        public CustomUserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        // Override the DeleteAsync method to add custom behavior
        public override async Task<IdentityResult> DeleteAsync(User user)
        {
            // Add your custom logic here before deleting the user




            // Call the base DeleteAsync method to perform the actual deletion
            var result = await base.DeleteAsync(user);

            // Add your custom logic here after deleting the user

            return result;
        }
    }

}
