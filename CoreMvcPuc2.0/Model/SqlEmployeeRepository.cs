using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcPuc2.Model
{
    public class SqlEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext context;

        public SqlEmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Employee AddEmployee(Employee employee)
        {
            context.employees.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public Employee DeleteEmployee(int id)
        {
            var employee=context.employees.Find(id);
            if(employee!=null)
            {
                context.employees.Remove(employee);
                context.SaveChanges();
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return context.employees;
        }

        public Employee GetEmployee(int Id)
        {
            return context.employees.Find(Id);
        }

        public Employee UpdateEmployee(Employee changedEmployee)
        {
            var employee= context.employees.Attach(changedEmployee);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return changedEmployee;
        }
    }
}
