using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcPuc2.Model
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;
        
        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>
            {
                new Employee(){Id=1,Name="Abhi",Email="abhi@gmail.com",Department=Dept.HR},
                new Employee(){Id=2,Name="Abhinav",Email="navv@live.in",Department=Dept.Finance},
                new Employee(){Id=3,Name="Amit",Email="amit@yahoo.com",Department=Dept.IT}
            };
        }

        public Employee AddEmployee(Employee employee)
        {
            employee.Id = _employeeList.Max((emp) => emp.Id)+1;
            _employeeList.Add(employee);
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == Id);
        }
    }
}
