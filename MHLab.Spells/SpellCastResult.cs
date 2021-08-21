using MHLab.Spells.Requirements;

namespace MHLab.Spells
{
    public struct SpellCastResult
    {
        public SpellCastState         State;
        public CheckRequirementResult CheckRequirementResult;
    }
}