using System.Collections.Generic;
using MHLab.Spells.Costs;
using MHLab.Spells.Definitions;
using MHLab.Spells.Effects;
using MHLab.Spells.Requirements;
using MHLab.Spells.Tests.Spells.Effects;
using NUnit.Framework;

namespace MHLab.Spells.Tests.Tests
{
    public class DelayedDamageEffectTest
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
            }

            public void AddEffects(SpellEffectContainer container)
            {
                container.Add<DelayedDamageSpellEffect>();
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
                new MyTarget() { HealthPoints = 50}
            };
            _spell = _context.Spells.Get<SimpleSpell>();
        }
        
        [Test]
        public void Correctly_Apply_Delayed_Effect()
        {
            var caster = new MyPlayer()
            {
                Damage = 10
            };

            var castResult = _context.CasterSystem.Cast(caster, _targets, _spell, out _);
            Assert.AreEqual(SpellCastState.Success, castResult.State);
            
            _context.Update(2);

            foreach (var spellTarget in _targets)
            {
                var enemy = (MyTarget)spellTarget;
                Assert.AreEqual(50, enemy.HealthPoints);
            }
            
            _context.Update(1);

            foreach (var spellTarget in _targets)
            {
                var enemy = (MyTarget)spellTarget;
                Assert.AreEqual(40, enemy.HealthPoints);
            }
        }
    }
}