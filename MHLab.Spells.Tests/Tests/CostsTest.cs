using System.Collections.Generic;
using MHLab.Spells.Costs;
using MHLab.Spells.Definitions;
using MHLab.Spells.Effects;
using MHLab.Spells.Requirements;
using MHLab.Spells.Tests.Spells.Costs;
using NUnit.Framework;

namespace MHLab.Spells.Tests.Tests
{
    public class CostsTest
    {
        private class MySpellsContext : SpellsContext
        {
            public MySpellsContext() : base()
            {
                Spells.Add<SimpleSpell>();
            }
        }
        
        private class SimpleSpell : ISpellDefinition
        {
            public void SetMetadata(Spell spell)
            {
            }

            public void AddRequirements(SpellRequirementContainer container)
            {
            }

            public void AddCosts(SpellCostContainer container)
            {
                container.Add(new ManaSpellCost(10));
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
            _spell = _context.Spells.Get<SimpleSpell>();
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