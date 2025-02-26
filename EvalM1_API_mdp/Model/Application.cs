namespace EvalM1_API_mdp.Model
{
    public class Application
    {
        public int IdApplication { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        // Relation One-to-One
        public Password Password { get; set; }
    }
}
