using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    private Renderer rend;
    private GameObject player;
    private float healthCheck;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.Find("Cube");
        rend = player.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            if(gameObject.tag == "Enemy")//health controls for enemy
            {
                GameController.EnemyCount--;//lowers the count of enemies so it doesn't spawn 20 and then stop
                currentHealth = maxHealth;//resets the health
                gameObject.SetActive(false);//sets the object to not active
            }

            if(gameObject.tag == "Player")//health controls for player
            {
                currentHealth = maxHealth;
                UIController.instance.endGame();
            }
        }
        if (gameObject.tag == "Player")//changes player colour based on their health
        {
            healthCheck = currentHealth / 5;//makes the switch case statement possible
            switch (healthCheck)//switch case is more efficient than if and else if 
            {
                case 1: 
                    rend.material.SetColor("_Color", Color.red);
                    break;
                case 2:
                    rend.material.SetColor("_Color", new Color(1, 0.405365f, 0));
                    break;
                case 3:
                    rend.material.SetColor("_Color", new Color(1, 0.7287828f, 0));
                    break;
                case 4:
                    rend.material.SetColor("_Color", Color.yellow);
                    break;
                case 5:
                    rend.material.SetColor("_Color", Color.white);
                    break;
            }//changes the colour of the player according to their remaining health
        }
    }

    public void updateHealth(float amt)
    {
        currentHealth += amt;
    }

    public void resetHealth()
    {
        currentHealth = maxHealth;
    }
}
