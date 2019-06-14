using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestJob.Core.Data
{
    public class UserEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }
    }
}
