using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using TestJob.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace TestJob.Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserDbContext _ctx;

        public UserService(UserDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<PaginationList<UserEntity>> GetUsersAsync(int page, int pageSize, SortDefenition sort, string filter)
        {
            var query = _ctx.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(u => u.Email.Contains(filter) || 
                                         u.FirstName.Contains(filter) || 
                                         u.LastName.Contains(filter) || 
                                         u.Login.Contains(filter));
            }

            int totalCount = await query.CountAsync();

            query = query.OrderBy(GetSortValue(sort));

            var items = await query
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToArrayAsync();

            return new PaginationList<UserEntity>
            {
                Items = items,
                TotalCount = totalCount
            };
            
        }

        private string GetSortValue(SortDefenition sort)
        {

            if (sort.IsDesc)
            {
                return $"{sort.FieldName} DESC";
            }
            else
            {
                return sort.FieldName;
            }
        }
    }
}
