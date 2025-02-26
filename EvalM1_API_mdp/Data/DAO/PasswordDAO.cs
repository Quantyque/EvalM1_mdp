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
        private readonly IApplicationDAO _applicationDao;
        private readonly PasswordEncryptionService _encryptionService;

        public PasswordDao(AppDbContext context, IApplicationDAO applicationDao, PasswordEncryptionService encryptionService)
        {
            _context = context;
            _applicationDao = applicationDao;
            _encryptionService = encryptionService;
        }

        public async Task<IEnumerable<Password>> GetAllPasswordsAsync()
        {
            return await _context.Passwords.ToListAsync();
        }

        public async Task AddPasswordAsync(Password password)
        {
            // Vérifier si l'application existe en recherchant dans les mots de passe existants
            var existingPassword = await _context.Passwords
                                                 .FirstOrDefaultAsync(p => p.IdApplication == password.IdApplication);
            if (existingPassword != null)
            {
                // Si un mot de passe existe déjà pour cette application, envoyer une erreur
                throw new InvalidOperationException("Cette application a déjà un mot de passe associé.");
            }

            // Vérifier si l'application existe
            var application = await _context.Applications
                                            .FirstOrDefaultAsync(a => a.IdApplication == password.IdApplication);

            if (application == null)
            {
                // Si l'application n'existe pas, envoyer une erreur
                throw new ArgumentException("L'application spécifiée n'existe pas.");
            }

            // Récupérer le type de l'application associée
            string appType = application.Type.TypeCode;

            // Chiffrement dynamique basé sur le type d'application
            string encryptedPassword = _encryptionService.EncryptPassword(password.PasswordValue, appType);

            // Sauvegarder le mot de passe chiffré dans la base de données
            password.PasswordValue = encryptedPassword;
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
