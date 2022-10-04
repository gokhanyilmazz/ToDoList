using System;
using System.Collections.Generic;

namespace ToDoList.Models
{
    public partial class Status
    {
        public Status()
        {
            ToDos = new HashSet<ToDo>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<ToDo> ToDos { get; set; }
    }
}
