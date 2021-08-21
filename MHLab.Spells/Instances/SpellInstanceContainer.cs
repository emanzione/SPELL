using System;
using System.Collections.Generic;
using MHLab.Spells.Definitions;

namespace MHLab.Spells.Instances
{
    public class SpellInstanceContainer
    {
        internal List<SpellInstance> Instances => _instances;

        private readonly List<SpellInstance> _instances;

        internal SpellInstanceContainer()
        {
            _instances = new List<SpellInstance>();
        }
        
        public void Add(SpellInstance instance)
        {
            _instances.Add(instance);
        }

        public void Add<TInstance>() where TInstance : SpellInstance
        {
            var instance = Activator.CreateInstance<TInstance>();
            Add(instance);
        }

        public void Remove(SpellInstance instance)
        {
            _instances.Remove(instance);
        }

        internal void RemoveAt(int index)
        {
            _instances.RemoveAt(index);
        }

        public bool TryGetFirst<TDefinition>(TDefinition definition, out SpellInstance instance) where TDefinition : SpellDefinition
        {
            var definitionType = typeof(TDefinition);

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
        
        public bool TryGetFirst(SpellDefinition definition, out SpellInstance instance)
        {
            var definitionType = definition.GetType();

            for (var i = _instances.Count - 1; i >= 0; i--)
            {
                var temporaryInstance = _instances[i];

                var instanceType = temporaryInstance.Definition.GetType();

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