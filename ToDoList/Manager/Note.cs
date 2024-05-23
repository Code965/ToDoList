using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList.Manager
{
    public class Note
    {
        public int Notes_Id { get; set; }
        public string Title { get; set; }
        public DateTime DateNotes { get; set; }
        public string DescriptionNotes { get; set; }
        public int CategoryNotes { get; set; }

    }
}