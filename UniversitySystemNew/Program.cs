using System;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        CourseManager manager = new CourseManager();

        Console.WriteLine("Добро пожаловать в систему!");

        bool running = true;
        while (running)
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1 - Добавить курс");
            Console.WriteLine("2 - Добавить студента в курс");
            Console.WriteLine("3 - Показать все курсы");
            Console.WriteLine("4 - Показать курсы преподавателя");
            Console.WriteLine("5 - Выход");
            Console.Write("Ваш выбор: ");

            string choice = Console.ReadLine()!;
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Название курса: ");
                    string courseName = Console.ReadLine()!;
                    Console.Write("Тип курса (online/offline): ");
                    string type = Console.ReadLine()!;
                    Console.Write("Имя преподавателя: ");
                    string teacherName = Console.ReadLine()!;

                    Teacher t = new Teacher(teacherName);

                    if (type.ToLower() == "online")
                    {
                        Console.Write("Платформа: ");
                        string platform = Console.ReadLine()!;
                        manager.AddCourse(new OnlineCourse(courseName, t, platform));
                    }
                    else
                    {
                        Console.Write("Аудитория: ");
                        string room = Console.ReadLine()!;
                        manager.AddCourse(new OfflineCourse(courseName, t, room));
                    }

                    Console.WriteLine($"Курс \"{courseName}\" добавлен!");
                    break;

                case "2":
                    if (manager.AllCourses.Count == 0)
                    {
                        Console.WriteLine("Сначала добавьте курс.");
                        break;
                    }

                    for (int i = 0; i < manager.AllCourses.Count; i++)
                    {
                        Console.WriteLine($"{i + 1} - {manager.AllCourses[i].Name}");
                    }

                    Console.Write("Выберите курс (номер): ");
                    int courseIndex = int.Parse(Console.ReadLine()!) - 1;

                    Console.Write("Имя студента: ");
                    string studentName = Console.ReadLine()!;

                    Student s = new Student(studentName);
                    manager.AllCourses[courseIndex].AddStudent(s);

                    Console.WriteLine($"Студент {studentName} добавлен в курс {manager.AllCourses[courseIndex].Name}.");
                    break;

                case "3":
                    foreach (var c in manager.AllCourses)
                    {
                        Console.WriteLine($"Курс: {c.Name}, Преподаватель: {c.Teacher.Name}");
                        if (c.Students.Count > 0)
                        {
                            Console.WriteLine("Студенты:");
                            foreach (var st in c.Students)
                                Console.WriteLine($"- {st.Name}");
                        }
                    }
                    break;

                case "4":
                    Console.Write("Имя преподавателя: ");
                    string searchTeacher = Console.ReadLine()!;
                    var coursesByTeacher = manager.GetCoursesByTeacher(new Teacher(searchTeacher));
                    if (coursesByTeacher.Count == 0)
                        Console.WriteLine("У этого преподавателя пока нет курсов.");
                    else
                    {
                        Console.WriteLine($"Курсы преподавателя {searchTeacher}:");
                        foreach (var c in coursesByTeacher)
                            Console.WriteLine($"- {c.Name}");
                    }
                    break;

                case "5":
                    running = false;
                    Console.WriteLine("Выход...");
                    break;

                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    break;
            }
        }
    }
}
