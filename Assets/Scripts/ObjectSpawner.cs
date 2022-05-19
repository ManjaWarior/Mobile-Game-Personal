using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct spawnable
    {
        public string type;
        public float weight;//different chances of spawning different enemies
    }

    [System.Serializable]
    public struct spawnSettings
    {
        public string type;
        public float minimumWait;//minimum time to wait before spawning enemies
        public float maximumWait;//maximum time to wait before spawning enemies
        public int maxObjects;//prevents creating tonnes of objects and causing lag
    }//the above structs help us call one variable to use all the value inside

    private float totalWeight;//total weight of the spawnable objects
    private bool spawningObject = false;//checks if we are spawning an object 
    [SerializeField] private float groundSpawnDistance = 50f;

    public List<spawnable> enemySpawnables = new List<spawnable>();//allows us to create enemies with all the values in the list
    public List<spawnSettings> SpawnSettings = new List<spawnSettings>();

    public static ObjectSpawner instance;

    private void Awake()
    {
        instance = this;
        totalWeight = 0;
        foreach(spawnable spawnable in enemySpawnables)
        {
            totalWeight += spawnable.weight;//keeps track of the totalweight
        }
    }

    public void spawnGround()
    {
        ObjectPooler.instance.spawnFromPool("Ground", new Vector3(0, 0, groundSpawnDistance), Quaternion.identity);//60 is testing value, creates the object of type ground 
    }

    private IEnumerator spawnObject(string type, float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPooler.instance.spawnFromPool(type, new Vector3(Random.Range(-4.3f, 4.3f), 0.5f, -3.5f), Quaternion.identity);//creates a new enemy within this vector 3
        spawningObject = false;
        GameController.EnemyCount++;//keeps track of the number of enemies
    }

    void Update()
    {
        if(!spawningObject && GameController.EnemyCount < SpawnSettings[0].maxObjects && !GameController.GamePaused)//checks to make sure we aren't spawning other objects or over the maximum number of objects
        {
            spawningObject = true;//prevents constant spawning on top of each other
            float pick = Random.value * totalWeight;
            int chosenIndex = 0;
            float cumulativeWeight = enemySpawnables[0].weight;

            while(pick > cumulativeWeight && chosenIndex < enemySpawnables.Count - 1)
            {
                chosenIndex++;//moves to the next enemy in the list
                cumulativeWeight += enemySpawnables[chosenIndex].weight; //keeps track of weight and updates 
            }

            StartCoroutine(spawnObject(enemySpawnables[chosenIndex].type, Random.Range(SpawnSettings[0].minimumWait / GameController.DifficultyMultiplier, SpawnSettings[0].maximumWait / GameController.DifficultyMultiplier)));//spawns object based at different speeds based on how far the player has travelled
            {

            }
        }
    }
}

