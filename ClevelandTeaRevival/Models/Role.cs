using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ClevelandTeaRevival.Models
{
    public class Role
    {
        public string RoleName { get; set; }
        public int RoleId { get; set; }
    }
}
