using MHLab.Spells.Effects;
using MHLab.Spells.Instances;

namespace MHLab.Spells.Tests.Spells.Effects
{
    public class DamageSpellEffect : ISpellEffect
    {
        public void Setup(SpellEffectInstance effectInstance)
        {
        }

        public SpellEffectContinuationState ApplyEffect(SpellEffectInstance effectInstance, float deltaTime)
        {
            var player = (MyPlayer)effectInstance.SpellInstance.Caster;

            foreach (var target in effectInstance.SpellInstance.Targets)
            {
                var enemy = (MyTarget)target;
                enemy.HealthPoints -= player.Damage;
            }

            return SpellEffectContinuationState.Complete;
        }

        public void CleanUp(SpellEffectInstance effectInstance)
        {
        }
    }
}