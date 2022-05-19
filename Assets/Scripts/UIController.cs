using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private Image chargeImage = null;
    [SerializeField] private GameObject emptyImage = null;
    [SerializeField] private float timeOffSet = 2.0f;
    [SerializeField] private float TimeModifier = 4.0f;
    [SerializeField] private TextMeshProUGUI distanceText = null;

    [SerializeField] private GameObject startPanel = null;
    [SerializeField] private GameObject restartPanel = null;

    private float chargeValue = 1.0f;

    public bool usingCharge;
    public static UIController instance;

    void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if(!GameController.GamePaused)
        {
            GameController.Distance += Time.deltaTime * TimeModifier;
            distanceText.text = String.Format("{0:0m}", GameController.Distance);//allows us to display on the UI how far the player has travelled

        }
        if (usingCharge)
        {
            chargeValue = Mathf.Clamp01(chargeValue - (timeOffSet * Time.deltaTime));
            chargeImage.fillAmount = chargeValue;//reduces the amount of charge on the UI charge bar
        }
        else
        {
            chargeValue = Mathf.Clamp01(chargeValue + (timeOffSet * Time.deltaTime));
            chargeImage.fillAmount = chargeValue;//increases the amount of charge on the UI charge bar
        }

        if(chargeValue <= 0)
        {
            PlayerMovement.instance.emptyCharge = true;
            emptyImage.SetActive(true);//overlays the empty charge bar sprite when all charge has been used
        }
        else
        {
            PlayerMovement.instance.emptyCharge = false;
            emptyImage.SetActive(false);//disables the empty charge bar sprite from being used
        }
    }
    
    public void startGame()
    {
        startPanel.SetActive(false);
        GameController.GamePaused = false;//sets the start panel to unactive so the player cna see the game screen
    }

    public void restartGame()
    {
        restartPanel.SetActive(false);
        GameController.GamePaused = false;//sets the restart panel to unactive so the player cna see the game screen
    }

    public void endGame()
    {
        restartPanel.SetActive(true);//opens the restart panel
        GameController.GamePaused = true;//stops the game functions from carrying on after the player is dead
        GameController.Distance = 0;
        GameController.EnemyCount = 0;//resets all variables when the player dies
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.GetComponent<HealthComponent>().resetHealth();
            enemy.SetActive(false);//resets and disables all enemies
        }
    }
}
