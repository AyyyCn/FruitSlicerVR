using UnityEngine;

public class BombManager : MonoBehaviour
{
    public static BombManager Instance;

    public int maxHits = 3;
    private int currentHits = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void BombHit()
    {
        currentHits++;
        Debug.Log($"💣 Bomb hit! Total hits: {currentHits}/{maxHits}");

        if (currentHits >= maxHits)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("💀 Game Over! Too many bombs hit.");
        // TODO: Add UI feedback, restart, etc.
    }
}
