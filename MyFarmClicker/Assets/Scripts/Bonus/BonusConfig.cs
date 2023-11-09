using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BonusConfig
{
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public ListBonus ListBonus { get; private set; }
    [field: SerializeField] public List<Bonus> Bonus { get; private set; }

}