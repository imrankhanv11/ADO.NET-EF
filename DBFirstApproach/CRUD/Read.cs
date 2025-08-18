using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFirstApproach
{
    internal class CrudOp
    {

        InputValidation input = new InputValidation();
        public void crudmethod()
        {

            while (true)
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("----------------| Menu |----------------");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("| 1.                 ReadStudent       |");
                Console.WriteLine("| 2.                 AddSTudent        |");
                Console.WriteLine("| 3.                 DeleteStudent     |");
                Console.WriteLine("| 4.                 UpdateStudent     |");
                Console.WriteLine("| 5.                 Exit              |");
                Console.WriteLine("----------------------------------------");

                Console.Write("Enter the Code to Run : ");
                string code = Console.ReadLine();

                switch (code)
                {
                    case "1":
                        readstudents();
                        break;
                    case "2":
                        addStudents();
                        break;
                    case "3":
                        delelteStudent();
                        break;
                    case "4":
                        updateStudent();
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

        public void readstudents()
        {
            using (var dbcontext = new StudentsEntities())
            {
                List<Student> students = dbcontext.Students.ToList();

                foreach (var student in students)
                {
                    Console.WriteLine($"ID : {student.StudentID}, Name : {student.Name}, Email : {student.Email}, Age : {student.Age}, Deparment ID : {student.DepartmentID}");
                }
            }
        }

        public void addStudents()
        {
            Console.Write("Enter the Student Name :");
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
            using(var dbcontext = new StudentsEntities())
            {
                List<Department> departments = dbcontext.Departments.ToList();

                int count = 1;
                foreach(var department in departments)
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
            try
            {
                using (var dbcontext = new StudentsEntities())
                {
                    var newstudent = new Student()
                    {
                        Name = Name,
                        Email = Email,
                        Age = Age,
                        DepartmentID = DepartmentId,
                    };

                    dbcontext.Students.Add(newstudent);

                    dbcontext.SaveChanges();
                }

                Console.WriteLine("New Student added succesfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }
        }

        public void delelteStudent()
        {
        checkdelid:
            Console.WriteLine("Enter the student Id to delete :");
            string checkdelId = Console.ReadLine();
            int DelId = input.IntCheck(checkdelId);

            using(var dbcontext = new StudentsEntities())
            {
                List<Student> checkid = dbcontext.Students.ToList();

                int count = 0;
                foreach( Student student in checkid)
                {
                    if(DelId == student.StudentID)
                    {
                        count++;
                    }
                }

                if(count == 0)
                {
                    Console.WriteLine("Student id not fount");
                    goto checkdelid;
                }

                var delstudent = dbcontext.Students.Find(DelId);

                dbcontext.Students.Remove(delstudent);
                dbcontext.SaveChanges();
                Console.WriteLine("Student deleted succesfully");
            }
        }

        public void updateStudent()
        {
        againupId:
            Console.Write("Enter the Student id to update : ");
            string checkupid = Console.ReadLine();
            int UpdateId = input.IntCheck(checkupid);

            using( var dbcontext = new StudentsEntities())
            {
                var studentid = dbcontext.Students.Find(UpdateId);

                if(studentid != null)
                {
                    Console.WriteLine("Which one need to update : ");
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("1.       Student Name");
                    Console.WriteLine("2.       Email");
                    Console.WriteLine("3.       Age");
                    Console.WriteLine("4.       Department");
                    Console.WriteLine("----------------------------");

                    Console.Write("Enter the code : ");
                    string code = Console.ReadLine();

                    switch(code)
                    {
                        case "1":
                            Console.Write("Enter the Name to update : ");
                            string checkname = Console.ReadLine();
                            string Nameup = input.StringCheck(checkname);

                            studentid.Name = Nameup;
                            break;
                        case "2":
                            Console.Write("Enter the Email to Update : ");
                            string checkemail = Console.ReadLine();
                            string Email = input.EmailCheck(checkemail);

                            studentid.Email = Email;
                            break;
                        case "3":
                            Console.Write("Enter the Age to Update : ");
                            string checkAge = Console.ReadLine();
                            int Age = input.AgeCheck(checkAge);

                            studentid.Age = Age;
                            break;
                        case "4":

                            Console.WriteLine("Student Department Name to update :");

                            Dictionary<int, int> codevalue = new Dictionary<int, int>();
                            using (var dbcontextinner = new StudentsEntities())
                            {
                                List<Department> departments = dbcontext.Departments.ToList();

                                int count = 1;
                                foreach (var department in departments)
                                {
                                    codevalue.Add(count, department.DepartmentID);

                                    Console.WriteLine($"{count++} : {department.DepartmentName}");
                                }
                            }

                            string departmentidcheck = Console.ReadLine();
                            int DepartmentId = input.IntCheck(departmentidcheck);

                            if (!codevalue.ContainsKey(DepartmentId))
                            {
                                Console.WriteLine("Try again not match");
                            }

                            int OriginalID = codevalue[DepartmentId];

                            studentid.DepartmentID = OriginalID;
                            break;
                        default:
                            Console.WriteLine("try again");
                            break;
                    }

                    dbcontext.SaveChanges();
                    Console.WriteLine("Update Succesfully");
                }
                else
                {
                    Console.WriteLine("Student id Not found ");
                    goto againupId;
                }
            }
        }
    }
}
