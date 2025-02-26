using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]  // Permet d'utiliser l'attribut sur des classes ou des méthodes
public class ApiKeyAuthFilter : ActionFilterAttribute
{
    private const string ApiKeyHeaderName = "x-api-key"; // Nom du header

    private readonly string _apiKey; // Clé API à vérifier

    public ApiKeyAuthFilter(string apiKey)
    {
        _apiKey = apiKey;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Vérification de la présence du header x-api-key
        if (!context.HttpContext.Request.Headers.ContainsKey(ApiKeyHeaderName))
        {
            context.Result = new UnauthorizedResult(); // Retourner un code 401 si pas de clé
            return;
        }

        var providedApiKey = context.HttpContext.Request.Headers[ApiKeyHeaderName].FirstOrDefault();

        // Vérification si la clé fournie est correcte
        if (providedApiKey != _apiKey)
        {
            context.Result = new UnauthorizedResult(); // Retourner un code 401 si clé invalide
            return;
        }
    }

    // Pas besoin d'implémenter OnActionExecuted si vous n'avez pas de logique après l'action
}
