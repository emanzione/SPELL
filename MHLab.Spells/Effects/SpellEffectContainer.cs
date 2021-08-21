using System;
using System.Collections.Generic;

namespace MHLab.Spells.Effects
{
    public class SpellEffectContainer
    {
        internal List<ISpellEffect> Effects => _effects;

        private readonly List<ISpellEffect> _effects;

        internal SpellEffectContainer()
        {
            _effects = new List<ISpellEffect>();
        }
        
        public void Add(ISpellEffect effect)
        {
            _effects.Add(effect);
        }

        public void Add<TEffect>() where TEffect : ISpellEffect
        {
            var effect = Activator.CreateInstance<TEffect>();
            Add(effect);
        }

        public void Remove(ISpellEffect effect)
        {
            _effects.Remove(effect);
        }
    }
}