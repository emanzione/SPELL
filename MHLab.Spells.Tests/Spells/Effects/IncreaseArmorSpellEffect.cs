using MHLab.Spells.Effects;
using MHLab.Spells.Instances;

namespace MHLab.Spells.Tests.Spells.Effects
{
    public class IncreaseArmorSpellEffect : ISpellEffect
    {
        private readonly float _duration;

        public IncreaseArmorSpellEffect(float durationInSeconds)
        {
            _duration = durationInSeconds;
        }

        public void Setup(SpellEffectInstance effectInstance)
        {
            effectInstance.RemainingDuration = _duration;
            
            var targets = effectInstance.SpellInstance.Targets;

            foreach (var spellTarget in targets)
            {
                var target = (MyTarget)spellTarget;
                target.Armor += 3;
            }
        }
        
        public SpellEffectContinuationState ApplyEffect(SpellEffectInstance effectInstance, float deltaTime)
        {
            if (effectInstance.RemainingDuration > 0f)
                return SpellEffectContinuationState.Continue;
            else
                return SpellEffectContinuationState.Complete;
        }

        public void CleanUp(SpellEffectInstance effectInstance)
        {
            var targets = effectInstance.SpellInstance.Targets;

            foreach (var spellTarget in targets)
            {
                var target = (MyTarget)spellTarget;
                target.Armor -= 3;
            }
        }
    }
}