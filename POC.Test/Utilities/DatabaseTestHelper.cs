using System;
using Microsoft.EntityFrameworkCore;
using POC.Core;

namespace POC.Test.Utilities
{
    public class DatabaseTestHelper
    {
        public static DbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<POCContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            return new POCContext(options);
        }
    }
}