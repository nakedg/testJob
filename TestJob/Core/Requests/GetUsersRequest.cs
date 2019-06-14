using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestJob.Core.Requests
{
    public class GetUsersRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public SortDefenition Sort { get; set; }

        public string Filter { get; set; }
    }
}
