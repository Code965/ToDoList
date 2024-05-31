using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList.ViewModels
{
    public class NotesViewModels
    {
        public string title { get; set; }
        public DateTime DateNotes { get; set; }
        public string DescriptionNotes { get; set; }
        public int CategoryNotes { get; set; }
    }
}