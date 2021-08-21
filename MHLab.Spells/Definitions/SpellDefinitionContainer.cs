using System;
using System.Collections.Generic;
using MHLab.Spells.Definitions.Exceptions;

namespace MHLab.Spells.Definitions
{
    public class SpellDefinitionContainer
    {
        public List<SpellDefinition> Definitions => _definitions;
    
        private readonly List<SpellDefinition> _definitions;

        internal SpellDefinitionContainer()
        {
            _definitions = new List<SpellDefinition>();
        }

        public void Add(SpellDefinition definition)
        {
            _definitions.Add(definition);
        }

        public void Add<TDefinition>() where TDefinition : SpellDefinition
        {
            var definition = Activator.CreateInstance<TDefinition>();
            Add(definition);
        }

        public void Remove(SpellDefinition definition)
        {
            _definitions.Remove(definition);
        }

        public SpellDefinition Get<TDefinition>() where TDefinition : SpellDefinition
        {
            var definitionType = typeof(TDefinition);
            
            foreach (var definition in _definitions)
            {
                if (definition.GetType() == definitionType)
                    return definition;
            }

            throw new SpellNotFoundException();
        }
    }
}