using System;
using System.Collections.Generic;

namespace EFandLINQPractices.Models
{
    public class Student
    {
        public string StudentID { get; set; }
        public string StudentName { get; set; }
        public DateTime StudentDOB { get; set; }
        public string StudentAddress { get; set; }

        public List<Subject> subject { get; set; }
    }
}