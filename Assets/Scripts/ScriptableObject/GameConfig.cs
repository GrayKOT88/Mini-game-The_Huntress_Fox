using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Game/Game Config")]
public class GameConfig : ScriptableObject
{
    [Header("Player Settings")]
    public int PlayerMaxHealth = 15;
    public float PlayerSpeed = 5f;
    public float PlayerRotationSpeed = 0.5f;

    [Header("Bounds")]
    public Vector2 XBounds = new Vector2(-50, 50);
    public Vector2 ZBounds = new Vector2(-50, 50);

    [Header("Collision Effects")]
    public int DamageFromDog = 5;
    public int HealthFromChicken = 1;
    public int ScorePerChicken = 1;
}
