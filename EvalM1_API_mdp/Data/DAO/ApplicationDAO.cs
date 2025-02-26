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
            // Vérifier si l'application existe déjà en base
            var existingApplication = await _context.Applications
                .Include(a => a.Password)
                .FirstOrDefaultAsync(a => a.IdApplication == application.IdApplication);

            if (existingApplication != null)
            {
                // Vérifier si l'application a déjà un mot de passe
                if (existingApplication.Password != null)
                {
                    throw new Exception("L'application existe déjà et possède un mot de passe.");
                }

                // Si l'application existe mais n'a pas de mot de passe, on l'ajoute
                existingApplication.Password = new Password
                {
                    PasswordValue = application.Password.PasswordValue,
                    IdApplication = existingApplication.IdApplication
                };
            }
            else
            {
                // Si l'application n'existe pas, on la crée avec son mot de passe
                var newApplication = new Application
                {
                    IdApplication = application.IdApplication,
                    Name = application.Name,
                    Description = application.Description,
                    TypeId = application.TypeId,
                    Password = new Password
                    {
                        PasswordValue = application.Password.PasswordValue
                        //IdApplication = application.IdApplication
                    }
                };

                _context.Applications.Add(newApplication);
            }

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
