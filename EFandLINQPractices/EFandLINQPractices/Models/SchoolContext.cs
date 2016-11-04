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
            Database.SetInitializer<SchoolContext>(new DropCreateDatabaseAlways<SchoolContext>());
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
            modelBuilder.Entity<Student>().HasKey(d => d.StudentID);

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Gets or sets the students.
        /// </summary>
        /// <value>
        /// The students.
        /// </value>
        public DbSet<Student> Students { get; set; }

        public System.Data.Entity.DbSet<EFandLINQPractices.ViewModels.StudentEditViewModel> StudentEditViewModels { get; set; }
    }
}