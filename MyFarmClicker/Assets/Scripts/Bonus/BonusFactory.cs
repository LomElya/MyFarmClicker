using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bonus", menuName = "Bonus/BonusFactory")]
public class BonusFactory : ScriptableObject
{
    [field: SerializeField] public List<BonusConfig> BonusConfig { get; private set; }

    
}
