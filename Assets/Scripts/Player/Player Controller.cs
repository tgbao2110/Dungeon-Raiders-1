using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Joystick joystick;
    GameObject player;
    [SerializeField] float movementSpeed;
    float vInput, hInput;
    Rigidbody2D rb;
    Animator animator;

    // Start is called before the first frame update
    private void Awake() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
        animator = player.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update() 
    {  
        vInput = joystick.Vertical * movementSpeed;
        hInput = joystick.Horizontal * movementSpeed;

        if (joystick.Vertical !=0 || joystick.Horizontal !=0)
        {
            animator.SetBool("isWalking",true);
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
            animator.SetBool("isWalking",false);
        }
        
        
        rb.velocity = new UnityEngine.Vector2(hInput, vInput);
    }
}
