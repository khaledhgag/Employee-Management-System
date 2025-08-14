﻿using Company.DAL.Entites;
using Company.Reposatory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompanyProject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepo _repo;
        public EmployeeController()
        {
            _repo = new EmployeeRepo();

        }

        public IActionResult Index()
        {
            var employee = _repo.GetAll();
            return View(employee);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var departments = _repo.GetAllDepartments(); 
            ViewBag.Departments = new SelectList(departments, "ID", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            try
            {
                var result = _repo.ADD(emp);
                if (result > 0)
                {
                    TempData["SuccessMessage"] = "Employee added successfully!";
                    return RedirectToAction("Index");
                }
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An unexpected error occurred: " + ex.Message);
            }
            return View(emp);
        }
        public IActionResult Details(int id)
        {
            var employee = _repo.GetbyID(id);
            if (employee is null)
            {
                return NotFound();
            }
            return View(employee);

        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var employee = _repo.GetbyID(id);
            if (employee is null)
            {
                return NotFound();
            }
            var departments = _repo.GetAllDepartments();
            ViewBag.Departments = new SelectList(departments, "ID", "Name", employee.DepartmentId);  
            return View(employee);
        }
        [HttpPost]
        public IActionResult Update(Employee emp)
        {
            var result = _repo.update(emp);
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            return View(result);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var emplyee = _repo.GetbyID(id);
            if (emplyee is null)
            {
                return NotFound();
            }
            return View(emplyee);

        }
        [HttpPost]
        public IActionResult Delete(Employee emplyee)
        {
            var result = _repo.DELETE(emplyee);
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            return View(result);
        }

    }
}
