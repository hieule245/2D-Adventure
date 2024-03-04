using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private float dirX = 0f;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private LayerMask jumpableWall;
    [SerializeField] private float wallCoolDown;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 10f;
    private enum MovementState { idle, running, jumping, falling };
    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update() 
    {
        dirX = Input.GetAxisRaw("Horizontal");
        
        
        UpdateAnimationState();
        if ( wallCoolDown < 0.2f)
        {
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                jumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
            }
        }
    }

    private void UpdateAnimationState()
    {
        MovementState state;
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else state = MovementState.idle;

        if(rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        } 
        else if (rb.velocity.y < -.1f) 
        {
        state = MovementState.falling;
        } 
        anim.SetInteger("state",(int)state);
    }

    private bool IsGrounded()
    {
       return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private bool IsWall()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, new Vector2(transform.localScale.x,0), .1f, jumpableWall);
    }
}
