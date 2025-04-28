namespace TopScorer1_6.Models.IdentityModels
{
    //таблица от базата TopScorerRegister1.1
    public class Subscriber
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime SubscriptionDate { get; set; }
    }
}
