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
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationDAO _applicationDao;
        private readonly ILogger<ApplicationController> _logger; // Déclaration de ILogger

        public ApplicationController(IApplicationDAO applicationDao, ILogger<ApplicationController> logger)
        {
            _applicationDao = applicationDao;
            _logger = logger; // Initialisation du logger
        }

        [HttpGet("GetAllApp")]
        public async Task<ActionResult<IEnumerable<Application>>> GetApplications()
        {
            try
            {
                _logger.LogInformation("Tentative de récupération de toutes les applications.");
                var applications = await _applicationDao.GetAllApplicationsAsync();
                _logger.LogInformation("Toutes les applications ont été récupérées avec succès.");
                return Ok(applications);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération des applications.");
                return StatusCode(500, "Une erreur interne s'est produite.");
            }
        }

        [HttpPost("AddApp")]
        public async Task<ActionResult> AddApplication(Application application)
        {
            try
            {
                _logger.LogInformation("Tentative d'ajout d'une nouvelle application.");
                await _applicationDao.AddApplicationAsync(application);
                _logger.LogInformation($"Application ajoutée avec succès. ID: {application.IdApplication}");
                return CreatedAtAction(nameof(GetApplications), new { id = application.IdApplication }, application);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de l'ajout de l'application.");
                return StatusCode(500, "Une erreur interne s'est produite.");
            }
        }
    }
}
