using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed;
    public bool isMoving;
    public Vector2 input;

    private Animator animator;

    public LayerMask wallsLayer;
    public LayerMask doorsLayer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Update()
    {

        if (!isMoving)
        {

            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0)
            {
                input.y = 0;
            }

            if (input != Vector2.zero)
            {

                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
            }

        }

        animator.SetBool("isMoving", isMoving);
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            yield return null;
        }

        transform.position = targetPos;

        isMoving = false;
    }

    private bool IsWalkable(Vector3 targetPos)
    {

        if (Physics2D.OverlapCircle(targetPos, 0.2f, wallsLayer) != null)
        {
            return false;
        }

        if (Physics2D.OverlapCircle(targetPos, 0.2f, doorsLayer) != null)
        {
            return false;
        }

        return true;
    }

}