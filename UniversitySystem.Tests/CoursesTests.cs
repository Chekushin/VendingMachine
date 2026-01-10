using System;
using Xunit;
using System.Collections.Generic;

public class CourseTests
{
    [Fact]
    public void AddCourse_WorksCorrectly()
    {
        CourseManager mgr = new CourseManager();
        Teacher t = new Teacher("Test");
        Course c = new OnlineCourse("Test Course", t, "Zoom");

        mgr.AddCourse(c);

        Assert.Contains(c, mgr.AllCourses);
    }

    [Fact]
    public void AddStudent_WorksCorrectly()
    {
        Teacher t = new Teacher("Test");
        Course c = new OnlineCourse("Test", t, "Zoom");

        Student s = new Student("Alex");

        c.AddStudent(s);

        Assert.Contains(s, c.Students);
    }

    [Fact]
    public void GetCoursesByTeacher_ReturnsCorrectCourses()
    {
        CourseManager manager = new CourseManager();

        Teacher t1 = new Teacher("Иван");
        Teacher t2 = new Teacher("Мария");

        var c1 = new OnlineCourse("C#", t1, "Teams");
        var c2 = new OfflineCourse("SQL", t1, "101");
        var c3 = new OfflineCourse("History", t2, "102");

        manager.AddCourse(c1);
        manager.AddCourse(c2);
        manager.AddCourse(c3);

        var result = manager.GetCoursesByTeacher(t1);

        Assert.Equal(2, result.Count);
    }
}