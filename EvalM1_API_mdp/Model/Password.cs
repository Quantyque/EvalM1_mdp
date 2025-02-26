namespace EvalM1_API_mdp.Model
{
    public class Password
    {
        public int IdPassword { get; set; }
        public string PasswordValue { get; set; } // Clé primaire unique

        // Relation One-to-One avec Application
        public Application Application { get; set; }
        public int IdApplication { get; set; } // Clé étrangère de l'Application
    }
}
