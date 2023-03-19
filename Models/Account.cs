using System;
using System.Collections.Generic;

namespace TinTucCongNghe.Models
{
    public partial class Account
    {
        public int AccountId { get; set; }
        public string? UseName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? RoleId { get; set; }
        public bool IsActive { get; set; }

        public virtual Role? Role { get; set; }
    }
}
