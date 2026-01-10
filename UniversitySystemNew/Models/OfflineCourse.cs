public class OfflineCourse : Course
{
    public string Room { get; set; }

    public OfflineCourse(string name, Teacher teacher, string room)
        : base(name, teacher)
    {
        Room = room;
    }
}