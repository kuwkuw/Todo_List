using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TodosList.DataAccess.Entities
{
    public class TodoCategory
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual List<Todo> Todos { get; set; }
    }
}