using System.Collections.Generic;
using MHLab.Spells.Definitions;

namespace MHLab.Spells.Costs
{
    public interface ISpellCost
    {
        void ApplyCost(ISpellCaster caster, IEnumerable<ISpellTarget> targets, SpellDefinition spellDefinition);
    }
}