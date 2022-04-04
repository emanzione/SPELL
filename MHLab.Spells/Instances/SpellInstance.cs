using System.Collections.Generic;
using MHLab.Spells.Definitions;

namespace MHLab.Spells.Instances
{
    public enum SpellInstanceState
    {
        Starting,
        Progressing,
        Completed
    }
    
    public class SpellInstance
    {
        public SpellInstanceState        State             { get; internal set; }
        public Spell           Definition        { get; internal set; }
        public float                     RemainingCooldown { get; internal set; }
        public ISpellCaster              Caster            { get; internal set; }
        public IEnumerable<ISpellTarget> Targets           { get; internal set; }

        public SpellInstance(Spell definition, ISpellCaster caster, IEnumerable<ISpellTarget> targets)
        {
            Definition        = definition;
            State             = SpellInstanceState.Starting;
            RemainingCooldown = 0;
            Caster            = caster;
            Targets           = targets;
        }

        public bool IsInCooldown()
        {
            return RemainingCooldown < Definition.Cooldown;
        }
    }
}