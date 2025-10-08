using UnityEngine;

[CreateAssetMenu(fileName = "AIConfig", menuName = "Game/AI Config")]
public class AIConfig : ScriptableObject
{
    private static AIConfig _instance;
    public static AIConfig Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<AIConfig>("AIConfig");
                if (_instance == null)
                    Debug.LogError("AIConfig not found in Resources!");
            }
            return _instance;
        }
    }

    [Header("Chicken AI")]
    public float ChickenRunRange = 5f;
    public float ChickenChaseRange = 10f;
    public float ChickenMinSpeed = 3.5f;
    public float ChickenMaxSpeed = 5f;

    [Header("Dog AI")]
    public float DogChaseRange = 15f;
    public float DogBarkingDistance = 3f;
    public float DogStopChaseDistance = 20f;
    public float DogAttackRange = 1.5f;

    [Header("State Timers")]
    public float ChickenEatTime = 5f;
    public float ChickenIdleTime = 3f;
    public float ChickenWalkTime = 10f;
    public float DogIdleTime = 3f;
    public float DogSitTime = 5f;
    public float DogPatrolTime = 30f;
}
