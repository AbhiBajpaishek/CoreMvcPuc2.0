using CoreMvcPuc2.Model;
using CoreMvcPuc2.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcPuc2Controller
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(IEmployeeRepository employeeRepository, IHostingEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            _hostingEnvironment = hostingEnvironment;

        }

        public ViewResult Index()
        {
            var model = _employeeRepository.GetAllEmployees();
            return View(model);
        }

        public ViewResult Details(int? id)
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel
            {
                Employee = _employeeRepository.GetEmployee(id ?? 1),
                PageTitle = "Employee Details"
            };
            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                existingPhotoPath = employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                string uniqueFileName;
                if (model.Photo != null)
                {
                    if (model.existingPhotoPath != null)
                    {
                        string pathToDelete = Path.Combine(_hostingEnvironment.WebRootPath + "Images" + model.existingPhotoPath);
                        System.IO.File.Delete(pathToDelete);
                    }
                    uniqueFileName = ProcessUploadedPhoto(model);
                    employee.PhotoPath = uniqueFileName;
                }
                else
                {
                    employee.PhotoPath = model.existingPhotoPath;
                }
                
                _employeeRepository.UpdateEmployee(employee);
                return RedirectToAction("details", new { id = model.Id });
            }
            else
                return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if(model.Photo!=null)
                {
                    uniqueFileName = ProcessUploadedPhoto(model);
                }
                Employee emp = _employeeRepository.AddEmployee(
                    new Employee
                    {
                        Name = model.Name,
                        Department = model.Department,
                        Email = model.Email,
                        PhotoPath = uniqueFileName
                    });
                return RedirectToAction("details", new { id = emp.Id });
            }
            else
                return View();
        }

        private string ProcessUploadedPhoto(EmployeeCreateViewModel model)
        {
            string uniqueFileName;
            string folderName = Path.Combine(_hostingEnvironment.WebRootPath, "images");
            uniqueFileName = Guid.NewGuid() + "_" + model.Photo.FileName;
            string completePath = Path.Combine(folderName, uniqueFileName);
            using (FileStream fileStream= new FileStream(completePath, FileMode.Create))
            {
                model.Photo.CopyTo(fileStream);
            }
            return uniqueFileName;
        }
    }
}
