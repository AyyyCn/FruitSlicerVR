using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Level Settings")]
    public LevelData currentLevel;

    [Header("Runtime State")]
    public float timeRemaining;
    public bool gameRunning = false;

    [Header("Events")]
    public UnityEvent onGameStart;
    public UnityEvent onGameOver;

    public static LevelData selectedLevel;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        //currentLevel = selectedLevel;
        if (currentLevel == null)
        {
            Debug.LogError("No level assigned!");
            return;
        }
        StartLevel();
    }

    public void StartLevel()
    {
        if (currentLevel == null)
        {
            Debug.LogError("No level assigned!");
            return;
        }

        timeRemaining = currentLevel.duration;
        gameRunning = true;
        onGameStart?.Invoke();

        Debug.Log($"Starting {currentLevel.levelName}...");
    }

    private void Update()
    {
        if (!gameRunning) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            EndLevel();
        }
    }

    public void EndLevel()
    {
        gameRunning = false;
        onGameOver?.Invoke();
        Debug.Log("Level Complete!");
    }
}
