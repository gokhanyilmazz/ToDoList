using System;
using System.Collections.Generic;

namespace ToDoList.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public int UserTypeId { get; set; }
        public string FullName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string PassWord { get; set; } = null!;
        public string? ProfilePicture { get; set; }
        public bool IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual UserType UserType { get; set; } = null!;
    }
}
