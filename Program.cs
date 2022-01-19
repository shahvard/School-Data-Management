using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Hello\nWhat would you like to do\n1.Select from Table\n2.Insert value into a table" +
                "\n3.Update a row in a table\n4.Delete a value from a table\n5.Select from a table using a name" +
                "\n6.Get the total and Average grades of a student\n7.Exit");
            int op = Int32.Parse(Console.ReadLine());
            while (op != 7)
            {
                switch (op)
                {
                    case 1:
                        select();
                        break;
                    case 2:
                        Insert();
                        break;
                    case 3:
                        Update();
                        break;
                    case 4:
                        Delete();
                        break;
                    case 5:
                        GetByName();
                        break;
                    case 6:
                        calculateTotalAndAverageGrades();
                        break;
                    default:
                        Console.WriteLine("Enter a valid number");
                        break;

                }




                Console.WriteLine("Hello\n What would you like to do\n 1.Select from Table\n2.Insert value into a table" +
                "\n3.Update a row in a table\n4.Delete a value from a table\n5.Select from a table using a name" +
                "\n6.Get the total and Average grades of a student\n7.Exit");
                op = Int32.Parse(Console.ReadLine());
            }


        }




        static void calculateTotalAndAverageGrades()
        {
            using (var context = new SchoolDBEntities())
            {
                Console.WriteLine("Enter the student id:");
                int id = Int32.Parse(Console.ReadLine());
                Student std = context.Students.Find(id);
                while (std == null)
                {
                    Console.WriteLine("Please Enter a valid Student Id");
                    id = Int32.Parse(Console.ReadLine());
                    std = context.Students.Find(id);
                }
                Console.WriteLine("Enter Marks of Chemistry:");
                double c = Double.Parse(Console.ReadLine());
                Console.WriteLine("Enter Marks of Physics:");
                double p = Double.Parse(Console.ReadLine());
                Console.WriteLine("Enter Marks of Maths:");
                double m = Double.Parse(Console.ReadLine());

                double total = c + p + m;
                double avg = total / 3;

                Console.WriteLine($"The Total grade of student id {id} is {total} and the average grade is {avg} ");



            }
        }

        static void select()
        {
            Console.WriteLine("Which table you want to read \n 1.Student \n 2.Teacher \n3.Course ");
            int choice = Int32.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    using (var context = new SchoolDBEntities())
                    {
                        var student = context.Students;
                        Console.WriteLine($"\n{"Student Id",5} {"Student Name",-20} {"Standard Id",5}\n");
                        foreach (var s in student)
                        {
                            Console.WriteLine($"\n{s.StudentID,-15} {s.StudentName,-20} {s.StandardId,5}");
                        }
                    }

                    break;

                case 2:
                    using (var context = new SchoolDBEntities())
                    {
                        var teacher = context.Teachers;
                        Console.WriteLine($"\n{"Teacher Id",5} {"Teacher Name",-20} {"Teacher Type",5}\n");
                        foreach (var s in teacher)
                        {
                            Console.WriteLine($"\n{s.TeacherId,-15} {s.TeacherName,-20} {s.TeacherType,5}");
                        }
                    }

                    break;

                case 3:
                    using (var context = new SchoolDBEntities())
                    {
                        var course = context.Courses;
                        Console.WriteLine($"\n{"Course Id",5} {"Course Name",-20} {"Teacher Id",5}\n");
                        foreach (var s in course)
                        {
                            Console.WriteLine($"\n{s.CourseId,-15} {s.CourseName,-20} {s.TeacherId,5}");
                        }
                    }
                    break;



            }
        }

        static void GetByName()
        {
            Console.WriteLine("Search By Name \n 1.Search Student \n 2.Search Teacher ");
            int choice = Int32.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    using (var context = new SchoolDBEntities())
                    {
                        Console.WriteLine("Enter Student Name");
                        string name = Console.ReadLine();
                        Console.WriteLine("How do you want to sort the data \n1. Ascending \n2.Descending");
                        int asde = Int32.Parse(Console.ReadLine());

                        if (asde == 1)
                        {
                            var student = (from st in context.Students
                                           join sa in context.StudentAddresses on
                                           st.StudentID equals sa.StudentID
                                           where st.StudentName == name
                                           orderby st.StudentID
                                           select new
                                           {
                                               st.StudentID,
                                               st.StudentName,
                                               sa.Address1,
                                               sa.City,
                                               sa.State

                                           }).ToList();
                            Console.WriteLine($"\n{"Student Id",5} {"Student Name",-20} {"Address",-5} " +
                                                $"{"City",-10}{"Province",-15}\n");
                            foreach (var s in student)
                            {
                                Console.WriteLine($"\n{s.StudentID,-15} {s.StudentName,-20} {s.Address1,5} " +
                                    $"{s.City,-10} {s.State,-15}\n");
                            }
                        }
                        else if (asde == 2)
                        {
                            var student = (from st in context.Students
                                           join sa in context.StudentAddresses on
                                           st.StudentID equals sa.StudentID
                                           where st.StudentName == name
                                           orderby st.StudentID descending
                                           select new
                                           {
                                               st.StudentID,
                                               st.StudentName,
                                               sa.Address1,
                                               sa.City,
                                               sa.State

                                           }).ToList();
                            Console.WriteLine($"\n{"Student Id",5} {"Student Name",-20} {"Address",-5} " +
                                $"{"City",-10}{"Province",-15}\n");
                            foreach (var s in student)
                            {
                                Console.WriteLine($"\n{s.StudentID,-15} {s.StudentName,-20} {s.Address1,5} " +
                                    $"{s.City,-10} {s.State,-15}\n");
                            }

                        }
                        else
                        {
                            Console.WriteLine("Enter a valid number");
                        }


                    }

                    break;

                case 2:
                    using (var context = new SchoolDBEntities())
                    {
                        Console.WriteLine("Enter Teacher Name");
                        string name = Console.ReadLine();
                        Console.WriteLine("How do you want to sort the data \n1. Ascending \n2.Descending");
                        int asde = Int32.Parse(Console.ReadLine());
                        var teacher = context.Teachers
                                             .Where(s => s.TeacherName == name);
                        if (asde == 1)
                        {
                            teacher = context.Teachers
                                              .Where(s => s.TeacherName == name)
                                              .OrderBy(s => s.TeacherId);
                        }
                        else if (asde == 2)
                        {
                            teacher = context.Teachers
                           .Where(s => s.TeacherName == name)
                            .OrderByDescending(s => s.TeacherId);
                        }
                        Console.WriteLine($"\n{"Teacher Id",5} {"Teacher Name",-20} {"Teacher type",5} \n");
                        foreach (var s in teacher)
                        {
                            Console.WriteLine($"\n{s.TeacherId,-15} {s.TeacherName,-20} {s.StandardId,-5} {s.TeacherType,5}\n");
                        }

                    }

                    break;
            }

        }

        static void Insert()
        {
            Console.WriteLine("1.Insert Student \n2. Insert Teacher \n3. Insert Course");
            int choice = Int32.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    using (var context = new SchoolDBEntities())
                    {
                        Console.WriteLine("Enter name of the student:");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter Standard Id of the student:");
                        int stid = Int32.Parse(Console.ReadLine());
                        Standard st = context.Standards.Find(stid);
                        while (st == null)
                        {

                            Console.WriteLine("Please Enter a valid Standard Id");
                            stid = Int32.Parse(Console.ReadLine());
                            st = context.Standards.Find(stid);

                        }

                        Student std = new Student();
                        std.StudentName = name;
                        std.StandardId = stid;

                        // add student to Students entity set and save changes
                        context.Students.Add(std);
                        context.SaveChanges();
                        Console.WriteLine("\n Student Added \n");
                    }
                    break;
                case 2:
                    using (var context = new SchoolDBEntities())
                    {
                        Console.WriteLine("Enter name of the Teacher:");
                        string name = Console.ReadLine();

                        Console.WriteLine("Enter Standard Id of the Teacher:");
                        int stid = Int32.Parse(Console.ReadLine());
                        Standard st = context.Standards.Find(stid);
                        while (st == null)
                        {

                            Console.WriteLine("Please Enter a valid Standard Id");
                            stid = Int32.Parse(Console.ReadLine());
                            st = context.Standards.Find(stid);

                        }
                        Console.WriteLine("ENter Teacher Type (1,2,3)");
                        int teachtype = Int32.Parse(Console.ReadLine());


                        Teacher std = new Teacher();
                        std.TeacherName = name;
                        std.StandardId = stid;
                        std.TeacherType = teachtype;

                        context.Teachers.Add(std);
                        context.SaveChanges();
                        Console.WriteLine("\n Teacher Added \n");
                    }
                    break;
                case 3:
                    using (var context = new SchoolDBEntities())
                    {
                        Console.WriteLine("Enter name of the Course:");
                        string courseName = Console.ReadLine();

                        Console.WriteLine("Enter Teacher Id");
                        int teachId = Int32.Parse(Console.ReadLine());
                        Teacher t = context.Teachers.Find(teachId);
                        while (t == null)
                        {

                            Console.WriteLine("Please Enter a valid Teacher Id");
                            teachId = Int32.Parse(Console.ReadLine());
                            t = context.Teachers.Find(teachId);

                        }
                        Course std = new Course();
                        std.CourseName = courseName;
                        std.TeacherId = teachId;
                        context.Courses.Add(std);
                        context.SaveChanges();
                        Console.WriteLine("Record Inserted\n");
                    }
                    break;

            }

        }

        static void Update()
        {
            Console.WriteLine("1.Update Student \n2. Update Teacher \n3. Update Course");
            int choice = Int32.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    using (var context = new SchoolDBEntities())
                    {
                        Console.WriteLine("Enter Student Id");
                        int stId = Int32.Parse(Console.ReadLine());
                        Student std = context.Students.Find(stId);
                        Console.WriteLine("What would you like to update \n 1.Student Name \n2.Standard Id ");
                        int choice1 = Int32.Parse(Console.ReadLine());
                        switch (choice1)
                        {
                            case 1:
                                Console.WriteLine("Enter Student Name");
                                string name = Console.ReadLine();
                                std.StudentName = name;
                                Console.WriteLine("Do you want to confirm the update?\n 1.Yes \n2.No");
                                int conf = Int32.Parse(Console.ReadLine());
                                switch (conf)
                                {
                                    case 1:
                                        context.SaveChanges();
                                        Console.WriteLine("Record Updated\n");
                                        break;
                                    case 2:
                                        Console.WriteLine("Changes Not Saved");
                                        break;

                                }

                                break;
                            case 2:
                                Console.WriteLine("Enter StandardId");
                                int staId = Int32.Parse(Console.ReadLine());
                                Standard st = context.Standards.Find(staId);
                                while (st == null)
                                {

                                    Console.WriteLine("Please Enter a valid Standard Id");
                                    staId = Int32.Parse(Console.ReadLine());
                                    st = context.Standards.Find(staId);

                                }
                                std.StandardId = staId;
                                Console.WriteLine("Do you want to confirm the update?\n 1.Yes \n2.No");
                                int conf1 = Int32.Parse(Console.ReadLine());
                                switch (conf1)
                                {
                                    case 1:
                                        context.SaveChanges();
                                        Console.WriteLine("Record Updated\n");
                                        break;
                                    case 2:
                                        Console.WriteLine("Changes Not Saved");
                                        break;

                                }
                                break;

                        }



                    }
                    break;

                case 2:
                    using (var context = new SchoolDBEntities())
                    {
                        Console.WriteLine("Enter Teacher Id");
                        int teachId = Int32.Parse(Console.ReadLine());
                        Teacher std = context.Teachers.Find(teachId);
                        Console.WriteLine("What would you like to update \n 1.Teacher Name \n2.Standard Id \n3. Teacher Type");
                        int choice1 = Int32.Parse(Console.ReadLine());
                        switch (choice1)
                        {
                            case 1:
                                Console.WriteLine("Enter Teacher Name");
                                string name = Console.ReadLine();
                                std.TeacherName = name;
                                Console.WriteLine("Do you want to confirm the update?\n 1.Yes \n2.No");
                                int conf = Int32.Parse(Console.ReadLine());
                                switch (conf)
                                {
                                    case 1:
                                        context.SaveChanges();
                                        Console.WriteLine("Record Updated\n");
                                        break;
                                    case 2:
                                        Console.WriteLine("Changes Not Saved");
                                        break;

                                }
                                break;
                            case 2:
                                Console.WriteLine("Enter StandardId");
                                int staId = Int32.Parse(Console.ReadLine());
                                Standard st = context.Standards.Find(staId);
                                while (st == null)
                                {

                                    Console.WriteLine("Please Enter a valid Standard Id");
                                    staId = Int32.Parse(Console.ReadLine());
                                    st = context.Standards.Find(staId);

                                }
                                std.StandardId = staId;
                                Console.WriteLine("Do you want to confirm the update?\n 1.Yes \n2.No");
                                int conf1 = Int32.Parse(Console.ReadLine());
                                switch (conf1)
                                {
                                    case 1:
                                        context.SaveChanges();
                                        Console.WriteLine("Record Updated\n");
                                        break;
                                    case 2:
                                        Console.WriteLine("Changes Not Saved");
                                        break;

                                }
                                break;
                            case 3:
                                Console.WriteLine("Enter Teacher Type in (1,2,3)");
                                int teachType = Int32.Parse(Console.ReadLine());
                                std.TeacherType = teachType;
                                Console.WriteLine("Do you want to confirm the update?\n 1.Yes \n2.No");
                                int conf2 = Int32.Parse(Console.ReadLine());
                                switch (conf2)
                                {
                                    case 1:
                                        context.SaveChanges();
                                        Console.WriteLine("Record Updated\n");
                                        break;
                                    case 2:
                                        Console.WriteLine("Changes Not Saved");
                                        break;

                                }
                                break;
                        }



                    }
                    break;
                case 3:
                    using (var context = new SchoolDBEntities())
                    {
                        Console.WriteLine("Enter Course Id");
                        int courseId = Int32.Parse(Console.ReadLine());
                        Course std = context.Courses.Find(courseId);
                        Console.WriteLine("What would you like to update \n 1.Course Name \n2.Teacher Id \n");
                        int choice1 = Int32.Parse(Console.ReadLine());
                        switch (choice1)
                        {
                            case 1:
                                Console.WriteLine("Enter Course Name");
                                string name = Console.ReadLine();
                                std.CourseName = name;
                                Console.WriteLine("Do you want to confirm the update?\n 1.Yes \n2.No");
                                int conf = Int32.Parse(Console.ReadLine());
                                switch (conf)
                                {
                                    case 1:
                                        context.SaveChanges();
                                        Console.WriteLine("Record Updated\n");
                                        break;
                                    case 2:
                                        Console.WriteLine("Changes Not Saved");
                                        break;

                                }
                                break;
                            case 2:
                                Console.WriteLine("Enter TeacherId");
                                int staId = Int32.Parse(Console.ReadLine());
                                Teacher st = context.Teachers.Find(staId);
                                while (st == null)
                                {

                                    Console.WriteLine("Please Enter a valid Teacher Id");
                                    staId = Int32.Parse(Console.ReadLine());
                                    st = context.Teachers.Find(staId);

                                }
                                std.TeacherId = staId;
                                Console.WriteLine("Do you want to confirm the update?\n 1.Yes \n2.No");
                                int conf1 = Int32.Parse(Console.ReadLine());
                                switch (conf1)
                                {
                                    case 1:
                                        context.SaveChanges();
                                        Console.WriteLine("Record Updated\n");
                                        break;
                                    case 2:
                                        Console.WriteLine("Changes Not Saved");
                                        break;

                                }
                                break;

                        }
                    }
                    break;



            }

        }

        static void Delete()
        {
            Console.WriteLine("1.Delete Student \n2. Delete Teacher \n3. Delete Course");
            int choice = Int32.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:

                    using (var context = new SchoolDBEntities())
                    {
                        Console.WriteLine("Enter Student Id of the Student");
                        int id = Int32.Parse(Console.ReadLine());
                        Student std = context.Students.Find(id);
                        context.Students.Remove(std);
                        Console.WriteLine("Do you want to confirm the Delete?\n 1.Yes \n2.No");
                        int conf = Int32.Parse(Console.ReadLine());
                        switch (conf)
                        {
                            case 1:
                                context.SaveChanges();
                                Console.WriteLine("Record Deleted\n");
                                break;
                            case 2:
                                Console.WriteLine("Not Deleted");
                                break;

                        }
                        break;
                    }

                case 2:
                    using (var context = new SchoolDBEntities())
                    {
                        Console.WriteLine("Enter Teacher Id of the Teacher");
                        int id = Int32.Parse(Console.ReadLine());
                        Teacher std = context.Teachers.Find(id);
                        context.Teachers.Remove(std);
                        Console.WriteLine("Do you want to confirm the Delete?\n 1.Yes \n2.No");
                        int conf = Int32.Parse(Console.ReadLine());
                        switch (conf)
                        {
                            case 1:
                                context.SaveChanges();
                                Console.WriteLine("Record Deleted\n");
                                break;
                            case 2:
                                Console.WriteLine("Not Deleted");
                                break;

                        }
                        break;
                    }

                case 3:
                    using (var context = new SchoolDBEntities())
                    {
                        Console.WriteLine("Enter Course Id of the Course");
                        int id = Int32.Parse(Console.ReadLine());
                        Course std = context.Courses.Find(id);
                        context.Courses.Remove(std);
                        Console.WriteLine("Do you want to confirm the Delete?\n 1.Yes \n2.No");
                        int conf = Int32.Parse(Console.ReadLine());
                        switch (conf)
                        {
                            case 1:
                                context.SaveChanges();
                                Console.WriteLine("Record Deleted\n");
                                break;
                            case 2:
                                Console.WriteLine("Not Deleted");
                                break;

                        }
                        break;
                    }


            }
        }
    }
}
