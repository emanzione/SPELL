using System.Collections.Generic;
using MHLab.Spells.Costs;
using MHLab.Spells.Definitions;
using MHLab.Spells.Effects;
using MHLab.Spells.Requirements;
using MHLab.Spells.Tests.Spells.Requirements;
using NUnit.Framework;

namespace MHLab.Spells.Tests.Tests
{
    public class CooldownsTest
    {
        private class MySpellsContext : SpellsContext
        {
            protected override void RegisterSpells(SpellContainer container)
            {
                container.Add<SimpleSpell>();
            }
        }
        
        private class SimpleSpell : ISpellDefinition
        {
            public void SetMetadata(Spell spell)
            {
                spell.Cooldown = 2;
            }

            public void AddRequirements(SpellRequirementContainer container)
            {
                container.Add<IsNotInCooldownSpellRequirement>();
            }

            public void AddCosts(SpellCostContainer container)
            {
            }

            public void AddEffects(SpellEffectContainer container)
            {
            }
        }
        
        private MySpellsContext           _context;
        private IEnumerable<ISpellTarget> _targets;
        private Spell           _spell;

        [SetUp]
        public void Setup()
        {
            _context = new MySpellsContext();
            _targets = new[]
            {
                new MyTarget()
            };
            _spell = _context.Definitions.Get<SimpleSpell>();
        }
        
        [Test]
        public void Cannot_Recast_While_In_Cooldown()
        {
            var caster = new MyPlayer();

            var castResult = _context.CasterSystem.Cast(caster, _targets, _spell, out _);
            Assert.AreEqual(SpellCastState.Success, castResult.State);
            
            _context.Update(1);
            
            castResult = _context.CasterSystem.Cast(caster, _targets, _spell, out _);
            Assert.AreEqual(SpellCastState.RequirementsNotMet, castResult.State);
        }
        
        [Test]
        public void Can_Recast_After_Cooldown()
        {
            var caster = new MyPlayer();

            var castResult = _context.CasterSystem.Cast(caster, _targets, _spell, out _);
            Assert.AreEqual(SpellCastState.Success, castResult.State);
            
            _context.Update(3);
            
            castResult = _context.CasterSystem.Cast(caster, _targets, _spell, out _);
            Assert.AreEqual(SpellCastState.Success, castResult.State);
        }
    }
}