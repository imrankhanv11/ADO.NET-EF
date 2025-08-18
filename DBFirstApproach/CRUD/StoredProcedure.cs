using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Dynamic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DbFirstApproach
{
    internal class StoredProcedure
    {
        InputValidation input = new InputValidation();
        public void sp()
        {
            Console.Write("Enter the Department ID : ");
            string checkID = Console.ReadLine();
            int ID = input.IntCheck(checkID);

            using(var dbcontext = new StudentsEntities())
            {
                var sp_department = dbcontext.GetStudentsByDepartment(ID).ToList();
                
                foreach(var result in sp_department)
                {
                    Console.WriteLine(result.StudentID+" "+result.Name);
                }
            }

            Console.WriteLine();
            //------------------------------------------


            List<int> list = new List<int>();
            using (var dbcontext = new StudentsEntities())
            {
                var student = dbcontext.Students.ToList();

                foreach(var result in student)
                {
                    list.Add(result.StudentID);
                    Console.WriteLine($"ID : {result.StudentID},  Name : {result.Name}");
                }
            }
        checkagian:
            Console.Write("Choose the Student ID : ");
            string checkStudentID = Console.ReadLine();
            int studentID = input.IntCheck(checkStudentID);

            if (!list.Contains(studentID))
            {
                Console.WriteLine("Id not found, Try Again");
                goto checkagian;
            }

            List<int> list2 = new List<int>();  
            using(var dbcontext = new StudentsEntities())
            {
                var student = dbcontext.Courses.ToList();

                foreach(var result in student)
                {
                    list2.Add(result.CourseID);
                    Console.WriteLine($"CouseID : {result.CourseID}, CourseName : {result.CourseName}");
                }
            }

        checkagaincourse:
            Console.Write("Choose the Course ID : ");
            string checkagaincourseID = Console.ReadLine();
            int courseId = input.IntCheck(checkagaincourseID);

            if(!list2.Contains(courseId))
            {
                Console.WriteLine("Course ID Not found, try again");
                goto checkagaincourse;
            }



            string grade="";
            while (true)
            {
                Console.WriteLine("1.  A");
                Console.WriteLine("2.  B");
                Console.WriteLine("3.  C");
                Console.WriteLine("4.  D");

                Console.Write("Enter the Grade : ");
                string code = Console.ReadLine();

                switch (code)
                {
                    case "1":
                        grade = "A";
                        break;
                    case "2":
                        grade = "B";
                        break;
                    case "3":
                        grade = "C";
                        break;
                    case "4":
                        grade = "D";
                        break;
                    default:
                        Console.WriteLine("Try again");
                        continue;
                }
                break;
            }
            using (var dbcontext = new StudentsEntities())
            {
                dbcontext.AddEnrollment(studentID, courseId, grade);
                dbcontext.SaveChanges();
            }

            //ObjectParameter outputParameter = new ObjectParameter("StudentId", typeof(int));
            //context.spCreateStudent(1, "Pranaya", "Rout", outputParameter);
            //Console.WriteLine($"Student ID: {outputParameter.Value}");

        }
    }
}
