using System.Data.Entity;

namespace EFandLINQPractices.Models
{
    public class SchoolContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SchoolContext"/> class.
        /// Define generated database name : dbschool.
        /// </summary>
        public SchoolContext() : base("dbschool") 
        {
            Database.SetInitializer<SchoolContext>(new DropCreateDatabaseIfModelChanges<SchoolContext>());
            //Database.SetInitializer<SchoolContext>(new DropCreateDatabaseAlways<SchoolContext>());
        }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //// Set primary key of table
            modelBuilder.Entity<Student>().HasKey(d => d.StudentID);
            modelBuilder.Entity<Subject>().HasKey(d => d.SubjectId);

            //// Create many-to-many relationship
            //modelBuilder.Entity<Student>()
            //    .HasMany(s => s.Subject)
            //    .WithMany(sb => sb.Student)
            //    .Map(mt => mt.ToTable("SubjectAssignment"));

            modelBuilder.Entity<Student>()
                  .HasMany(x => x.subject)
                  .WithMany(x => x.student)
                  .Map(x =>
                  {
                      x.ToTable("SubjectAssignment");
                      x.MapLeftKey("StudentId");
                      x.MapRightKey("SubjectId");
                  });

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Gets or sets the students.
        /// </summary>
        /// <value>
        /// The students.
        /// </value>
        public DbSet<Student> Students { get; set; }

        /// <summary>
        /// Gets or sets the subjects.
        /// </summary>
        /// <value>
        /// The subjects.
        /// </value>
        public DbSet<Subject> Subjects { get; set; }

        /// <summary>
        /// Gets or sets the subject assignments.
        /// </summary>
        /// <value>
        /// The subject assignments.
        /// </value>
        public DbSet<SubjectAssignment> SubjectAssignments { get; set; }

        /// <summary>
        /// Gets or sets the student edit view models.
        /// </summary>
        /// <value>
        /// The student edit view models.
        /// </value>
        public System.Data.Entity.DbSet<EFandLINQPractices.ViewModels.StudentEditViewModel> StudentEditViewModels { get; set; }
    }
}