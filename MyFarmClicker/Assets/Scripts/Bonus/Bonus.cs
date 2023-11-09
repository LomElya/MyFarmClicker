using UnityEngine;

[System.Serializable]
public class Bonus
{
    [field: SerializeField] public int Level { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public float Profit { get; private set; }
    
}
