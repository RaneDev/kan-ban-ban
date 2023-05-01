using System.Collections.ObjectModel;

namespace KanBanModule.Models
{
    public class Group : GroupBase, IGroup
    {
        public string CardsCount => $"{Cards.Count - 1}";

        public ObservableCollection<ICard> Cards { get; set; } = new();
    }
}
