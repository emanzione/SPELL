using System.Collections.Generic;
using MHLab.Spells.Cooldowns;
using MHLab.Spells.Definitions;
using MHLab.Spells.Requirements;

namespace MHLab.Spells.Tests.Spells.Requirements
{
    public class IsNotInCooldownSpellRequirement : ISpellRequirement
    {
        private const string RequirementErrorCode = "SPELL_IS_IN_COOLDOWN";
        
        public CheckRequirementResult IsMet(ISpellCaster caster, IEnumerable<ISpellTarget> targets, SpellDefinition spellDefinition, SpellsContext context)
        {
            if (SpellCooldownHelper.IsInCooldown(context, caster, spellDefinition) == false)
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