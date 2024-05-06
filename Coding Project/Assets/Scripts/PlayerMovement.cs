using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;

    float walkSpeed = 4f;
    float speedLimiter = 0.7f;
    float inputHorizontal;
    float inputVertical;

    Animator animator;
    string currentState;
    string idleState = PLAYER_DOWN;
    public const string PLAYER_DOWN = "IdleDown";
    public const string PLAYER_UP = "IdleUp";
    public const string PLAYER_LEFT = "IdleLeft";
    public const string PLAYER_RIGHT = "IdleRight";
    public const string PLAYER_DOWN_MOVE = "WalkDown";
    public const string PLAYER_UP_MOVE = "WalkUp";
    public const string PLAYER_LEFT_MOVE = "WalkLeft";
    public const string PLAYER_RIGHT_MOVE = "WalkRight";

    public AudioSource step1Sound;
    public AudioSource step2Sound;
    bool isStep1 = true;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (!enabled) return;
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
    }

    public string IdleState
    {
        get { return idleState; }
    }

    public static string CurrentDirection { get; private set; }

    void FixedUpdate()
    {
        if (inputHorizontal != 0 || inputVertical != 0)
        {
            if (inputHorizontal != 0 && inputVertical != 0)
            {
                inputHorizontal *= speedLimiter;
                inputVertical *= speedLimiter;
            }

            rb.velocity = new UnityEngine.Vector2(inputHorizontal * walkSpeed, inputVertical * walkSpeed);

            PlayStepSound();

            if (inputHorizontal < 0)
            {
                idleState = PLAYER_LEFT;
                ChangeAnimationState(PLAYER_LEFT_MOVE);
            }
            else if (inputHorizontal > 0)
            {
                idleState = PLAYER_RIGHT;
                ChangeAnimationState(PLAYER_RIGHT_MOVE);
            }
            else if (inputVertical < 0)
            {
                idleState = PLAYER_DOWN;
                ChangeAnimationState(PLAYER_DOWN_MOVE);
            }
            else if (inputVertical > 0)
            {
                idleState = PLAYER_UP;
                ChangeAnimationState(PLAYER_UP_MOVE);
            }
            CurrentDirection = idleState;
        }
        else
        {
            rb.velocity = new UnityEngine.Vector2(0f, 0f);
            ChangeAnimationState(idleState);
        }
    }

    void PlayStepSound()
    {

        if (isStep1 && !step1Sound.isPlaying)
        {
            step1Sound.Play();
        }
        else if (!isStep1 && !step2Sound.isPlaying)
        {
            step2Sound.Play();
        }
        isStep1 = !isStep1;
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        CurrentDirection = newState;

        animator.Play(newState);
        currentState = newState;
    }
}