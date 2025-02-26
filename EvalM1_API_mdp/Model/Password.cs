namespace EvalM1_API_mdp.Model
{
    public class Password
    {
        public int IdPassword { get; set; }
        public string PasswordValue { get; set; }
        public int IdApplication { get; set; }  // Clé étrangère vers Application
        public ICollection<Application> Applications { get; set; } // Relation un-à-plusieurs avec Application
    }
}
