using UnityEngine;

[CreateAssetMenu(fileName = "PoolConfig", menuName = "Game/Pool Config")]
public class PoolConfig : ScriptableObject
{
    [Header("Spawning")]
    public string ChickenPoolTag = "Chicken";
    public int MaxChickens = 10;
    public float SpawnCheckInterval = 10f;
}
