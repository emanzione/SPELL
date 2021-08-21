using MHLab.Spells.Effects;
using MHLab.Spells.Instances;

namespace MHLab.Spells.Tests.Spells.Effects
{
    public class BurnDamageSpellEffect : ISpellEffect
    {
        private class BurnDamageCustomData
        {
            public float CurrentTimer;
        }

        private readonly float _duration;
        private readonly float _tickInterval;

        public BurnDamageSpellEffect(float duration, float tickInterval)
        {
            _duration     = duration;
            _tickInterval = tickInterval;
        }
        
        public void Setup(SpellEffectInstance effectInstance)
        {
            effectInstance.RemainingDuration = _duration;
            effectInstance.CustomData        = new BurnDamageCustomData();
        }

        public SpellEffectContinuationState ApplyEffect(SpellEffectInstance effectInstance, float deltaTime)
        {
            var customData = (BurnDamageCustomData)effectInstance.CustomData;

            customData.CurrentTimer += deltaTime;

            if (customData.CurrentTimer >= _tickInterval)
            {
                foreach (var target in effectInstance.SpellInstance.Targets)
                {
                    var enemy = (MyTarget)target;
                    enemy.HealthPoints -= 1;
                }
                
                customData.CurrentTimer = 0;
            }
            
            if (effectInstance.RemainingDuration > 0f)
                return SpellEffectContinuationState.Continue;

            return SpellEffectContinuationState.Complete;
        }

        public void CleanUp(SpellEffectInstance effectInstance)
        {
        }
    }
}