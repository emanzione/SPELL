﻿using MHLab.Spells.Cooldowns.Systems;
using MHLab.Spells.Definitions;
using MHLab.Spells.Instances;
using MHLab.Spells.Instances.Systems;

namespace MHLab.Spells
{
    public class SpellsContext
    {
        public readonly SpellContainer           Spells;
        public readonly SpellCasterSystem        CasterSystem;
        public readonly SpellCastedDataContainer CastedData;

        private readonly CooldownSystem        _cooldownSystem;
        private readonly CleanUpInstanceSystem _cleanUpInstanceSystem;
        private readonly HandleEffectsSystem   _handleEffectsSystem;

        public SpellsContext()
        {
            Spells       = new SpellContainer();
            CasterSystem = new SpellCasterSystem(this);
            CastedData   = new SpellCastedDataContainer();

            _cooldownSystem        = new CooldownSystem(this);
            _cleanUpInstanceSystem = new CleanUpInstanceSystem(this);
            _handleEffectsSystem   = new HandleEffectsSystem(this);
        }

        public void Update(float deltaTime)
        {
            _cooldownSystem.Update(deltaTime);
            _cleanUpInstanceSystem.Update(deltaTime);
            _handleEffectsSystem.Update(deltaTime);
        }
    }
}