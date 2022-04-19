using System.Collections.Generic;
using MHLab.Spells.Costs;
using MHLab.Spells.Effects;
using MHLab.Spells.Requirements;

namespace MHLab.Spells.Definitions
{
    public class Spell
    {
        public float Cooldown { get; set; }

        internal SpellEffectContainer Effects => _effects;
        
        private readonly SpellRequirementContainer _requirements;
        private readonly SpellCostContainer        _costs;
        private readonly SpellEffectContainer      _effects;

        public readonly ISpellDefinition SpellDefinition;

        public Spell(ISpellDefinition spellDefinition)
        {
            _requirements = new SpellRequirementContainer();
            _costs        = new SpellCostContainer();
            _effects      = new SpellEffectContainer();

            SpellDefinition = spellDefinition;
            
            Initialize();
        }

        private void Initialize()
        {
            SpellDefinition.SetMetadata(this);
            SpellDefinition.AddRequirements(_requirements);
            SpellDefinition.AddCosts(_costs);
            SpellDefinition.AddEffects(_effects);
        }

        internal CheckRequirementResult CheckRequirements(ISpellCaster caster, IEnumerable<ISpellTarget> targets, SpellsContext context)
        {
            foreach (var requirement in _requirements.Requirements)
            {
                var isMetResult = requirement.IsMet(caster, targets, this, context);
                if (isMetResult.Result == false)
                {
                    return isMetResult;
                }
            }

            return new CheckRequirementResult()
            {
                Error  = null,
                Result = true
            };
        }
        
        internal void ApplyCosts(ISpellCaster caster, IEnumerable<ISpellTarget> targets)
        {
            foreach (var cost in _costs.Costs)
            {
                cost.ApplyCost(caster, targets, this);
            }
        }
    }
}