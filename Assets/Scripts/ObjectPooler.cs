using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]

    public class Pool
    {
        public string type;
        public GameObject prefab;
        public int size;
    }

    public static ObjectPooler instance;

    private void Awake()
    {
        instance = this;
    }

    public List<Pool> pools;

    public Dictionary<string, Queue<GameObject>> poolDictionary;//using a queue as it is more expandable and less demanding for a mobile platform

    GameObject objectToSpawn;
    void Start()//doing all this as the game begins will prevent any lag during gameplay
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>(); //sets the initial value of our dictionary

        foreach(Pool pool in  pools)//begins a loop for every item object pool
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i < pool.size; i++)//creates a loop until pool size has been filled with all the game objects needed
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);//creates the object with status false so the player can't see it
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.type, objectPool);//takes the type and sets the value to the queue of the pool
        }
    }


    public GameObject spawnFromPool(string type, Vector3 position, Quaternion rotation)//this function enables us to spawn objects from within the pool
    {
        if(!poolDictionary.ContainsKey(type))
        {
            Debug.LogWarning("Pool with type: " + type + " doesn't exist.");
            return null;//this check prevents any errors occuring by trying to create an object with a type that doesn't exist within the pool
        }

        objectToSpawn = poolDictionary[type].Dequeue();//removes the type from the queue
        objectToSpawn.SetActive(true);//all objects within pool are currently false so must be made true
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;//these 2 take the position and rotation passed and add them to the gameObject

        poolDictionary[type].Enqueue(objectToSpawn);//adds the new object back to the queue

        return objectToSpawn;//returns the object to be spawned 
    }
}
