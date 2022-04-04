using System.Collections.Generic;
using MHLab.Spells.Costs;
using MHLab.Spells.Definitions;
using MHLab.Spells.Effects;
using MHLab.Spells.Requirements;
using MHLab.Spells.Tests.Spells.Effects;
using NUnit.Framework;

namespace MHLab.Spells.Tests.Tests
{
    public class DamageEffectTest
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
            }

            public void AddRequirements(SpellRequirementContainer container)
            {
            }

            public void AddCosts(SpellCostContainer container)
            {
            }

            public void AddEffects(SpellEffectContainer container)
            {
                container.Add<DamageSpellEffect>();
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
            _spell = _context.Definitions.Get<SimpleSpell>();
        }
        
        [Test]
        public void Correctly_Apply_Damage_Effect()
        {
            var caster = new MyPlayer()
            {
                Damage = 10
            };

            var castResult = _context.CasterSystem.Cast(caster, _targets, _spell, out _);
            Assert.AreEqual(SpellCastState.Success, castResult.State);
            
            _context.Update(1);

            foreach (var spellTarget in _targets)
            {
                var enemy = (MyTarget)spellTarget;
                Assert.AreEqual(40, enemy.HealthPoints);
            }
        }
        
        [Test]
        public void Damage_Effect_Is_Applied_Only_Once()
        {
            var caster = new MyPlayer()
            {
                Damage = 10
            };

            var castResult = _context.CasterSystem.Cast(caster, _targets, _spell, out _);
            Assert.AreEqual(SpellCastState.Success, castResult.State);
            
            _context.Update(1);
            _context.Update(1);

            foreach (var spellTarget in _targets)
            {
                var enemy = (MyTarget)spellTarget;
                Assert.AreEqual(40, enemy.HealthPoints);
            }
        }
    }
}