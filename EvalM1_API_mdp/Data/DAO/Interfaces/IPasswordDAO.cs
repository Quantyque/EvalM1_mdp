using EvalM1_API_mdp.Model;

namespace EvalM1_API_mdp.Data.DAO.Interfaces
{
    public interface IPasswordDAO
    {
        /// <summary>
        /// Permet d'avoir tous les mot de passes
        /// </summary>
        /// <returns>Retourne la liste de smots de passe</returns>
        Task<IEnumerable<Password>> GetAllPasswordsAsync();

        /// <summary>
        /// Permet d'ajouter un mot de passe
        /// </summary>
        /// <param name="password">Mot de passe à ajouter</param>
        /// <returns>Retourne la satisfaction de la requête</returns>
        Task AddPasswordAsync(Password password);

        /// <summary>
        /// Permet de supprimer un mot de passe
        /// </summary>
        /// <param name="passwordValue">Valeur du mot de passe</param>
        /// <returns>Retourne la satisfaction de la requête</returns>
        Task<bool> DeletePasswordAsync(string passwordValue);
    }
}
