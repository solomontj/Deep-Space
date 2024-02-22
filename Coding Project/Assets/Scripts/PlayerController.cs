using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed; // Public variable to set the player's movement speed in the Unity Editor
    public bool isMoving; // A flag to check if the player is currently moving
    public Vector2 input; // A Vector2 to store the player's input direction

    private Animator animator;

    public LayerMask wallsLayer;
    public LayerMask doorsLayer;

    private void Awake() 
    {
        animator = GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame by Unity
    public void Update() 
    {
        // Check if the player is not currently moving
        if(!isMoving)
        {
            

            // Get horizontal and vertical input from the player (arrow keys or WASD) and store it in 'input'
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0)
            {
                input.y = 0;
            }

           // Debug.Log("This is the input.x" + input.x);
           // Debug.Log("This is the input.y" + input.y);

            
            // Check if there is any input (i.e., if the input vector is not zero)
            if(input != Vector2.zero)
            {
                
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);
                
                // Calculate the target position based on current position and input direction
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;
                                
                // Start the Move coroutine to move the player towards the target position
                if (IsWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
            }
            
        }

        animator.SetBool("isMoving", isMoving);
    }

    // Coroutine to move the player towards the target position
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        // Continue moving towards the target position until close enough (distance squared is less than a tiny value, Mathf.Epsilon)
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            // Move the player towards the target position using Translate, adjusting for movement speed and frame time
            //transform.Translate ((transform.up*Input.GetAxisRaw("Vertical") + transform.right*Input.GetAxisRaw("Horizontal")).normalized *moveSpeed*Time.deltaTime);
            // Wait for the next frame before continuing the loop
            yield return null;
        }
        
        // Once close to the target position, set the player's position to the exact target position to avoid overshooting
        transform.position = targetPos;
        
        isMoving = false; // Reset the isMoving flag to false since the movement is complete
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        // Collider for walls
        if (Physics2D.OverlapCircle(targetPos, 0.2f, wallsLayer) != null)
        {
            return false;
        }

        // Collider for doors
        if (Physics2D.OverlapCircle(targetPos, 0.2f, doorsLayer) != null)
        {
            return false;
        }
        
        return true;
    }

    
}
