using MHLab.Spells.Effects;

namespace MHLab.Spells.Instances.Systems
{
    internal class HandleEffectsSystem
    {
        private readonly SpellsContext        _context;
        private readonly SetupEffectsSystem   _setupEffectsSystem;
        private readonly ApplyEffectsSystem   _applyEffectsSystem;
        private readonly CleanUpEffectsSystem _cleanUpEffectsSystem;

        public HandleEffectsSystem(SpellsContext context)
        {
            _context              = context;
            _setupEffectsSystem   = new SetupEffectsSystem(_context);
            _applyEffectsSystem   = new ApplyEffectsSystem(_context);
            _cleanUpEffectsSystem = new CleanUpEffectsSystem(_context);
        }

        public void Update(float deltaTime)
        {
            foreach (var spellCastedData in _context.CastedData.Data)
            {
                for (var i = spellCastedData.EffectInstances.Instances.Count - 1; i >= 0; i--)
                {
                    var spellEffectInstance = spellCastedData.EffectInstances.Instances[i];
                    
                    _setupEffectsSystem.Update(spellEffectInstance, deltaTime);

                    _applyEffectsSystem.Update(spellEffectInstance, deltaTime);
                    
                    _cleanUpEffectsSystem.Update(spellEffectInstance, deltaTime, spellCastedData, i);
                }
            }
        }
    }
}