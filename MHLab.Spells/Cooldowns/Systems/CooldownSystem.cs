namespace MHLab.Spells.Cooldowns.Systems
{
    internal class CooldownSystem
    {
        private readonly SpellsContext _context;

        public CooldownSystem(SpellsContext context)
        {
            _context = context;
        }

        public void Update(float deltaTime)
        {
            foreach (var spellCastedData in _context.CastedData.Data)
            {
                foreach (var spellInstance in spellCastedData.SpellInstances.Instances)
                {
                    spellInstance.RemainingCooldown += deltaTime;
                }
            }
        }
    }
}