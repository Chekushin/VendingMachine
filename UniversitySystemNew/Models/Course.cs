using System.Collections.Generic;

public abstract class Course
{
    public string Name { get; set; }
    public Teacher Teacher { get; set; }
    public List<Student> Students { get; set; } = new List<Student>();

    public Course(string name, Teacher teacher)
    {
        Name = name;
        Teacher = teacher;
    }

    public void AddStudent(Student s)
    {
        Students.Add(s);
    }
}