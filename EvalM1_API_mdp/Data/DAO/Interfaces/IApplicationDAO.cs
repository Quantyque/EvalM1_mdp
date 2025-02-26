using EvalM1_API_mdp.Model;

namespace EvalM1_API_mdp.Data.DAO.Interfaces
{
    public interface IApplicationDAO
    {
        /// <summary>
        /// Permet d'avoir toutes les applications
        /// </summary>
        /// <returns>Retourne la liste de toutes les applications</returns>
        Task<IEnumerable<Application>> GetAllApplicationsAsync();

        /// <summary>
        /// Permet d'avoir une application grâce à son identifiant
        /// </summary>
        /// <returns>Retourne l'application désirée</returns>
        Task<IEnumerable<Application>> GetApplicationById(int IdApp);

        /// <summary>
        /// Permet d'ajouter une application
        /// </summary>
        /// <param name="application">L'application à ajouter</param>
        /// <returns>Retourne la satisfaction de la requête</returns>
        Task AddApplicationAsync(Application application);
    }
}
