using System;
using System.Collections.Generic;
using MHLab.Spells.Effects;

namespace MHLab.Spells.Instances
{
    public class SpellEffectInstanceContainer
    {
        internal List<SpellEffectInstance> Instances => _instances;

        private readonly List<SpellEffectInstance> _instances;

        internal SpellEffectInstanceContainer()
        {
            _instances = new List<SpellEffectInstance>();
        }
        
        public void Add(SpellEffectInstance instance)
        {
            _instances.Add(instance);
        }

        public void Add<TInstance>() where TInstance : SpellEffectInstance
        {
            var instance = Activator.CreateInstance<TInstance>();
            Add(instance);
        }

        public void Remove(SpellEffectInstance instance)
        {
            _instances.Remove(instance);
        }

        internal void RemoveAt(int index)
        {
            _instances.RemoveAt(index);
        }

        public bool TryGetFirst<TEffect>(TEffect definition, out SpellEffectInstance instance) where TEffect : ISpellEffect
        {
            var definitionType = typeof(TEffect);

            for (var i = _instances.Count - 1; i >= 0; i--)
            {
                var temporaryInstance = _instances[i];

                var instanceType = temporaryInstance.GetType();

                if (instanceType == definitionType)
                {
                    instance = temporaryInstance;
                    return true;
                }
            }

            instance = null;
            return false;
        }
        
        public bool TryGetFirst(ISpellEffect definition, out SpellEffectInstance instance)
        {
            var definitionType = definition.GetType();

            for (var i = _instances.Count - 1; i >= 0; i--)
            {
                var temporaryInstance = _instances[i];

                var instanceType = temporaryInstance.Effect.GetType();

                if (instanceType == definitionType)
                {
                    instance = temporaryInstance;
                    return true;
                }
            }

            instance = null;
            return false;
        }
    }
}