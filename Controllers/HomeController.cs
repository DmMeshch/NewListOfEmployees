using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewListOfEmployees.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;


namespace NewListOfEmployees.Controllers
{
   
    public class HomeController : Controller
    {
        private EmployeeContext db;
        public HomeController(EmployeeContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Employees.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employee)
        {
            Employee emp = new Employee
            {
                ID = employee.ID,
                Fio = employee.Fio,
                DateOfBirth = employee.DateOfBirth,
                PhoneNumber = employee.PhoneNumber,
                Sex = employee.Sex        
            };
            byte[] imageData = null;
            // считываем переданный файл в массив байтов
            using (var binaryReader = new BinaryReader(employee.Avatar.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)employee.Avatar.Length);
            }
            // установка массива байтов
            emp.AvatarByte = imageData;
        
        db.Employees.Add(emp);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Employee employee = await db.Employees.FirstOrDefaultAsync(p => p.ID == id);
                if (employee != null)
                    return View(employee);
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Employee employee = await db.Employees.FirstOrDefaultAsync(p => p.ID == id);
                if (employee != null)
                {
                    EmployeeViewModel emp = new EmployeeViewModel
                    {
                        ID = employee.ID,
                        Fio = employee.Fio,
                        DateOfBirth = employee.DateOfBirth,
                        PhoneNumber = employee.PhoneNumber,
                        Sex = employee.Sex
                    };
                    
                    return View(emp);
                }
               
            }

            return NotFound();

        }
        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeViewModel employee)
        {
            Employee emp = new Employee
            {
                ID = employee.ID,
                Fio = employee.Fio,
                DateOfBirth = employee.DateOfBirth,
                PhoneNumber = employee.PhoneNumber,
                Sex = employee.Sex
            };
            byte[] imageData =null;

            // считываем переданный файл в массив байтов
            using (var binaryReader = new BinaryReader(employee.Avatar.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)employee.Avatar.Length);
            }
            // установка массива байтов
            emp.AvatarByte = imageData;
            db.Employees.Update(emp);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Employee employee = await db.Employees.FirstOrDefaultAsync(p => p.ID == id);
                if (employee != null)
                    return View(employee);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Employee employee = new Employee { ID = id.Value };
                db.Entry(employee).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}

