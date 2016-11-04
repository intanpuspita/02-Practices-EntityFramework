using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EFandLINQPractices.Models
{
    public class SchoolContextInitializer : DropCreateDatabaseAlways<SchoolContext>
    {
    }
}