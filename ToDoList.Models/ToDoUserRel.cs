using System;
using System.Collections.Generic;

namespace ToDoList.Models
{
    public partial class ToDoUserRel
    {
        public int Id { get; set; }
        public int ToDoId { get; set; }
        public int UserId { get; set; }

        public virtual ToDo ToDo { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
