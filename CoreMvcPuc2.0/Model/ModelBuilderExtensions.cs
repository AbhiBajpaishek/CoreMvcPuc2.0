using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcPuc2.Model
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData
            (
                new Employee
                {
                    Id = 1,
                    Name = "Sam",
                    Email = "sam@gmail.com",
                    Department = Dept.Admin
                },
               new Employee
               {
                   Id = 2,
                   Name = "John",
                   Email = "John@gmail.com",
                   Department = Dept.HR
               }
            );
        }

    }
}
