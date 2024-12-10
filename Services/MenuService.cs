using Microsoft.EntityFrameworkCore;
using Курсова_робота.Models.Entities;

namespace Курсова_робота.Services
{
    public interface IMenuService
    {
        Task<List<OperationEntity>> GetOperationsByRoleAsync(int roleId);
    }

    public class MenuService : IMenuService
    {
        private readonly DB _context;

        public MenuService(DB context)
        {
            _context = context;
        }

        public async Task<List<OperationEntity>> GetOperationsByRoleAsync(int roleId)
        {

            return await _context.RoleOperations.
                Where(r => r.IdRole == roleId)
                .Select(ro => ro.Operation)
                .ToListAsync();

        }
    }

}
