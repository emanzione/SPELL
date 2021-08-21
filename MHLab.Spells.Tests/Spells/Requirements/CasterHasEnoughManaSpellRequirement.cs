using System.Collections.Generic;
using MHLab.Spells.Definitions;
using MHLab.Spells.Requirements;

namespace MHLab.Spells.Tests.Spells.Requirements
{
    public class CasterHasEnoughManaSpellRequirement : ISpellRequirement
    {
        private const string RequirementErrorCode = "CASTER_HAS_NOT_ENOUGH_MANA";

        private readonly uint _requiredMana;

        public CasterHasEnoughManaSpellRequirement(uint requiredMana)
        {
            _requiredMana = requiredMana;
        }

        public CheckRequirementResult IsMet(ISpellCaster    caster,          IEnumerable<ISpellTarget> targets,
                                            SpellDefinition spellDefinition, SpellsContext             context)
        {
            var player = (MyPlayer)caster;

            if (player.Mana >= _requiredMana)
            {
                return new CheckRequirementResult()
                {
                    Result = true,
                    Error  = null
                };
            }

            return new CheckRequirementResult()
            {
                Result = false,
                Error  = RequirementErrorCode
            };
        }
    }
}