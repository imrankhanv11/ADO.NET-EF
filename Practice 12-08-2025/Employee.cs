using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice12
{
    class Employees
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }

        public Employees(int id, string name, int age, string department)
        {
            Id = id;
            Name = name;
            Age = age;
            Department = department;
        }
    }
}
