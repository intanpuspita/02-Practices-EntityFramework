using System;
using System.ComponentModel.DataAnnotations;

namespace EFandLINQPractices.ViewModels
{
    public class StudentEditViewModel
    {
        [Key]
        [Required()]
        [StringLength(5, MinimumLength=5)]
        public string StudentID { get; set; }

        [Required()]
        [StringLength(250, MinimumLength=2)]
        public string StudentName { get; set; }
        public DateTime StudentDOB { get; set; }
        public string StudentAddress { get; set; }
    }
}