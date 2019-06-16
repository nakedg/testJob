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
                filter = filter.ToLower();
                query = query.Where(u => u.Email.ToLower().Contains(filter) || 
                                         u.FirstName.ToLower().Contains(filter) || 
                                         u.LastName.ToLower().Contains(filter) || 
                                         u.Login.ToLower().Contains(filter));
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

        public async Task<UserEntity> GetUserAsync(int id)
        {
            return await _ctx.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UserEntity> DeleteAsync(int id)
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                _ctx.Users.Remove(user);
                await _ctx.SaveChangesAsync();
                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<UserEntity> SaveAsync(UserEntity user)
        {
            if (user.Id > 0)
            {
                var entry = _ctx.Users.Attach(user);
                entry.State = EntityState.Modified;
            }
            else
            {
                //эмулируем автоинкремент для InMemory database
                var max = _ctx.Users.Max(u => u.Id);
                user.Id = max + 1;
                await _ctx.Users.AddAsync(user);
            }
            var res = await _ctx.SaveChangesAsync();
            return user;
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
