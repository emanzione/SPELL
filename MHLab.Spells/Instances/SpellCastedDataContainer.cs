using System.Collections.Generic;

namespace MHLab.Spells.Instances
{
    public class SpellCastedDataContainer
    {
        internal Dictionary<ISpellCaster, SpellCastedData> DataMap => _dataMap;
        internal List<SpellCastedData>                     Data    => _data;

        private readonly Dictionary<ISpellCaster, SpellCastedData> _dataMap;
        private readonly List<SpellCastedData>                     _data;

        internal SpellCastedDataContainer()
        {
            _dataMap = new Dictionary<ISpellCaster, SpellCastedData>();
            _data    = new List<SpellCastedData>();
        }
        
        private void Add(ISpellCaster caster, SpellCastedData castedData)
        {
            if (_dataMap.ContainsKey(caster))
            {
                _dataMap[caster]        = castedData;
                _data[castedData.Index] = castedData;
            }
            else
            {
                castedData.Index = _data.Count;
                
                _dataMap.Add(caster, castedData);
                _data.Add(castedData);
            }
        }

        public void Remove(ISpellCaster caster)
        {
            if (_dataMap.TryGetValue(caster, out var castedData))
            {
                _dataMap.Remove(caster);
                _data.Remove(castedData);
            }
        }

        internal SpellCastedData EnsureSpellCastedData(ISpellCaster caster)
        {
            SpellCastedData castedData;

            if (TryGet(caster, out castedData) == false)
            {
                castedData = new SpellCastedData();
                Add(caster, castedData);
            }

            return castedData;
        }
        
        public bool TryGet(ISpellCaster caster, out SpellCastedData castedData)
        {
            return _dataMap.TryGetValue(caster, out castedData);
        }
    }
}