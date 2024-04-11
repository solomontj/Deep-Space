using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    // Components
    Rigidbody2D rb;
    public AudioSource moveSound;
    public AudioSource randomSound1;  // First random sound
    public AudioSource randomSound2;  // Second random sound

    // Player
    float walkSpeed = 4f;
    float speedLimiter = 0.8f;
    float moveTime = 0.5f;
    float moveTimeCounter;
    Vector2 movementDirection;

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
        moveTimeCounter = moveTime;
        ChooseNewDirection();

        // Set the AudioSource to 3D sound
        moveSound.spatialBlend = 1.0f; // Set to 1.0 for full 3D sound effect

        // Set the proximity at which the sound is heard at full volume
        moveSound.minDistance = 1.0f; // Set as needed

        // Set the maximum distance at which the sound can be heard
        moveSound.maxDistance = 5.0f; // Set as needed

        // Set how audio attenuates with distance
        moveSound.rolloffMode = AudioRolloffMode.Linear; // Or choose another mode as needed
        randomSound1.spatialBlend = 1.0f;
        randomSound2.spatialBlend = 1.0f;

        randomSound1.rolloffMode = AudioRolloffMode.Linear;
        randomSound2.rolloffMode = AudioRolloffMode.Linear;

        randomSound1.minDistance = 1.0f;
        randomSound2.minDistance = 1.0f;

        randomSound1.maxDistance = 5.0f;
        randomSound2.maxDistance = 5.0f;

        randomSound1.playOnAwake = false;
        randomSound2.playOnAwake = false;
        StartCoroutine(PlayRandomSounds());
    }

    // Update is called once per frame
    void Update()
    {
        moveTimeCounter -= Time.deltaTime;
        if (moveTimeCounter <= 0)
        {
            ChooseNewDirection();
            moveTimeCounter = moveTime;
        }
    }

    void FixedUpdate()
    {
        MoveCharacter(movementDirection);
        // Check if the robot has stopped moving
        if (rb.velocity.magnitude <= 0.1f)
        {
            if (moveSound.isPlaying)
            {
                moveSound.Stop();
            }
        }
    }

    void MoveCharacter(Vector2 direction)
    {
        if (direction.x != 0 || direction.y != 0)
        {
            rb.velocity = new Vector2(direction.x * walkSpeed, direction.y * walkSpeed);
            PlayStepSound();
            UpdateAnimation(direction);
        }
        else
        {
            rb.velocity = Vector2.zero;
            ChangeAnimationState(idleState);
        }
    }


    void PlayStepSound()
    {
        if (!moveSound.isPlaying)
        {
            moveSound.Play();
        }
    }


    void UpdateAnimation(Vector2 direction)
    {
        if (direction.x < 0)
        {
            idleState = PLAYER_LEFT;
            ChangeAnimationState(PLAYER_LEFT_MOVE);
        }
        else if (direction.x > 0)
        {
            idleState = PLAYER_RIGHT;
            ChangeAnimationState(PLAYER_RIGHT_MOVE);
        }
        else if (direction.y < 0)
        {
            idleState = PLAYER_DOWN;
            ChangeAnimationState(PLAYER_DOWN_MOVE);
        }
        else if (direction.y > 0)
        {
            idleState = PLAYER_UP;
            ChangeAnimationState(PLAYER_UP_MOVE);
        }
    }

    void ChooseNewDirection()
    {
        Vector2 newDirection = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2)).normalized;
        if (newDirection.x != 0 && newDirection.y != 0)
        {
            newDirection.x *= speedLimiter;
            newDirection.y *= speedLimiter;
        }
        movementDirection = newDirection;
    }

    // Animation state changer
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }

    IEnumerator PlayRandomSounds()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 1));  // Random delay between 20 to 40 seconds
            if (Random.value > 0.5f)
                randomSound1.Play();
            else
                randomSound2.Play();
        }
    }
}