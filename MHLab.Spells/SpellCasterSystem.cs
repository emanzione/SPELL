﻿using System.Collections.Generic;
using MHLab.Spells.Definitions;
using MHLab.Spells.Instances;
using MHLab.Spells.Requirements;

namespace MHLab.Spells
{
    public class CasterSystemOptions
    {
        public bool CheckRequirements { get; set; } = true;
        public bool ApplyCosts        { get; set; } = true;
    }

    public class SpellCasterSystem
    {
        public CasterSystemOptions Options { get; set; }

        private readonly SpellsContext _context;

        public SpellCasterSystem(SpellsContext context)
        {
            _context = context;

            Options = new CasterSystemOptions();
        }

        public SpellCastResult Cast(ISpellCaster caster, IEnumerable<ISpellTarget> targets, Spell spell,
                                    out SpellInstance spellInstance)
        {
            if (Options.CheckRequirements)
            {
                var checkRequirementsResult = spell.CheckRequirements(caster, targets, _context);
                if (checkRequirementsResult.Result == false)
                {
                    spellInstance = null;
                    return new SpellCastResult()
                    {
                        State                  = SpellCastState.RequirementsNotMet,
                        CheckRequirementResult = checkRequirementsResult
                    };
                }
            }

            if (Options.ApplyCosts)
            {
                spell.ApplyCosts(caster, targets);
            }

            spellInstance = new SpellInstance(spell, caster, targets);

            var spellCastedData = _context.CastedData.EnsureSpellCastedData(caster);

            CreateEffectInstances(spellInstance, spellCastedData);

            spellCastedData.SpellInstances.Add(spellInstance);

            return new SpellCastResult()
            {
                State = SpellCastState.Success,
                CheckRequirementResult = new CheckRequirementResult()
                {
                    Error  = null,
                    Result = true
                }
            };
        }

        public SpellCastResult Cast<TDefinition>(ISpellCaster      caster, IEnumerable<ISpellTarget> targets,
                                                 out SpellInstance spellInstance)
            where TDefinition : ISpellDefinition
        {
            var spell = _context.Spells.Get<TDefinition>();

            return Cast(caster, targets, spell, out spellInstance);
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