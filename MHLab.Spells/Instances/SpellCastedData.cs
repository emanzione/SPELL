namespace MHLab.Spells.Instances
{
    public class SpellCastedData
    {
        public readonly SpellInstanceContainer       SpellInstances;
        public readonly SpellEffectInstanceContainer EffectInstances;

        internal int Index;
        
        public SpellCastedData()
        {
            SpellInstances  = new SpellInstanceContainer();
            EffectInstances = new SpellEffectInstanceContainer();
        }
    }
}