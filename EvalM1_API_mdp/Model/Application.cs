namespace EvalM1_API_mdp.Model
{
    public class Application
    {
        public int IdApplication { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TypeId { get; set; }
        public Type Type { get; set; }  // Navigation vers Type
        public Password Password { get; set; }  // Navigation vers Password
    }
}
