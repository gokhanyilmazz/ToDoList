using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public partial class ToDo
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
    
        public DateTime? EndDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public int StatusId { get; set; }

        public virtual Status Status { get; set; } = null!;
    }
}
