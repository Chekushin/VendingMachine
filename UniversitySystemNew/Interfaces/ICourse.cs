using System.Collections.Generic;

namespace UniversitySystemNew.Interfaces
{
    public interface ICourse
    {
        string Title { get; set; }
        List<IStudent> Students { get; }

        void AddStudent(IStudent student);
    }
}