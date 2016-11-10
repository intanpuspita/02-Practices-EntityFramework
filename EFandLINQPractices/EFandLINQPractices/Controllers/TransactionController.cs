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
        SubjectRepository subjectRepo = new SubjectRepository();
        SubjectAssignmentRepository subjectAssignmentRepo = new SubjectAssignmentRepository();

        public ActionResult Index()
        {
            List<SubjectAssignment> subjectAssignments = subjectAssignmentRepo.GetAll();

            //// Obtaining Data Source
            var data = from sa in subjectAssignments
                       select sa;

            return View(data);
        }

        /// <summary>
        /// Searches the specified searchtext.
        /// To fire this action, fill the textbox for search criteria.
        /// And then click on search button.
        /// </summary>
        /// <param name="searchtext">The searchtext.</param>
        /// <returns>Search Result</returns>
        [HttpPost]
        public ActionResult Search(string searchtext)
        {
            List<SubjectAssignment> subjectAssignments = subjectAssignmentRepo.GetAll();

            //// Filtering Data
            var data = from sa in subjectAssignments
                       where sa.students.StudentName.Contains(searchtext) || sa.subjects.SubjectName.Contains(searchtext)
                       select sa;

            return View("Index", data);
        }

        /// <summary>
        /// Sorts the specified field.
        /// To fire this action, click column name in table header.
        /// It will sort data based on clicked column.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>Data that already sorted.</returns>
        public ActionResult Sort(string field)
        {
            List<SubjectAssignment> subjectAssignments = subjectAssignmentRepo.GetAll();
            var data = (IEnumerable<SubjectAssignment>)null;

            //// Sorting Data
            switch(field)
            {
                case "stdname" :
                    data = from sa in subjectAssignments
                           orderby sa.students.StudentName ascending
                           select sa;
                     break;
                case "sbname" :
                     data = from sa in subjectAssignments
                            orderby sa.subjects.SubjectName ascending
                            select sa;
                     break;
                default :
                     data = from sa in subjectAssignments
                            select sa;
                     break;
            }

            return View("Index", data);
        }

        /// <summary>
        /// Groups this instance.
        /// To fire this action, click on group icon near column that want to be grouped.
        /// </summary>
        /// <returns>Data that already grouped.</returns>
        public ActionResult Group(string groupby)
        {
            List<Student> students = studentRepo.GetAll();
            List<Subject> subject = subjectRepo.GetAll();
            List<SubjectAssignment> subjectAssignments = subjectAssignmentRepo.GetAll();

            var data = (List<SubjectAssignment>)null;

            //// Join and Grouping
            switch(groupby)
            {
                case "stdname" :
                    data = //(List<SubjectAssignment>)
                          (from sa in subjectAssignments
                           join std in students on sa.StudentID equals std.StudentID
                           group sa by std.StudentName into sagroup
                           select sagroup).SelectMany(d => d).ToList();
                    break;
                case "sbname":
                    data = //(List<SubjectAssignment>)
                           (from sa in subjectAssignments
                            join sb in subject on sa.SubjectId equals sb.SubjectId
                            group sa by sb.SubjectName into sagroup
                            select sagroup).SelectMany(d => d).ToList();
                    break;
                default :
                    data = //(List<SubjectAssignment>)
                           (from sa in subjectAssignments
                           select sa).ToList();
                    break;
            }

            return View("Index", data);
        }
	}
}