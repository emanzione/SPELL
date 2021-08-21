using System.Collections.Generic;
using MHLab.Spells.Costs;
using MHLab.Spells.Definitions;
using MHLab.Spells.Effects;
using MHLab.Spells.Requirements;
using MHLab.Spells.Tests.Spells.Effects;
using MHLab.Spells.Tests.Spells.Requirements;
using NUnit.Framework;

namespace MHLab.Spells.Tests.Tests
{
    public class CooldownsTest
    {
        private class MySpellsContext : SpellsContext
        {
            protected override void RegisterSpells(SpellDefinitionContainer container)
            {
                container.Add<SimpleSpellDefinition>();
            }
        }
        
        private class SimpleSpellDefinition : SpellDefinition
        {
            protected override void SetMetadata()
            {
                Cooldown = 2;
            }

            protected override void AddRequirements(SpellRequirementContainer container)
            {
                container.Add<IsNotInCooldownSpellRequirement>();
            }

            protected override void AddCosts(SpellCostContainer container)
            {
            }

            protected override void AddEffects(SpellEffectContainer container)
            {
            }
        }
        
        private MySpellsContext           _context;
        private IEnumerable<ISpellTarget> _targets;
        private SpellDefinition           _spell;

        [SetUp]
        public void Setup()
        {
            _context = new MySpellsContext();
            _targets = new[]
            {
                new MyTarget()
            };
            _spell = _context.Definitions.Get<SimpleSpellDefinition>();
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