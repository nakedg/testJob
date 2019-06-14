using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestJob.Core.Data
{
    public class UserDbContext: DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            :base(options)
        {

        }

        public DbSet<UserEntity> Users { get; set; }
    }
}
