using MHLab.Spells.Effects;

namespace MHLab.Spells.Instances.Systems
{
    internal class CleanUpEffectsSystem
    {
        private readonly SpellsContext _context;

        public CleanUpEffectsSystem(SpellsContext context)
        {
            _context = context;
        }

        public void Update(SpellEffectInstance spellEffectInstance, float deltaTime, SpellCastedData spellCastedData, int index)
        {
            if (spellEffectInstance.ContinuationState == SpellEffectContinuationState.Complete)
            {
                spellCastedData.EffectInstances.RemoveAt(index);
                spellEffectInstance.State = SpellEffectState.Completed;
                spellEffectInstance.Effect.CleanUp(spellEffectInstance);
            }
        }
    }
}