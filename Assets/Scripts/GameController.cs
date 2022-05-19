using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static float distance = 0;
    private static float difficultyMultiplier = 1;
    private static float difficultyOffset = 100f;
    private static int enemyCount = 0;

    private static bool gamePaused = true;

    public static float Distance { get => distance; set => distance = value; }
    public static float DifficultyMultiplier { get => difficultyMultiplier; set => difficultyMultiplier = value; }
    public static float DifficultyOffset { get => difficultyOffset; set => difficultyOffset = value; }
    public static int EnemyCount { get => enemyCount; set => enemyCount = value; }//this creates refernces too all of the private variables used here

    public static bool GamePaused { get => gamePaused; set => gamePaused = value; }

    public static GameController instance;

    void Awake()
    {
        instance = this;        
    }

    void Update()
    {
        if(!gamePaused)
        {
            difficultyMultiplier = 1 + (distance / difficultyOffset);//increases the dificulty every 100 metres               
        }
    }
}
