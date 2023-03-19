using System.Collections.ObjectModel;

namespace MainModule.Models
{
    public class Group
    {
        public string Name { get; set; }

        public ObservableCollection<ICard> Cards { get; set; }
    }
}
