using MHLab.Spells.Effects;

namespace MHLab.Spells.Instances.Systems
{
    internal class SetupEffectsSystem
    {
        private readonly SpellsContext _context;

        public SetupEffectsSystem(SpellsContext context)
        {
            _context = context;
        }

        public void Update(SpellEffectInstance spellEffectInstance, float deltaTime)
        {
            if (spellEffectInstance.State != SpellEffectState.Started) return;
            
            spellEffectInstance.Effect.Setup(spellEffectInstance);
            spellEffectInstance.State = SpellEffectState.Progressing;
        }
    }
}