using EvalM1_API_mdp.Data.DAO.Interfaces;
using EvalM1_API_mdp.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EvalM1_API_mdp.DAO
{
    public class ApplicationDao : IApplicationDAO
    {
        private readonly AppDbContext _context;

        public ApplicationDao(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Application>> GetAllApplicationsAsync()
        {
            return await _context.Applications.Include(a => a.Password).ToListAsync();
        }

        public async Task AddApplicationAsync(Application application)
        {
            // Assurez-vous de récupérer l'Application en utilisant l'ID
            var applicationEntity = await _context.Applications.Include(a => a.Password)
                                                               .FirstOrDefaultAsync(a => a.IdApplication == application.IdApplication);
            if (applicationEntity == null)
            {
                throw new Exception("Pas d'applications trouvées");
            }

            // Créer et associer le mot de passe à l'application
            var password = new Password
            {
                PasswordValue = application.Password.PasswordValue,
                IdApplication = application.IdApplication
            };

            _context.Applications.Add(applicationEntity);
            _context.Passwords.Add(password);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Application>> GetApplicationById(int IdApp)
        {
            // Utilisation de LINQ pour filtrer l'application par ID
            var application = await _context.Applications
                .Include(a => a.Password) // Si vous voulez inclure les mots de passe associés à l'application
                .Where(a => a.IdApplication == IdApp) // Filtrer par l'ID
                .ToListAsync(); // Convertir en liste (car vous retournez IEnumerable)

            return application; // Retourner la liste contenant l'application
        }
    }
}
