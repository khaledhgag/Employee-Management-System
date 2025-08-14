using Company.DAL.context;
using Company.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Reposatory
{
	public class EmployeeRepo
	{
		private readonly CompanyDBContext _context;
		public EmployeeRepo()
		{
			_context = new CompanyDBContext();

		}
		public IEnumerable<Employee> GetAll()
		{

            return _context.Employees.Include(e => e.Departments).ToList();


        }
        public IEnumerable<Department> GetAllDepartments()
        {
            return _context.Departments.ToList(); 
        }
        public Employee GetbyID(int id)
		{
			return _context.Employees.Find(id);

		}
		public int ADD(Employee emp)
		{
            var departmentExists = _context.Departments.Any(d => d.ID == emp.DepartmentId);

            if (!departmentExists)
            {
                throw new InvalidOperationException("The specified DepartmentId does not exist.");
            }
            _context.Employees.Add(emp);
			return _context.SaveChanges();
		}

		public int DELETE(Employee emp)
		{

			_context.Employees.Remove(emp);
			return (_context.SaveChanges());

		}
		public int update(Employee emp)
		{

			_context.Employees.Update(emp);
			return (_context.SaveChanges());

		}
	}
}
