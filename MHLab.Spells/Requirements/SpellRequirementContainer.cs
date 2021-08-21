using System;
using System.Collections.Generic;
using MHLab.Spells.Requirements.Exceptions;

namespace MHLab.Spells.Requirements
{
    public class SpellRequirementContainer
    {
        internal IEnumerable<ISpellRequirement> Requirements => _requirements;

        private readonly List<ISpellRequirement> _requirements;

        internal SpellRequirementContainer()
        {
            _requirements = new List<ISpellRequirement>();
        }
        
        public void Add(ISpellRequirement requirement)
        {
            _requirements.Add(requirement);
        }

        public void Add<TRequirement>() where TRequirement : ISpellRequirement
        {
            var requirement = Activator.CreateInstance<TRequirement>();
            Add(requirement);
        }

        public void Remove(ISpellRequirement requirement)
        {
            _requirements.Remove(requirement);
        }
        
        public TRequirement Get<TRequirement>() where TRequirement : ISpellRequirement
        {
            var requirementType = typeof(TRequirement);
            
            foreach (var requirement in _requirements)
            {
                if (requirement.GetType() == requirementType)
                    return (TRequirement)requirement;
            }

            throw new RequirementNotFoundException();
        }
    }
}