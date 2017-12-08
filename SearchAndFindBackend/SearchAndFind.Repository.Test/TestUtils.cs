using SearchAndFind.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Repository.Test
{
    public class TestUtils
    {
        public static void CleanDatabase()
        {
            using (var context = new SearchAndFindDbContext())
            {
                
                context.Database.ExecuteSqlCommand("delete from Reviews");
                context.Database.ExecuteSqlCommand("delete from TenderImages");
                context.Database.ExecuteSqlCommand("delete from UserCategories");
                context.Database.ExecuteSqlCommand("delete from Categories");
                context.Database.ExecuteSqlCommand("delete from Queries");
                context.Database.ExecuteSqlCommand("delete from Tenders");
                context.Database.ExecuteSqlCommand("delete from Users");
                
            }
        }
    }
}
