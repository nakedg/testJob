using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace TestJob.Core
{
    public static class AddTestDataExtensions
    {
        public static void UseTestData(this IApplicationBuilder app, string pathToJsonData)
        {
            var json = System.IO.File.ReadAllText(pathToJsonData);

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Data.UserEntity[]>(json);

            var dbContext = app.ApplicationServices.GetService<Data.UserDbContext>();

            dbContext.Users.AddRange(data);

            dbContext.SaveChanges();
        }
    }
}
