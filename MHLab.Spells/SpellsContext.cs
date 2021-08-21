using MHLab.Spells.Cooldowns.Systems;
using MHLab.Spells.Definitions;
using MHLab.Spells.Instances;
using MHLab.Spells.Instances.Systems;

namespace MHLab.Spells
{
    public abstract class SpellsContext
    {
        public readonly SpellDefinitionContainer Definitions;
        public readonly SpellCasterSystem        CasterSystem;
        public readonly SpellCastedDataContainer CastedData;

        private readonly CooldownSystem        _cooldownSystem;
        private readonly CleanUpInstanceSystem _cleanUpInstanceSystem;
        private readonly HandleEffectsSystem   _handleEffectsSystem;

        protected SpellsContext()
        {
            Definitions  = new SpellDefinitionContainer();
            CasterSystem = new SpellCasterSystem(this);
            CastedData   = new SpellCastedDataContainer();

            _cooldownSystem        = new CooldownSystem(this);
            _cleanUpInstanceSystem = new CleanUpInstanceSystem(this);
            _handleEffectsSystem   = new HandleEffectsSystem(this);

            Initialize();
        }

        private void Initialize()
        {
            RegisterSpells(Definitions);
        }

        protected abstract void RegisterSpells(SpellDefinitionContainer container);

        public void Update(float deltaTime)
        {
            _cooldownSystem.Update(deltaTime);
            _cleanUpInstanceSystem.Update(deltaTime);
            _handleEffectsSystem.Update(deltaTime);
        }
    }
}