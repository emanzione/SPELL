using System.Collections.Generic;
using MHLab.Spells.Costs;
using MHLab.Spells.Definitions;
using MHLab.Spells.Effects;
using MHLab.Spells.Requirements;
using MHLab.Spells.Tests.Spells;
using MHLab.Spells.Tests.Spells.Effects;
using MHLab.Spells.Tests.Spells.Requirements;
using NUnit.Framework;

namespace MHLab.Spells.Tests.Tests
{
    public class RequirementsTest
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
            }

            protected override void AddRequirements(SpellRequirementContainer container)
            {
                container.Add<IsNotInCooldownSpellRequirement>();
                container.Add(new CasterLevelSpellRequirement(5));
                container.Add(new CasterHasEnoughManaSpellRequirement(10));
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
        public void Requirements_Met()
        {
            var caster = new MyPlayer()
            {
                Level = 5,
                Mana  = 10
            };

            var castResult = _context.CasterSystem.Cast(caster, _targets, _spell, out _);
            
            Assert.AreEqual(SpellCastState.Success, castResult.State);
        }
        
        [Test]
        public void All_Requirements_Not_Met()
        {
            var caster = new MyPlayer()
            {
                Level = 4,
                Mana = 5
            };

            var castResult = _context.CasterSystem.Cast(caster, _targets, _spell, out _);
            
            Assert.AreEqual(SpellCastState.RequirementsNotMet, castResult.State);
        }
        
        [Test]
        public void Partial_Requirements_Not_Met()
        {
            var caster = new MyPlayer()
            {
                Level = 10,
                Mana  = 5
            };

            var castResult = _context.CasterSystem.Cast(caster, _targets, _spell, out _);
            
            Assert.AreEqual(SpellCastState.RequirementsNotMet, castResult.State);
        }
    }
}