using EvalM1_API_mdp.DAO;
using EvalM1_API_mdp.Data.DAO.Interfaces;
using EvalM1_API_mdp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Ajout de l'espace de noms pour le logging
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EvalM1_API_mdp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly IPasswordDAO _passwordDao;
        private readonly ILogger<PasswordController> _logger; // Déclaration de ILogger

        public PasswordController(IPasswordDAO passwordDao, ILogger<PasswordController> logger)
        {
            _passwordDao = passwordDao;
            _logger = logger; // Initialisation du logger
        }

        [HttpGet("GetAllPassword")]
        public async Task<ActionResult<IEnumerable<Password>>> GetPasswords()
        {
            try
            {
                _logger.LogInformation("Tentative de récupération de tous les mots de passe.");
                var passwords = await _passwordDao.GetAllPasswordsAsync();
                _logger.LogInformation("Tous les mots de passe ont été récupérés avec succès.");
                return Ok(passwords);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération des mots de passe.");
                return StatusCode(500, "Une erreur interne s'est produite.");
            }
        }

        [HttpPost("AddPassword")]
        public async Task<ActionResult> AddPassword(Password password)
        {
            try
            {
                _logger.LogInformation("Tentative d'ajout d'un nouveau mot de passe.");
                await _passwordDao.AddPasswordAsync(password);
                _logger.LogInformation($"Mot de passe ajouté avec succès: {password.PasswordValue}");
                return CreatedAtAction(nameof(GetPasswords), new { id = password.PasswordValue }, password);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de l'ajout du mot de passe.");
                return StatusCode(500, "Une erreur interne s'est produite.");
            }
        }

        [HttpDelete("{passwordValue}")]
        public async Task<ActionResult> DeletePassword(string passwordValue)
        {
            try
            {
                _logger.LogInformation($"Tentative de suppression du mot de passe: {passwordValue}");
                var deleted = await _passwordDao.DeletePasswordAsync(passwordValue);
                if (!deleted)
                {
                    _logger.LogWarning($"Mot de passe {passwordValue} non trouvé.");
                    return NotFound();
                }

                _logger.LogInformation($"Mot de passe {passwordValue} supprimé avec succès.");
                return NoContent();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la suppression du mot de passe.");
                return StatusCode(500, "Une erreur interne s'est produite.");
            }
        }
    }
}
