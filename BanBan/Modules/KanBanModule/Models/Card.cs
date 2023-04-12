using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanBanModule.Models
{
    public class Card : ICard
    {
        public string Name { get; set; }
        public CardContent Content { get; set; } = new CardContent() { Description = "Default" };
    }

    public class CardContent
    {
        public string Description { get; set; }
    }
}
