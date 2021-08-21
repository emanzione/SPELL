using MHLab.Spells.Effects;

namespace MHLab.Spells.Instances
{
    public class SpellEffectInstance
    {
        public SpellEffectState State             { get; internal set; }
        public ISpellEffect     Effect            { get; internal set; }
        public SpellInstance    SpellInstance     { get; internal set; }
        public float            RemainingDuration { get; set; }
        public float            RemainingDelay    { get; set; }

        public SpellEffectContinuationState ContinuationState { get; internal set; }

        public object CustomData { get; set; }

        public SpellEffectInstance(ISpellEffect effect, SpellInstance spellInstance)
        {
            State             = SpellEffectState.Started;
            ContinuationState = SpellEffectContinuationState.Continue;
            Effect            = effect;
            SpellInstance     = spellInstance;
        }
    }
}