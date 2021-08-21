using System.Collections.Generic;
using MHLab.Spells.Definitions;
using MHLab.Spells.Requirements;

namespace MHLab.Spells.Tests.Spells.Requirements
{
    public class CasterLevelSpellRequirement : ISpellRequirement
    {
        private const string RequirementErrorCode = "CASTER_HAS_NOT_REQUIRED_LEVEL";

        private readonly uint RequiredLevel;

        public CasterLevelSpellRequirement(uint requiredLevel)
        {
            RequiredLevel = requiredLevel;
        }

        public CheckRequirementResult IsMet(ISpellCaster  caster, IEnumerable<ISpellTarget> targets, SpellDefinition spellDefinition,
                          SpellsContext context)
        {
            var player = (MyPlayer)caster;

            if (player.Level >= RequiredLevel)
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