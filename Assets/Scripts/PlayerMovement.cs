using UnityEngine;

public enum WeaponType
{
    Punch,
    Machete,
    Sickle,
    Scythe,
    BigMachete
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;

    [Header("Combat Settings")]
    public WeaponType currentWeapon = WeaponType.Punch; // start with Punch

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    private Vector2 movement;
    private Vector2 lastMoveDir;
    private bool isAttacking = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isAttacking)
        {
            // Movement input
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            movement = new Vector2(h, v).normalized;

            bool isMoving = movement.sqrMagnitude > 0.001f;
            animator.SetBool("isWalking", isMoving);

            if (isMoving)
            {
                lastMoveDir = movement;
                HandleSpriteFlip(movement);
                animator.SetFloat("Horizontal", Mathf.Abs(movement.x));
                animator.SetFloat("Vertical", movement.y);
            }
            else
            {
                HandleSpriteFlip(lastMoveDir);
                animator.SetFloat("Horizontal", Mathf.Abs(lastMoveDir.x));
                animator.SetFloat("Vertical", lastMoveDir.y);
            }
        }

        // Attack input (left mouse click)
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            TriggerAttack();
        }
    }

    void FixedUpdate()
    {
        if (!isAttacking)
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    void HandleSpriteFlip(Vector2 dir)
    {
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            sr.flipX = dir.x < 0;
        else
            sr.flipX = false;
    }

    void TriggerAttack()
    {
        // Update animator params before attack
        animator.SetFloat("Horizontal", Mathf.Abs(lastMoveDir.x));
        animator.SetFloat("Vertical", lastMoveDir.y);
        HandleSpriteFlip(lastMoveDir);

        // Play correct animation based on weapon
        if (currentWeapon == WeaponType.Punch)
            animator.SetTrigger("Punch");
        else
            animator.SetTrigger("Slash");

        isAttacking = true;
    }

    // Called by Animation Event at end of attack
    public void EndAttack()
    {
        isAttacking = false;
    }
}
