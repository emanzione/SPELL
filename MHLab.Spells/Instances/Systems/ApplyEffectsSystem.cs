using MHLab.Spells.Effects;

namespace MHLab.Spells.Instances.Systems
{
    internal class ApplyEffectsSystem
    {
        private readonly SpellsContext _context;

        public ApplyEffectsSystem(SpellsContext context)
        {
            _context = context;
        }

        public void Update(SpellEffectInstance spellEffectInstance, float deltaTime)
        {
            if (spellEffectInstance.State == SpellEffectState.Started)
            {
                spellEffectInstance.Effect.Setup(spellEffectInstance);
                spellEffectInstance.State = SpellEffectState.Progressing;
            }

            if (spellEffectInstance.RemainingDelay > 0f)
            {
                spellEffectInstance.RemainingDelay -= deltaTime;
            }
            
            if (spellEffectInstance.RemainingDelay <= 0f)
            {
                spellEffectInstance.RemainingDuration -= deltaTime;

                spellEffectInstance.ContinuationState =
                    spellEffectInstance.Effect.ApplyEffect(spellEffectInstance, deltaTime);
            }
        }
    }
}