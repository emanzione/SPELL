using System.Collections.Generic;
using MHLab.Spells.Costs;
using MHLab.Spells.Definitions;

namespace MHLab.Spells.Tests.Spells.Costs
{
    public class ManaSpellCost : ISpellCost
    {
        private readonly uint _requiredMana;

        public ManaSpellCost(uint requiredMana)
        {
            _requiredMana = requiredMana;
        }
        
        public void ApplyCost(ISpellCaster caster, IEnumerable<ISpellTarget> targets, Spell spell)
        {
            var player = (MyPlayer)caster;

            player.Mana -= _requiredMana;
        }
    }
}