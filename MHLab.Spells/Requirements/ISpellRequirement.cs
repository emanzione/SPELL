using System.Collections.Generic;
using MHLab.Spells.Definitions;

namespace MHLab.Spells.Requirements
{
    public interface ISpellRequirement
    {
        CheckRequirementResult IsMet(ISpellCaster caster, IEnumerable<ISpellTarget> targets, SpellDefinition spellDefinition, SpellsContext context);
    }
}