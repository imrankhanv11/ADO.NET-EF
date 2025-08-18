using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DbFirstApproach
{
    internal class Bulk
    {
        InputValidation input = new InputValidation();
        public async Task crudmethod()
        {

            while (true)
            {

                Console.WriteLine("----------------------------------------");
                Console.WriteLine("----------------| Menu |----------------");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("| 1.                 AddSTudent        |");
                Console.WriteLine("| 2.                 DeleteStudent     |");
                Console.WriteLine("| 3.                 UpdateStudent     |");
                Console.WriteLine("| 4.                 Exit              |");
                Console.WriteLine("----------------------------------------");

                Console.Write("Enter the Code to Run : ");
                string code = Console.ReadLine();

                switch (code)
                {
                    case "1":
                        await BulkInsert();
                        break;
                    case "2":
                        bulkDelete();
                        Console.WriteLine("Succesfully Deleted");
                        break;
                    case "3":
                        bulkUpdate();
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

        public async Task BulkInsert()
        {
            List<Student> studentsInsert = new List<Student>();

            string Choice;
            do
            {
                Console.Write("Enter the Name : ");
                string checkName = Console.ReadLine();
                string Name = input.StringCheck(checkName);

                Console.Write("Enter the Email : ");
                string checkEmail = Console.ReadLine();
                string Email = input.EmailCheck(checkEmail);

                Console.Write("Enter the Age : ");
                string checkAge = Console.ReadLine();
                int Age = input.AgeCheck(checkAge);

            againcheckid:
                Console.WriteLine("Student Department Name :");

                Dictionary<int, int> code = new Dictionary<int, int>();
                using (var dbcontext = new StudentsEntities())
                {
                    List<Department> departments = await dbcontext.Departments.ToListAsync();

                    int count = 1;
                    foreach (var department in departments)
                    {
                        code.Add(count, department.DepartmentID);

                        Console.WriteLine($"{count++} : {department.DepartmentName}");
                    }
                }

                string departmentidcheck = Console.ReadLine();
                int DepartmentId = input.IntCheck(departmentidcheck);

                if (!code.ContainsKey(DepartmentId))
                {
                    Console.WriteLine("Try again not match");
                    goto againcheckid;
                }

                int OriginalID = code[DepartmentId];

                studentsInsert.Add(new Student
                {
                    Name = Name,
                    Email = Email,
                    Age = Age,
                    DepartmentID = OriginalID
                });

                Console.Write("Do you want to Add another Student (yes/no) : ");
                Choice = Console.ReadLine();

            } while (Choice.ToLower() == "yes");

            using (var dbcontext = new StudentsEntities())
            {
                dbcontext.Students.AddRange(studentsInsert);
                await dbcontext.SaveChangesAsync();
            }
        }

        public void bulkUpdate()
        {
            //using(var dbcontext = new StudentsEntities())
            //{
            //    var studentUpdate = dbcontext.Students.Where(s => s.Age < 18).ToList();

            //    foreach(var student in studentUpdate)
            //    {
            //        student.Age = 20;
            //    }

            //    dbcontext.SaveChanges();
            //}

            //using(var dbcontext = new StudentsEntities())
            //{
            //    dbcontext.Students.Where(s => s.Age < 18).Update(s => new Student
            //    {
            //        Age = 20
            //    });
            //}

            using (var dbcontext = new StudentsEntities())
            {
                var studentUpdate = dbcontext.Students.Where(s => s.Age == 20).ToList();

                foreach (var student in studentUpdate)
                {
                    student.Name = "HELLO";
                }

                dbcontext.Students.BulkUpdate(studentUpdate);

            }
        }

        public async Task bulkDelete()
        {
            //using(var dbcontext = new StudentsEntities())
            //{
            //    var studentsToDelete = await dbcontext.Students.Where(s => s.Age < 18).ToListAsync();

            //    dbcontext.Students.RemoveRange(studentsToDelete);

            //    await dbcontext.SaveChangesAsync();
            //}

            //using (var dbcontext = new StudentsEntities())
            //{
            //    dbcontext.Students.Where(s => s.Age < 18).Delete();
            //}

            using (var dbcontext = new StudentsEntities())
            {
                var studentsToDelete = await dbcontext.Students.Where(s => s.Age < 18).ToListAsync();

                dbcontext.Students.BulkDelete(studentsToDelete);
            }
        }
    }
}
