using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Components
    Rigidbody2D rb;

    //Player
    float walkSpeed = 4f;
    float speedLimiter = 0.7f;
    float inputHorizontal;
    float inputVertical;

    // Animations & states
    Animator animator;
    string currentState;
    string idleState = PLAYER_DOWN;
    const string PLAYER_DOWN = "IdleDown";
    const string PLAYER_UP = "IdleUp";
    const string PLAYER_LEFT = "IdleLeft";
    const string PLAYER_RIGHT = "IdleRight";
    const string PLAYER_DOWN_MOVE = "WalkDown";
    const string PLAYER_UP_MOVE = "WalkUp";
    const string PLAYER_LEFT_MOVE = "WalkLeft";
    const string PLAYER_RIGHT_MOVE = "WalkRight";
    

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if (inputHorizontal != 0 || inputVertical != 0){
            if(inputHorizontal != 0 && inputVertical != 0){
                inputHorizontal *= speedLimiter;
                inputVertical *= speedLimiter;
            }

            rb.velocity = new UnityEngine.Vector2(inputHorizontal * walkSpeed, inputVertical * walkSpeed);

            if(inputHorizontal < 0) {
                idleState = PLAYER_LEFT;
                ChangeAnimationState(PLAYER_LEFT_MOVE);
            }
            else if(inputHorizontal > 0) {
                idleState = PLAYER_RIGHT;
                ChangeAnimationState(PLAYER_RIGHT_MOVE);
            }
            else if(inputVertical < 0) {
                idleState = PLAYER_DOWN;
                ChangeAnimationState(PLAYER_DOWN_MOVE);
            }
            else if(inputVertical > 0) {
                idleState = PLAYER_UP;
                ChangeAnimationState(PLAYER_UP_MOVE);
            }

        }
        else{
            rb.velocity = new UnityEngine.Vector2(0f, 0f);
            ChangeAnimationState(idleState); 
        }
    }

    // Animation state changer
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}


