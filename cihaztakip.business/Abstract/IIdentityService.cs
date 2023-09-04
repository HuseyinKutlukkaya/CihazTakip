using cihaztakip.entity.ViewModels;

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cihaztakip.business.Abstract
{
    public interface IIdentityService
    {
        public  Task<Result> Login(LoginModel model);
        public  Task<Result> Register(RegisterModel model);
        public Task LogOut();
        public Task<UserListViewModel> GetAllUsersWithRoles();
        public Task<UserDetailsModel> GetUserDetails(string id);
        public Task<string> GetRoleOfUser(string id);
        public Task<List<IdentityRole>> GetRoles();

        public Task<Result> UpdateUser(UserDetailsModel model);
        public Task<Result> CreateNewUser(NewUserModel model);
        public Task<Result> AddRoleToUser(string userId,string role);
    }
}
