using System.Collections.Generic;
using MHLab.Spells.Costs;
using MHLab.Spells.Definitions;
using MHLab.Spells.Effects;
using MHLab.Spells.Requirements;
using MHLab.Spells.Tests.Spells.Effects;
using NUnit.Framework;

namespace MHLab.Spells.Tests.Tests
{
    public class BurnDamageEffectTest
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
                container.Add(new BurnDamageSpellEffect(10, 1));
            }
        }

        private MySpellsContext           _context;
        private IEnumerable<ISpellTarget> _targets;
        private Spell                     _spell;

        [SetUp]
        public void Setup()
        {
            _context = new MySpellsContext();
            _targets = new[]
            {
                new MyTarget() { Level = 3, HealthPoints = 50, Armor = 0 }
            };
            _spell = _context.Spells.Get<SimpleSpell>();
        }

        [Test]
        public void Correctly_Burn_The_Targets_For_1_Tick()
        {
            var caster = new MyPlayer();

            var castResult = _context.CasterSystem.Cast(caster, _targets, _spell, out _);
            Assert.AreEqual(SpellCastState.Success, castResult.State);

            _context.Update(1);

            foreach (var spellTarget in _targets)
            {
                var target = (MyTarget)spellTarget;
                Assert.AreEqual(49, target.HealthPoints);
            }
        }

        [Test]
        public void Correctly_Burn_The_Targets_For_5_Ticks()
        {
            var caster = new MyPlayer();

            var castResult = _context.CasterSystem.Cast(caster, _targets, _spell, out _);
            Assert.AreEqual(SpellCastState.Success, castResult.State);

            for (var i = 0; i < 5; i++)
                _context.Update(1);

            foreach (var spellTarget in _targets)
            {
                var target = (MyTarget)spellTarget;
                Assert.AreEqual(45, target.HealthPoints);
            }
        }

        [Test]
        public void Correctly_Clean_Up_After_10_Ticks()
        {
            var caster = new MyPlayer();

            var castResult = _context.CasterSystem.Cast(caster, _targets, _spell, out _);
            Assert.AreEqual(SpellCastState.Success, castResult.State);


            for (var i = 0; i < 20; i++)
                _context.Update(1);

            foreach (var spellTarget in _targets)
            {
                var target = (MyTarget)spellTarget;
                Assert.AreEqual(40, target.HealthPoints);
            }
        }
    }
}