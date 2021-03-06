﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestJob.Core
{
    public class PaginationList<T>
    {
        public int TotalCount { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}
