using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList.ViewModels
{
    public class ActivityViewModels
    {
        public string name { get; set; }
        public string description { get; set; }
        public DateTime dateActivity { get; set; }
        public int priority { get; set; }
        public int category { get; set; }
    }
}