using EvalM1_API_mdp.Data.DAO.Interfaces;
using EvalM1_API_mdp.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EvalM1_API_mdp.DAO
{
    public class PasswordDao : IPasswordDAO
    {
        private readonly AppDbContext _context;

        public PasswordDao(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Password>> GetAllPasswordsAsync()
        {
            return await _context.Passwords.ToListAsync();
        }

        public async Task AddPasswordAsync(Password password)
        {
            _context.Passwords.Add(password);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletePasswordAsync(string passwordValue)
        {
            var password = await _context.Passwords.FindAsync(passwordValue);
            if (password == null)
                return false;

            _context.Passwords.Remove(password);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
