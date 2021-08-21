using System;
using System.Collections.Generic;

namespace MHLab.Spells.Costs
{
    public class SpellCostContainer
    {
        internal IEnumerable<ISpellCost> Costs => _costs;

        private readonly List<ISpellCost> _costs;

        internal SpellCostContainer()
        {
            _costs = new List<ISpellCost>();
        }
        
        public void Add(ISpellCost cost)
        {
            _costs.Add(cost);
        }

        public void Add<TCost>() where TCost : ISpellCost
        {
            var cost = Activator.CreateInstance<TCost>();
            Add(cost);
        }

        public void Remove(ISpellCost cost)
        {
            _costs.Remove(cost);
        }
    }
}