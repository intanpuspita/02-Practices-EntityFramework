using System.Collections.Generic;

namespace EFandLINQPractices.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string SubjectDescription { get; set; }

        //public virtual List<Student> student { get; set; }
        public ICollection<SubjectAssignment> subjectAssignment { get; set; }
    }
}