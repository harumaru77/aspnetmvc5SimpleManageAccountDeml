using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class AccountManager
    {
        List<UserInfo> _userInfos;
        List<RoleInfo> _roleInfos;

        public AccountManager()
        {
            _userInfos = new List<UserInfo>()
            {
                new UserInfo(){ UserId = 1, UserName = "testuser01", UserPassword = "1234" },
                new UserInfo(){ UserId = 2, UserName = "testuser02", UserPassword = "1234" },
            };

            _roleInfos = new List<RoleInfo>()
            {
                new RoleInfo(){ RoldId = 1, RoleName = "Admin", UserName = "testuser01"},
                new RoleInfo(){ RoldId = 2, RoleName = "Customer", UserName = "testuser01"},
                new RoleInfo(){ RoldId = 3, RoleName = "Customer", UserName = "testuser02"},
            };
        }

        public UserInfo Login(string userName, string userPassword)
        {
            return _userInfos.Find(m => m.UserName == userName && m.UserPassword == userPassword);
        }

        public string[] GetRoles(string userName)
        {
            List<RoleInfo> roleInfos = _roleInfos.FindAll(m => m.UserName == userName);
            List<string> roleNames = new List<string>();

            foreach(RoleInfo roleInfo in roleInfos)
            {
                roleNames.Add(roleInfo.RoleName);
            }

            return roleNames.ToArray();
            
        }
    }
}