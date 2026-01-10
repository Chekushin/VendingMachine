using System.Collections.Generic;

public class CourseManager
{
    public List<Course> AllCourses { get; set; } = new List<Course>();

    public void AddCourse(Course c)
    {
        AllCourses.Add(c);
    }

    public void RemoveCourse(Course c)
    {
        AllCourses.Remove(c);
    }

    public List<Course> GetCoursesByTeacher(Teacher t)
    {
        List<Course> result = new List<Course>();
        foreach (var c in AllCourses)
        {
            if (c.Teacher.Name == t.Name)
                result.Add(c);
        }
        return result;
    }
}