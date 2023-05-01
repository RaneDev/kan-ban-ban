namespace KanBanModule.Models
{
    public class Card : ICard
    {
        public string Name { get; set; }
        public int GroupIndex { get; set; } = 1;
        public CardContent Content { get; set; } = new CardContent() { Description = "Default" };
    }

    public class CardContent
    {
        public string Description { get; set; }
    }
}
