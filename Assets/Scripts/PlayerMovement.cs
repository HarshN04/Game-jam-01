using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    private Vector2 movement;      // Current input
    private Vector2 lastMoveDir;   // Last non-zero movement direction

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 1. Get input
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        movement = new Vector2(h, v).normalized;

        // 2. Are we moving?
        bool isMoving = movement.sqrMagnitude > 0.001f;
        animator.SetBool("isWalking", isMoving);

        if (isMoving)
        {
            lastMoveDir = movement;

            float blendX = movement.x;
            float blendY = movement.y;

            // 3. Side animation handling
            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y)) // moving sideways
            {
                if (movement.x < 0f)
                {
                    sr.flipX = true;          // flip sprite for left
                    blendX = Mathf.Abs(movement.x); // keep Blend Tree selecting side-right
                }
                else
                {
                    sr.flipX = false;         // right side, normal
                    blendX = movement.x;
                }
            }
            else
            {
                sr.flipX = false;             // front/back, no flip
                blendX = movement.x;
            }

            // 4. Update Blend Tree parameters
            animator.SetFloat("Horizontal", blendX);
            animator.SetFloat("Vertical", blendY);
        }
        else
        {
            // Idle: preserve last facing direction
            float blendX = lastMoveDir.x;
            float blendY = lastMoveDir.y;

            if (Mathf.Abs(lastMoveDir.x) > Mathf.Abs(lastMoveDir.y))
            {
                sr.flipX = lastMoveDir.x < 0f;
                blendX = Mathf.Abs(lastMoveDir.x); // force side-right for Blend Tree
            }
            else
            {
                sr.flipX = false;
            }

            animator.SetFloat("Horizontal", blendX);
            animator.SetFloat("Vertical", blendY);
        }
    }

    void FixedUpdate()
    {
        // 5. Move player
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
