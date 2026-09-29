using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.KitchenTicketRefactoring
{
    public class KitchenOrder
    {
        private readonly List<(string Item, List<string> Ingredients, int PrepMinutes)> _items = new();

        public IReadOnlyList<(string Item, List<string> Ingredients, int PrepMinutes)> Items => _items;

        public void AddItem(string item, IEnumerable<string> ingredients, int prepMinutes)
        {
            _items.Add((
                item,
                ingredients.Select(i => i.Trim().ToLowerInvariant()).ToList(),
                prepMinutes
            ));
        }
    }
}
