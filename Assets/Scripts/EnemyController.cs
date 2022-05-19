using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private HealthComponent health;

    private Transform player;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float range = 7.5f;

    [SerializeField] private float attackRange = 1f;

    public bool targetingPlayer = false;
    private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;//finds the Player object by looking for the player tag
        health = GetComponent<HealthComponent>();//sets the health of enemy at the start
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameController.GamePaused)//only runs when the game is unpaused
        {
            if (Vector3.Distance(transform.position, player.position) <= range)//checks if the player is within the sight range
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);//begins to move enemy towards the player
                transform.LookAt(player);//rotates the enemy to look at the player
                targetingPlayer = true;
            }
            else
            {
                targetingPlayer = false;
            }

            if(targetingPlayer && Vector3.Distance(transform.position, player.position) <= attackRange && !isAttacking)//check if the enemy is in range and isn't already attacking
            {
                isAttacking = true;
                StartCoroutine(Attack());//begins attacking routine
            }

        }

    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.01f);//waits a moment to attack
        player.GetComponent<HealthComponent>().updateHealth(-5f);//takes away 5 health from the player
        isAttacking = false;
        transform.position = new Vector3(Random.Range(-4.3f, 4.3f), 0.5f, -3.5f);
        transform.rotation = Quaternion.identity;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Weapon")
        {
            health.updateHealth(-1f);//if the enemy has collided with the player's weapon reduce their health by 1
        }
    }
}
