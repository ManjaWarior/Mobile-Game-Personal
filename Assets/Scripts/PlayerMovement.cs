using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Touch touch;
    private Vector2 touchPos;
    private Quaternion rotY;
    private float speedModifier = 0.3f;
    [SerializeField] private GameObject lightObject = null;

    public bool emptyCharge = false;
    public static PlayerMovement instance;

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameController.GamePaused)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                if (!emptyCharge)
                {
                    lightObject.SetActive(true);
                }
                else
                {
                    lightObject.SetActive(false);//enables and disables the light object depending on whether the screen is touched or not
                }

                UIController.instance.usingCharge = true;

                if (touch.phase == TouchPhase.Moved)
                {
                    rotY = Quaternion.Euler(0f, touch.deltaPosition.x * speedModifier, 0f);
                    transform.rotation = rotY * transform.rotation;//rotates the player
                }
            }
            else
            {
                lightObject.SetActive(false);//turns off the flash light if there is no touch on screen
                UIController.instance.usingCharge = false;
            }

        }

    }

}


