using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RobotMovement : MonoBehaviour, IDropHandler, IBeginDragHandler, IEndDragHandler
{

    Rigidbody2D rb;
    public AudioSource moveSound;
    public AudioSource randomSound1;
    public AudioSource randomSound2;

    float walkSpeed = 4f;
    float speedLimiter = 0.8f;
    float moveTime = 0.5f;
    float moveTimeCounter;
    Vector2 movementDirection;

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
    public List<Item> robotInventory;
    public RobotOptionMenu optionMenuScript;
    private bool canMove = false;
    private bool isDragging = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!isDragging)
        {
            InventoryItem droppedItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            if (droppedItem != null)
            {
                robotInventory.Add(droppedItem.item);
                Debug.Log(droppedItem.item.name + " was dropped on the robot.");

                if (optionMenuScript != null)
                {
                    optionMenuScript.OpenSettingsWithItem(droppedItem.item.name, droppedItem.item.image, droppedItem);
                }
                else
                {
                    Debug.LogError("OptionMenuScript is not assigned!");
                }
            }
        }
    }

    public void allowMovement()
    {
        canMove = true;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        moveTimeCounter = moveTime;
        SetupAudioSources();
    }

    void SetupAudioSources()
    {
        ConfigureAudioSource(moveSound, 1.0f, 1.0f, 5.0f, AudioRolloffMode.Linear);
        ConfigureAudioSource(randomSound1, 1.0f, 1.0f, 5.0f, AudioRolloffMode.Linear);
        ConfigureAudioSource(randomSound2, 1.0f, 1.0f, 5.0f, AudioRolloffMode.Linear);
        StartCoroutine(PlayRandomSounds());
    }

    void ConfigureAudioSource(AudioSource source, float spatialBlend, float minDistance, float maxDistance, AudioRolloffMode rolloffMode)
    {
        source.spatialBlend = spatialBlend;
        source.minDistance = minDistance;
        source.maxDistance = maxDistance;
        source.rolloffMode = rolloffMode;
        source.playOnAwake = false;
    }

    private void Update()
    {
        if (canMove)
        {
            moveTimeCounter -= Time.deltaTime;
            if (moveTimeCounter <= 0)
            {
                ChooseNewDirection();
                moveTimeCounter = moveTime;
            }
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            MoveCharacter(movementDirection);
            if (rb.velocity.magnitude <= 0.1f)
            {
                moveSound.Stop();
            }
        }
        else
        {
            if (rb.velocity != Vector2.zero)
            {
                rb.velocity = Vector2.zero;
                ChangeAnimationState(idleState);
            }
        }
    }

    public void ToggleMovement()
    {
        canMove = !canMove;
        if (!canMove && moveSound.isPlaying)
        {
            moveSound.Stop();
        }
    }

    void MoveCharacter(Vector2 direction)
    {
        if (direction.magnitude > 0)
        {
            rb.velocity = direction * walkSpeed;
            if (!moveSound.isPlaying)
            {
                PlayStepSound();
            }
            UpdateAnimation(direction);
        }
        else
        {
            rb.velocity = Vector2.zero;
            if (moveSound.isPlaying)
            {
                moveSound.Stop();
            }
            ChangeAnimationState(idleState);
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

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }

    void PlayStepSound()
    {
        if (canMove && !moveSound.isPlaying)
        {
            moveSound.Play();
        }
    }

    IEnumerator PlayRandomSounds()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 10));
            if (canMove && Random.value > 0.5f)
            {
                if (!randomSound1.isPlaying && !randomSound2.isPlaying)
                {
                    AudioSource chosenSound = (Random.value > 0.5f) ? randomSound1 : randomSound2;
                    chosenSound.Play();
                }
            }
        }
    }
}