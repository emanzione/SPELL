using System;
using System.Collections.Generic;
using MHLab.Spells.Definitions.Exceptions;

namespace MHLab.Spells.Definitions
{
    public class SpellContainer
    {
        public List<Spell> Spells => _spells;
    
        private readonly List<Spell> _spells;

        internal SpellContainer()
        {
            _spells = new List<Spell>();
        }

        public void Add(ISpellDefinition definition)
        {
            var spell = new Spell(definition);
            _spells.Add(spell);
        }

        public void Add<TDefinition>() where TDefinition : ISpellDefinition
        {
            var definition = Activator.CreateInstance<TDefinition>();
            Add(definition);
        }

        public void Remove(Spell definition)
        {
            _spells.Remove(definition);
        }

        public Spell Get<TDefinition>() where TDefinition : ISpellDefinition
        {
            var definitionType = typeof(TDefinition);
            
            foreach (var spell in _spells)
            {
                if (spell.SpellDefinition.GetType() == definitionType)
                    return spell;
            }

            throw new SpellNotFoundException();
        }
    }
}