using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    GameObject player;
    [SerializeField] float movementSpeed;
    float vInput, hInput;

    // Start is called before the first frame update
    void Start()
    {    
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void Update() 
    {  
        vInput = joystick.Vertical * movementSpeed;
        hInput = joystick.Horizontal * movementSpeed;

        if (joystick.Vertical !=0 || joystick.Horizontal !=0)
        {
            //animator.SetBool("isWalking",true);
            if (joystick.Horizontal<0)
            {
                player.transform.localScale = new (-1,1,0);
            }
            if (joystick.Horizontal>0)
            {
                player.transform.localScale = new (1,1,0);
            }
        }
        else
        {
            //animator.SetBool("isWalking",false);
        }
        

        player.transform.Translate(hInput,vInput,0);
    }
}
