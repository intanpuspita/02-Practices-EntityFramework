using EFandLINQPractices.Models;
using EFandLINQPractices.Models.Repositories;
using EFandLINQPractices.ViewModels;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Net;
using System.Transactions;
using System.Web.Mvc;

namespace EFandLINQPractices.Controllers
{
    public class StudentController : Controller
    {
        /// <summary>
        /// The repository.
        /// </summary>
        StudentRepository repo = new StudentRepository();

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>View of index and all data.</returns>
        public ActionResult Index()
        {
            return View(repo.GetAll());
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>Create page view.</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Creates the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>
        /// Redirect to index if success
        /// or
        /// keep in create page with error message if fail.
        /// </returns>
        [HttpPost]
        public ActionResult Create(StudentEditViewModel data)
        {
            if (!ModelState.IsValid) return View(data);

            Student newStudent = new Student
                                {
                                    StudentID = data.StudentID,
                                    StudentName = data.StudentName,
                                    StudentDOB = data.StudentDOB,
                                    StudentAddress = data.StudentAddress
                                };

            try
            {
                if (repo.GetById(newStudent.StudentID) != null) throw new Exception("Student ID already registered");

                repo.Add(newStudent);
                repo.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                ViewBag.Message = "Error : " + ex.Message;
                return View(data);
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var result in ex.EntityValidationErrors)
                {
                    foreach (var error in result.ValidationErrors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }
                return View(data);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error : " + ex.Message;
                return View(data);
            }
        }

        /// <summary>
        /// Edits this instance.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Edit page view.
        /// </returns>
        public ActionResult Edit(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Student data = repo.GetById(id);
            if (data != null)
            {
                StudentEditViewModel newmodel = new StudentEditViewModel
                                                {
                                                    StudentID = data.StudentID,
                                                    StudentName = data.StudentName,
                                                    StudentDOB = data.StudentDOB,
                                                    StudentAddress = data.StudentAddress
                                                };

                return View(newmodel);
            }
            else
            {
                return HttpNotFound();
            }
        }

        /// <summary>
        /// Edits the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Student ID already registered.</exception>
        [HttpPost]
        public ActionResult Edit(StudentEditViewModel data)
        {
            if (!ModelState.IsValid) return View(data);

            Student newStudent = new Student
            {
                StudentID = data.StudentID,
                StudentName = data.StudentName,
                StudentDOB = data.StudentDOB,
                StudentAddress = data.StudentAddress
            };

            try
            {
                if (repo.GetById(data.StudentID) == null) throw new Exception("Student ID not registered");

                using (TransactionScope scope = new TransactionScope())
                {
                    SchoolContext context = new SchoolContext();
                    context.Entry(newStudent).State = EntityState.Modified;
                    context.SaveChanges();
                    //repo.Update(newStudent);
                    //repo.SaveChanges();
                    scope.Complete();
                }

                return RedirectToAction("Index");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                ViewBag.Message = "Error : " + ex.Message;
                return View(data);
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var result in ex.EntityValidationErrors)
                {
                    foreach (var error in result.ValidationErrors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }
                return View(data);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error : " + ex.Message;
                return View(data);
            }
        }
	}
}