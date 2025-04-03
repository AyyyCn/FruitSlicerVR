using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Game/LevelData")]
public class LevelData : ScriptableObject
{
    public string levelName = "Level 1";
    public float duration = 60f;
    public int targetFruitCount = 0;
    public float spawnInterval = 1f;
    public float bombChance = 0.2f;

    [Header("Scene")]
    public string sceneName; // Must match the scene name in Build Settings
}
