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
    const string PLAYER_DOWN = "Player_down";
    const string PLAYER_UP = "Player_up";
    const string PLAYER_LEFT = "Player_left";
    const string PLAYER_DOWN_MOVE = "Player_down_move";
    const string PLAYER_UP_MOVE = "Player_up_move";
    const string PLAYER_LEFT_MOVE = "Player_left_move";
    const string PLAYER_RIGHT = "Player_left2";
    const string PLAYER_RIGHT_MOVE = "Player_left_move2";

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

            rb.velocity = new Vector2(inputHorizontal * walkSpeed, inputVertical * walkSpeed);

            if(inputHorizontal < 0) {
                this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                idleState = PLAYER_LEFT;
                ChangeAnimationState(PLAYER_LEFT_MOVE);
            }
            else if(inputHorizontal > 0) {
                this.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
                idleState = PLAYER_LEFT;
                ChangeAnimationState(PLAYER_LEFT_MOVE);
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
            rb.velocity = new Vector2(0f, 0f);
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
