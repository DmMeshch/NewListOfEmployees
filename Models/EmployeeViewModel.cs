using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NewListOfEmployees.Models
{
    public class EmployeeViewModel
    {
        public int ID { get; set; }
        public string Fio { get; set; }
        public string Sex { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IFormFile Avatar { get; set; }
        
    }
}
