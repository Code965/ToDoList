using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList.ViewModels
{
    public class UsersViewModel
    {
        public int id_user { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime dateBirth { get; set; }
        public string encryptPassword { get; set; }
        public string decryptPassword { get; set; }
    }
}