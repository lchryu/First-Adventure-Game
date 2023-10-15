using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
   
    private float dirX;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementsState { idle, running, jumping, falling}
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();    
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        UpdateAnimationState();
    }
    private void UpdateAnimationState()
    {
        MovementsState state;
        if (dirX > 0f)
        {
            state = MovementsState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementsState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementsState.idle;
        }
        if (rb.velocity.y > .1f)
        {
            state = MovementsState.jumping;
        } else if (rb.velocity.y < -.1f)
        { 
            state = MovementsState.falling;
        }
        anim.SetInteger("state", (int)state);
    }
}
