using MHLab.Spells.Instances;

namespace MHLab.Spells.Effects
{
    public interface ISpellEffect
    {
        /// <summary>
        /// Called when the spell effect is created and its state is Started.
        /// </summary>
        void Setup(SpellEffectInstance effectInstance);

        /// <summary>
        /// Called when the spell effect is in progress and its state is Progressing.
        /// </summary>
        SpellEffectContinuationState ApplyEffect(SpellEffectInstance effectInstance, float deltaTime);

        /// <summary>
        /// Called when the spell effect is completing and its state is Completed.
        /// </summary>
        void CleanUp(SpellEffectInstance effectInstance);
    }
}