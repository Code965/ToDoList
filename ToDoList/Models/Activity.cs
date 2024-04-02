using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList.Models
{
    public class Activity
    {
        public string name { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }
        public int priority { get; set; }
        public int category { get; set; }


    }
}