using NoiThatAdmin.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoiThatAdmin.Models
{
    public class DataViewModel
    {
    }

    public class UserViewModel
    {
        public List<User> users { get; set; }
        public List<Role> roles { get; set; }
        public List<Permission> permissions { get; set; }
    }

    public class UserRoleViewModel
    {
        public List<DataRole> Allow { get; set; }
        public List<DataRole> Available { get; set; }
    }

    public class DataRole
    {
        public int? RoleID { get; set; }
        public string RoleName { get; set; }
    }
    public class DataPermission
    {
        public int? PermissionID { get; set; }
        public string PermissonName { get; set; }
    }
    public class UpdateRoleViewModel
    {
        public List<DataPermission> Allow { get; set; }
        public List<DataPermission> Available { get; set; }
    }
}