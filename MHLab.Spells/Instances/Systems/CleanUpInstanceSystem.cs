namespace MHLab.Spells.Instances.Systems
{
    public class CleanUpInstanceSystem
    {
        private readonly SpellsContext _context;

        public CleanUpInstanceSystem(SpellsContext context)
        {
            _context = context;
        }

        public void Update(float deltaTime)
        {
            foreach (var spellCastedData in _context.CastedData.Data)
            {
                for (var i = spellCastedData.SpellInstances.Instances.Count - 1; i >= 0; i--)
                {
                    var spellInstance = spellCastedData.SpellInstances.Instances[i];

                    if (spellInstance.IsInCooldown() == false)
                    {
                        spellCastedData.SpellInstances.RemoveAt(i);
                    }
                }
            }
        }
    }
}