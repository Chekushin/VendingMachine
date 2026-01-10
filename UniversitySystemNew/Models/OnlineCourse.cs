public class OnlineCourse : Course
{
    public string Platform { get; set; }

    public OnlineCourse(string name, Teacher teacher, string platform)
        : base(name, teacher)
    {
        Platform = platform;
    }
}