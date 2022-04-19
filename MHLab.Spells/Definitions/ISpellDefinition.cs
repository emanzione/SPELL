using MHLab.Spells.Costs;
using MHLab.Spells.Effects;
using MHLab.Spells.Requirements;

namespace MHLab.Spells.Definitions
{
    public interface ISpellDefinition
    {
        void SetMetadata(Spell spell);

        void AddRequirements(SpellRequirementContainer container);

        void AddCosts(SpellCostContainer container);

        void AddEffects(SpellEffectContainer container);
    }
}