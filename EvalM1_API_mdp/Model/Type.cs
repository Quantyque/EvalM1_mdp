using System.ComponentModel.DataAnnotations;

namespace EvalM1_API_mdp.Model
{
    public class Type
    {
        [Key]
        [MaxLength(3)] // Longueur de la clé primaire
        public string TypeCode { get; set; } // 'PRO' ou 'CLI'
        public ICollection<Application> Applications { get; set; } // Relation un-à-plusieurs avec Application
    }
}
