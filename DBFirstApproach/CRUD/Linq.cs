using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
                Console.WriteLine("| 2.                 Where      |");
                Console.WriteLine("| 3.                 Orders     |");
                Console.WriteLine("| 4.                 Aggregate  |");
                Console.WriteLine("| 5.                 EXIT       |");
                Console.WriteLine("---------------------------------");

                Console.Write("Enter the Code to Run : ");
                string code = Console.ReadLine();

                switch (code)
                {
                    case "1":
                        select();
                        break;
                    case "2":
                        where();
                        break;
                    case "3":
                        order();
                        break;
                    case "4":
                        aggMethod();
                        break;
                    case "5":
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
                // basic
                IEnumerable<string> studentsSlect = dbcontext.Students.Select(s => s.Name);

                

                Console.WriteLine("Student Names :");
                foreach (var student in studentsSlect)
                {
                    Console.WriteLine(student + " ");
                }
                Console.WriteLine();


                // with more than 1 values
                IEnumerable<Student> studentsSlectMany = dbcontext.Students.AsEnumerable().Select(s => new Student
                {
                    Name = s.Name,
                    Age = s.Age,
                    Email = s.Email,
                });

                
                Console.WriteLine("Student with details :");
                foreach (var student in studentsSlectMany)
                {
                    Console.WriteLine(student.Name + " " + student.Age + " " + student.Email);
                }
                Console.WriteLine();

                // with Foreingkey 
                Console.WriteLine("Student and their Department :");
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

                // with forengkey with join
                var result2 = from s in dbcontext.Students
                             join d in dbcontext.Departments
                             on s.DepartmentID equals d.DepartmentID
                             select new
                             {
                                 StudentName = s.Name,
                                 DepartmentName = d.DepartmentName
                             };

                Console.WriteLine();
                Console.WriteLine("student with their Deparment Name :");
                foreach (var item in result2)
                {
                    Console.WriteLine($"{item.StudentName} - {item.DepartmentName}");
                }
            }

            // -------------------------------------------------
            // select many

            using(var dbcontext = new StudentsEntities())
            {
                // select many ---> one - many
                var students = dbcontext.Departments
                                .SelectMany(d => d.Students, (d, s) => new
                                {
                                    DepartmentName = d.DepartmentName,
                                    StudentName = s.Name
                                })
                                .ToList();

                Console.WriteLine();
                Console.WriteLine("Department With Students :");
                foreach (var item in students)
                {
                    Console.WriteLine(item.DepartmentName+" - "+item.StudentName);
                }

                Console.WriteLine();

                // select --- for array store
                var students2 = dbcontext.Departments
                                .Select(d => new
                                {
                                    DepartmentName = d.DepartmentName,
                                    StudentName = d.Students
                                })
                                .ToList();

                Console.WriteLine("Department and their Students:");
                foreach (var dept in students2)
                {
                    Console.WriteLine("Department: " + dept.DepartmentName);

                    foreach (var student in dept.StudentName)  
                    {
                        Console.WriteLine("   Student: " + student.Name);
                    }
                }
            }
        }

        public void where()
        {
            using(var dbcontext = new StudentsEntities())
            {
                var departments = dbcontext.Departments.Where(d => d.Students.Any(s => s.Age > 22));

                Console.WriteLine("Department With students who have age more than 22 atleast 1");
                foreach(var  dept in departments)
                {
                    Console.WriteLine(dept.DepartmentID+", "+dept.DepartmentName);
                }
                Console.WriteLine();

                var students = dbcontext.Students
                                        .Where(s => s.Age > 20)
                                        .Select(s=> new  { s.Name, s.Age })
                                        .ToList();

                Console.WriteLine("Students with age more than 20 :");
                foreach(var student in students)
                {
                    Console.WriteLine(student.Name+", "+student.Age);
                }
                Console.WriteLine();

                var deparmentvalue = dbcontext.Departments.Where(d => d.Students.Any(s => s.Age > 20) && d.DepartmentID == 1 || d.DepartmentID == 2). Select(s=> s.DepartmentName);

                Console.WriteLine("Department with student more than age 20 and code is 1 or 2");
                foreach( var department in deparmentvalue)
                {
                    Console.WriteLine(department);
                }
                Console.WriteLine();

                var ids = new List<int> { 1, 3, 5 };
                var students2 = dbcontext.Students
                    .Where(s => ids.Contains(s.StudentID))
                    .ToList();

                Console.WriteLine("Student name with contains id :");
                foreach(var stu  in students2)
                {
                    Console.WriteLine(stu.Name);
                }
                Console.WriteLine();

                var students3 = dbcontext.Students
                                        .Where(s => s.Name.Contains("an") || s.Name.EndsWith("a"))
                                        .ToList();

                Console.WriteLine("Student name with like :");
                foreach(var stu in students3)
                {
                    Console.WriteLine(stu.Name);
                }
                Console.WriteLine();

                var department3 = dbcontext.Departments.Where(s => s.Students
                                                                .Where(m => m.Age > 20)
                                                                .Count() > 2);

                //var department3 = dbcontext.Departments
                //                            .Where(s => s.Students.Count(m => m.Age > 20) > 2);

                Console.WriteLine("Where multiple : ");
                foreach(var dept  in department3)
                {
                    Console.WriteLine(dept.DepartmentName);
                }


            }
        }

        public void order()
        {
            using(var dbcontext = new StudentsEntities())
            {
                var student = dbcontext.Students.OrderBy(m => m.Age).Select(s => new
                {
                    Name = s.Name,
                    Age = s.Age,
                });

                int C = 1;
                foreach(var stud  in student)
                {
                    Console.WriteLine($"{C++} : {stud.Name}, {stud.Age}");
                }
                Console.WriteLine();

                var student2 = dbcontext.Students.OrderBy(m => m.Age).ThenByDescending(s=> s.Name).Select(s => new
                {
                    Name = s.Name,
                    Age = s.Age,
                });

                int C2 = 1;
                foreach (var stud in student2)
                {
                    Console.WriteLine($"{C2++} : {stud.Name}, {stud.Age}");
                }
            }
        }

        public void aggMethod()
        {
            using( var dbcontext = new StudentsEntities())
            {
                var studentAge = dbcontext.Students.GroupBy(e => e.DepartmentID)
                                                    .Select(s => new
                                                    {
                                                        ID = s.Key,
                                                        Count = s.Count(),
                                                        Max = s.Max(max=> max.Age),
                                                        Min = s.Min(min => min.Age),
                                                        Avg = s.Average(avg => avg.Age)
                                                    });

                Console.WriteLine("Values :");
                foreach( var student in studentAge)
                {
                    Console.WriteLine($"Id : {student.ID}, Count : {student.Count}, Max : {student.Max}, Min : {student.Min}, Average : {student.Avg}");

                }
                Console.WriteLine();



                var deptSummary = dbcontext.Departments
                           .Select(d => new
                           {
                               DepartmentName = d.DepartmentName,
                               StudentCount = d.Students.Count(),
                               MaxAge = d.Students.Max(s => s.Age)
                           })
                           .ToList();
                

                foreach( var student in deptSummary)
                {
                    Console.WriteLine($"DepartmentName : {student.DepartmentName}, StudentCount : {student.StudentCount}, MaxAge : {student.MaxAge}");
                }
                Console.WriteLine() ;

                var studentAgeGroup = dbcontext.Departments
                                        .Select(d => new
                                        {
                                            DepartmentName = d.DepartmentName,
                                            AvgAge = d.Students.Average(s => s.Age)
                                        })
                                        .ToList();

                foreach (var item in studentAgeGroup)
                {
                    Console.WriteLine($"{item.DepartmentName} - Average Age: {item.AvgAge}");
                }

            }
        }
    }
}
    