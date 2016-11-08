using EFandLINQPractices.Models;
using EFandLINQPractices.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFandLINQPractices.Controllers
{
    /// <summary>
    /// Transaction Controller
    /// Controller with example of LINQ
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class TransactionController : Controller
    {
        StudentRepository studentRepo = new StudentRepository();
        SubjectAssignmentRepository subjectAssignmentRepo = new SubjectAssignmentRepository();

        public ActionResult Index()
        {
            List<Student> students = studentRepo.GetAll();

            //// Obtaining Data Source
            var data = from std in students
                       select std;

            return View(data);
        }

        [HttpPost]
        public ActionResult Search(string searchtext)
        {
            List<Student> students = studentRepo.GetAll();

            //// Filtering Data
            var data = from std in students
                       where std.StudentName.Contains(searchtext)
                       select std;

            return View("Index", data);
        }

        public ActionResult Sort(string field)
        {
            List<Student> students = studentRepo.GetAll();
            var data = (IEnumerable<Student>)null;

            //// Sorting Data
            switch(field)
            {
                case "name" :
                     data = from std in students
                            orderby std.StudentName ascending
                            select std;
                     break;
                case "dob" :
                     data = from std in students
                            orderby std.StudentDOB ascending
                            select std;
                     break;
                case "address" :
                     data = from std in students
                            orderby std.StudentAddress ascending
                            select std;
                     break;
                default :
                     data = from std in students
                            select std;
                     break;
            }

            return View("Index", data);
        }

        public ActionResult Group()
        {
            List<Student> students = studentRepo.GetAll();
            List<SubjectAssignment> subjectAssignments = subjectAssignmentRepo.GetAll();

            //// Join and Grouping
            var data = from sa in subjectAssignments
                       join std in students on sa.StudentID equals std.StudentID
                       group sa by sa.StudentID into sagroup
                       select sagroup;

            return View("Index", data);
        }
	}
}