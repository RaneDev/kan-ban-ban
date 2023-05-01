namespace KanBanModule.Models
{
    internal class CardAdder : ICard
    {
        public string Name { get; set; }
        public int GroupIndex { get; set; } = 1;

    }
}
