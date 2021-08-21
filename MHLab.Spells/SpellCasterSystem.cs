using System.Collections.Generic;
using MHLab.Spells.Definitions;
using MHLab.Spells.Instances;
using MHLab.Spells.Requirements;

namespace MHLab.Spells
{
    public class SpellCasterSystem
    {
        private readonly SpellsContext _context;

        public SpellCasterSystem(SpellsContext context)
        {
            _context = context;
        }
        
        public SpellCastResult Cast(ISpellCaster caster, IEnumerable<ISpellTarget> targets, SpellDefinition definition, out SpellInstance spellInstance)
        {
            var checkRequirementsResult = definition.CheckRequirements(caster, targets, _context);
            if (checkRequirementsResult.Result == false)
            {
                spellInstance = null;
                return new SpellCastResult()
                {
                    State                  = SpellCastState.RequirementsNotMet,
                    CheckRequirementResult = checkRequirementsResult
                };
            }
            
            definition.ApplyCosts(caster, targets);
            
            spellInstance = new SpellInstance(definition, caster, targets);

            var spellCastedData = _context.CastedData.EnsureSpellCastedData(caster);
            
            CreateEffectInstances(spellInstance, spellCastedData);
            
            spellCastedData.SpellInstances.Add(spellInstance);

            return new SpellCastResult()
            {
                State                  = SpellCastState.Success,
                CheckRequirementResult = new CheckRequirementResult()
                {
                    Error = null,
                    Result = true
                }
            };
        }

        private void CreateEffectInstances(SpellInstance spellInstance, SpellCastedData spellCastedData)
        {
            var effects = spellInstance.Definition.Effects.Effects;

            foreach (var spellEffect in effects)
            {
                spellCastedData.EffectInstances.Add(new SpellEffectInstance(spellEffect, spellInstance));
            }
        }
    }
}