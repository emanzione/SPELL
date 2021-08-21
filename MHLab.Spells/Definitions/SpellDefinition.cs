using System.Collections.Generic;
using MHLab.Spells.Costs;
using MHLab.Spells.Effects;
using MHLab.Spells.Requirements;

namespace MHLab.Spells.Definitions
{
    public abstract class SpellDefinition
    {
        public float Cooldown { get; protected set; }

        internal SpellEffectContainer Effects => _effects;
        
        private readonly SpellRequirementContainer _requirements;
        private readonly SpellCostContainer        _costs;
        private readonly SpellEffectContainer      _effects;

        protected SpellDefinition()
        {
            _requirements = new SpellRequirementContainer();
            _costs        = new SpellCostContainer();
            _effects      = new SpellEffectContainer();
            
            Initialize();
        }

        private void Initialize()
        {
            SetMetadata();
            AddRequirements(_requirements);
            AddCosts(_costs);
            AddEffects(_effects);
        }

        protected abstract void SetMetadata();

        protected abstract void AddRequirements(SpellRequirementContainer container);

        protected abstract void AddCosts(SpellCostContainer container);

        protected abstract void AddEffects(SpellEffectContainer container);

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