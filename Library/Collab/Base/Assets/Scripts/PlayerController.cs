using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // vars        
    public bool isNearStickyWall { get; set; }
    public bool isNearCurtain { get; set; }
    public bool isSpikey { get; set; }
    public float health { get; set; }
    public Vector3 velocity { get { return rigidbody2D.velocity; } }

    public float healthMax;
    public float movespGround;
    public float movespAir;
    public float jumpsp;
    public Vector2 wallJumpsp;
    public float springsp;
    public LayerMask platformsLayerMask;
    public LayerMask softLayerMask;
    public LayerMask springLayerMask;
    public PhysicsMaterial2D[] materials;
    public Animator potAnimator;

    internal Rigidbody2D rigidbodyStuckTo;

    new Rigidbody2D rigidbody2D;
    BoxCollider2D boxCollider2D;
    Animator animator;
    GameManager gm;

    // to prevent one-wall climbing, player cannot change direction for this time
    float wallJumpBufferStart = 0.5f;
    float wallJumpBuffer = 0f;

    bool alreadyDying;
    float movesp;
    float wallJumpDir;

    // controller input vars
    //PlayerControls controls;
    Vector2 move;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        transform.position = gm.checkpointPos;
    }

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        health = healthMax;
        isSpikey = false;
        isNearStickyWall = false;
        isNearCurtain = false;
        alreadyDying = false;
        movesp = movespGround;
        rigidbody2D.sharedMaterial = materials[0];
        wallJumpBuffer = 0f;

        /*
        controls = new PlayerControls();
        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
        controls.Gameplay.Jump.performed += ctx => Jump();
        controls.Gameplay.Spikey.performed += ctx => ToggleSpikey();
        */
    }

    void Update()
    {
        movesp = (IsGrounded()) ? movespGround : movespAir;

        HandleHealth();

        if (health > 0 && wallJumpBuffer <= 0)
        {
            if (Input.GetKeyUp(GameManager.instance.left) && Input.GetKeyUp(GameManager.instance.right))
                move.x = 0;
            else if (Input.GetKey(GameManager.instance.left))
                move.x = -1;
            else if (Input.GetKey(GameManager.instance.right))
                move.x = 1;
            else
                move.x = 0;

            HandleXMovement();

            if (Input.GetKeyDown(GameManager.instance.jump))
                Jump();
            if (Input.GetKeyDown(GameManager.instance.spikey))
                ToggleSpikey();
        }
        else if (health <= 0 && !alreadyDying)
        {
            GameManager.instance.LoseLife();
            alreadyDying = true;
        }

        // hop a little when on the spring and not jumping
        if (onSpring(0.05f) && !Input.GetKeyDown(GameManager.instance.jump))
        {
            float velocityX = rigidbody2D.velocity.x;
            float velocityY = 2f;
            Vector2 velocity = new Vector2(velocityX, velocityY);
            rigidbody2D.velocity = velocity;
        }

        // when stucking to walls, fall slowly and give change to wall jump
        if (isSpikey && rigidbodyStuckTo != null)
        {
            if (isNearStickyWall)
            {
                float velocityX = rigidbodyStuckTo.velocity.x;
                float velocityY = rigidbodyStuckTo.velocity.y;
                // wall jump
                if (Input.GetKeyDown(GameManager.instance.jump) && // either
                    ((onWall("right") && move.x < 0) ||
                    (onWall("left") && move.x > 0)))
                {
                    FindObjectOfType<AudioManager>().Play("Jump");
                    isNearStickyWall = false;
                    velocityX = move.x * wallJumpsp.x;
                    velocityY = wallJumpsp.y;
                    wallJumpBuffer = wallJumpBufferStart;
                    wallJumpDir = move.x;
                }

                Vector2 velocity = new Vector2(velocityX, velocityY);
                rigidbody2D.velocity = velocity;
            }
            else if (isNearCurtain)
            {
                // jump from curtain
                if (Input.GetKeyDown(GameManager.instance.jump))
                {

                    FindObjectOfType<AudioManager>().Play("Jump");
                    isNearCurtain = false;
                    float velocityX = move.x * wallJumpsp.y;
                    float velocityY = wallJumpsp.y;
                    Vector2 velocity = new Vector2(velocityX, velocityY);
                    rigidbody2D.velocity = velocity;
                }
                // hold onto curtain (and control the swing)
                else
                {
                    rigidbodyStuckTo.AddForce(Vector2.right * move.x * 25);
                    float positionX = rigidbodyStuckTo.transform.position.x;
                    float positionY = rigidbodyStuckTo.transform.position.y;
                    Vector2 position = new Vector2(positionX, positionY);
                    rigidbody2D.MovePosition(position);
                    rigidbody2D.velocity = Vector2.zero;
                }
            }
        }
        wallJumpBuffer = Mathf.Clamp(0, wallJumpBuffer -= Time.deltaTime, wallJumpBufferStart);
    }

    void HandleXMovement()
    {
        float velocityX = move.x * movesp;
        float velocityY = rigidbody2D.velocity.y;
        Vector2 velocity = new Vector2(velocityX, velocityY);
        rigidbody2D.velocity = velocity;
    }

    void HandleHealth()
    {
        // takes exactly 5 units to reach -9.8 velocity (gravity's constant)
        if (velocity.y <= -9.8f && IsGrounded() && !onSoftSurface())
        {
            rigidbody2D.velocity = Vector2.zero;
            health = Mathf.Max(0, health - 1);
            if (health > 0)
            {
                animator.SetTrigger("Hit");
                potAnimator.SetTrigger("Hit");
            }

            InGameUI.instance.SetHealthValue(health, healthMax);
        }

        if (health <= 0) animator.SetBool("isDead", true);
        else animator.SetBool("isDead", false);
    }

    void ToggleSpikey()
    {
        isSpikey = !isSpikey;
        animator.SetBool("isSpikey", isSpikey);
    }

    void Jump()
    {
        if (health <= 0) return; //no jump when dead


        float velocityX = rigidbody2D.velocity.x;
        float velocityY = rigidbody2D.velocity.y;
        float jumpBoost = 0;

        if (velocityY >= 9.8f) jumpBoost = 1;

        if (onSpring(0.5f))
        {
            FindObjectOfType<AudioManager>().Play("Jump");
            velocityY = springsp + jumpBoost;
        }
        else if (IsGrounded())
        {
            FindObjectOfType<AudioManager>().Play("Jump");
            velocityY = jumpsp;
        }

        Vector2 velocity = new Vector2(velocityX, velocityY);
        rigidbody2D.velocity = velocity;
    }

    /*
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }
    */

    bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(
            boxCollider2D.bounds.center, boxCollider2D.bounds.size,
            0f, Vector2.down, 0.05f, platformsLayerMask);
        return raycastHit2D.collider != null;
    }

    // returns true if player is one either left or right side of wall
    bool onWall(string side)
    {
        RaycastHit2D raycastHit2D;
        if (side == "Right" || side == "right")
        {
            raycastHit2D = Physics2D.BoxCast(
                boxCollider2D.bounds.center, boxCollider2D.bounds.size,
                0f, Vector2.right, 0.3f, platformsLayerMask);
        }
        else if (side == "Left" || side == "left")
        {
            raycastHit2D = Physics2D.BoxCast(
                boxCollider2D.bounds.center, boxCollider2D.bounds.size,
                0f, Vector2.left, 0.3f, platformsLayerMask);
        }
        else
        {
            Debug.LogWarning("onWall has unidentifiable argument: " + side + "\n" +
                             "Enter either \"Right\" or \"Left\"");
            return false;
        }

        return raycastHit2D.collider != null && raycastHit2D.collider.tag != "Pickup";
    }

    bool onSpring(float distance)
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(
            boxCollider2D.bounds.center, boxCollider2D.bounds.size,
            0f, Vector2.down, distance, springLayerMask);
        return raycastHit2D.collider != null;
    }

    // returns true if lands on a soft surface
    bool onSoftSurface()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(
            boxCollider2D.bounds.center, boxCollider2D.bounds.size,
            0f, Vector2.down, 0.05f, softLayerMask);
        return raycastHit2D.collider != null;
    }

}


















