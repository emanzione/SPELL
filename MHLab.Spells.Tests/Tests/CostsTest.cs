using System.Collections.Generic;
using MHLab.Spells.Costs;
using MHLab.Spells.Definitions;
using MHLab.Spells.Effects;
using MHLab.Spells.Requirements;
using MHLab.Spells.Tests.Spells;
using MHLab.Spells.Tests.Spells.Costs;
using MHLab.Spells.Tests.Spells.Effects;
using MHLab.Spells.Tests.Spells.Requirements;
using NUnit.Framework;

namespace MHLab.Spells.Tests.Tests
{
    public class CostsTest
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
            }

            protected override void AddCosts(SpellCostContainer container)
            {
                container.Add(new ManaSpellCost(10));
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
        public void Apply_Costs()
        {
            var caster = new MyPlayer()
            {
                Mana  = 10
            };

            var castResult = _context.CasterSystem.Cast(caster, _targets, _spell, out _);
            
            Assert.AreEqual(SpellCastState.Success, castResult.State);
            Assert.AreEqual(0, caster.Mana);
        }
    }
}