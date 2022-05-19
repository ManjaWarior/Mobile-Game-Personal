using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour//this script will allow the ground to move
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float objectDistance = -40f;
    [SerializeField] private float despawnDistance = -110f;
    private bool canSpawnGround = true;

    private Rigidbody rb;

    private HealthComponent health;
    private GameObject player;
    private EnemyController enemy;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(GetComponent<HealthComponent>() != null)
        {
            health = GetComponent<HealthComponent>();//sets the health
        }
        player = GameObject.FindGameObjectWithTag("Player");//finds the player object by finding it's tag
        if (GetComponent<EnemyController>() != null)//these for health and enemy prevent the ground being assigned a health and enemy 
        {
            enemy = GetComponent<EnemyController>();
        }
    }

    void Update()
    {
        if(!GameController.GamePaused)
        {
            if (enemy != null && !enemy.targetingPlayer)
            {
                transform.position += -transform.forward * speed * Time.deltaTime;
            }
            else if (enemy != null && enemy.targetingPlayer)
            {
                transform.position += Vector3.zero;//the above 2 check if the component is an enemy and if so changes it's move pattern
            }
            else
            {
                transform.position += -transform.forward * speed * Time.deltaTime;//this makes the ground move backward to give the impression we are moving foward
            }

            if (transform.position.z < player.transform.position.z - 10f && enemy != null)
            {
                health.resetHealth();//makes sure the health returns to the max health
                gameObject.SetActive(false);
            }

            if (transform.position.z <= objectDistance && transform.tag == "Ground" && canSpawnGround)//checcks the distance, the tag and if we can spawn the ground
            {
                ObjectSpawner.instance.spawnGround();//creates an instance of objectspawner and calls it's function to spawn the ground
                canSpawnGround = false; //prevents ground being endlessly spawned
            }

            if (transform.position.z <= despawnDistance)//checks if the ground is off screen enough to be disabled
            {
                canSpawnGround = true;//allows us to spawn more ground
                gameObject.SetActive(false);//dsiables the current ground
            }
        }

    }
}
