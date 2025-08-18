using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFirstApproach
{
    internal class Linq
    {
        public void linqMethod()
        {
            while (true)
            {

                Console.WriteLine("---------------------------------");
                Console.WriteLine("-------------| Menu |------------");
                Console.WriteLine("---------------------------------");
                Console.WriteLine("| 1.                 Select     |");
                Console.WriteLine("| 2.                 -  |");
                Console.WriteLine("| 3.                 -       |");
                Console.WriteLine("| 4.                 EXIT       |");
                Console.WriteLine("---------------------------------");

                Console.Write("Enter the Code to Run : ");
                string code = Console.ReadLine();

                switch (code)
                {
                    case "1":
                        select();
                        break;
                    case "2":

                        break;
                    case "3":

                        break;
                    case "4":
                        Console.WriteLine("Thank you");
                        return;
                    default:
                        Console.WriteLine("Try again");
                        break;
                }
            }
        }

        public void select()
        {
            // select
            using (var dbcontext = new StudentsEntities())
            {
                IEnumerable<string> studentsSlect = dbcontext.Students.Select(s => s.Name);

                IEnumerable<Student> studentsSlectMany = dbcontext.Students.AsEnumerable().Select(s => new Student
                {
                    Name = s.Name,
                    Age = s.Age,
                    Email = s.Email,
                });

                foreach (var student in studentsSlect)
                {
                    Console.WriteLine(student + " ");
                }

                foreach (var student in studentsSlectMany)
                {
                    Console.WriteLine(student.Name + " " + student.Age + " " + student.Email);
                }


                var result = dbcontext.Students
    .Select(s => new
    {
        StudentName = s.Name,
        DepartmentName = s.Department.DepartmentName
    });

                foreach (var item in result)
                {
                    Console.WriteLine($"{item.StudentName} - {item.DepartmentName}");
                }

            }

            // select many
        }
    }
}
