using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFandLINQPractices.Models
{
    public class SubjectAssignment
    {
        public string StudentID { get; set; }
        public int SubjectId { get; set; }

        public virtual Student students { get; set; }
        public virtual Subject subjects { get; set; }

        //public virtual List<Student> students { get; set; }
        //public virtual List<Subject> subjects { get; set; }
    }
}