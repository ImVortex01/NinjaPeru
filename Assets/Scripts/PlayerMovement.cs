using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // < >
    private Rigidbody2D rBody;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { idle, running, jumping, falling}

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rBody.velocity = new Vector2(dirX * moveSpeed, rBody.velocity.y);

        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
           rBody.velocity = new Vector2(rBody.velocity.x, jumpForce);
        }
        
        AnimationState();
    }

    private void AnimationState()
    {
        MovementState state;

        if(dirX > 0f){
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if(dirX < 0f){
            state = MovementState.running;
            sprite.flipX = true;
        }
        else{
            state = MovementState.idle;
        }

        if(rBody.velocity.y > .1f){
            state = MovementState.jumping;
        }
        else if(rBody.velocity.y < -.1f){
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int) state);
    }

    private bool IsGrounded()
    {
       return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
