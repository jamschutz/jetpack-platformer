// taken from: https://roystan.net/articles/character-controller-2d/

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // =============================================================== //
    // ========  Variables  ========================================== //
    // =============================================================== //

    // ------------ public ----------- //
    public float speed = 9;
    public float walkAcceleration = 75;
    public float airAcceleration = 30;
    public float groundDeceleration = 70;
    public float jumpHeight = 4;
    public float gravity = 25;



    // ------------ private ----------- //
    private BoxCollider2D collider;
    private Vector2 velocity;
    private bool isGrounded;

    // helpers
    private readonly KeyCode[] leftKeys = { KeyCode.A, KeyCode.LeftArrow };
    private readonly KeyCode[] rightKeys = { KeyCode.D, KeyCode.RightArrow };
    private readonly KeyCode[] jumpKeys = { KeyCode.W, KeyCode.UpArrow, KeyCode.Space };





    // =============================================================== //
    // ========  Lifecycle Methods  ================================== //
    // =============================================================== //

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }


    private void Update()
    {
        Jump();
        ApplyGravity();
        Move();
        CheckCollision();
    }





    // =============================================================== //
    // ========  Main Methods  ======================================= //
    // =============================================================== //

    private void Move()
    {
        // read input
        float moveInput = GotInput(leftKeys)?  -1.0f : 0.0f;
        moveInput +=      GotInput(rightKeys)?  1.0f : 0.0f;

        // if we got move input, accelerate towards top speed
        if(moveInput != 0) {
            velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, walkAcceleration * Time.deltaTime);
        }
        // otherwise, decelerate to zero
        else {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, groundDeceleration * Time.deltaTime);
        }

        transform.Translate(velocity * Time.deltaTime);
    }


    private void Jump()
    {
        if(!isGrounded)
            return;

        velocity.y = 0;
        if(GotInput(jumpKeys, true)) {
            velocity.y = Mathf.Sqrt(2 * jumpHeight * gravity);
        }
    }


    private void ApplyGravity()
    {
        velocity.y -= gravity * Time.deltaTime;
    }


    // NOTE: for this to work, we set "Auto Sync Transforms" to ON under ProjectSettings > Physics2D
    private void CheckCollision()
    {
        // get all colliders we are colliding with
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, collider.size, 0);
        isGrounded = false;

        // and move ourselves out of collision for each one
        foreach(var hit in hits) {
            // ignore our own collider...
            if(hit == collider) continue;

            // get distance from us to collider
            var distance = hit.Distance(collider);
            // double check we are still overlapping (possible we fixed this in a previous iteration)
            if(distance.isOverlapped) {
                // move away from collider
                transform.Translate(distance.pointA - distance.pointB);

                // check grounded
                if(Vector2.Angle(distance.normal, Vector2.up) < 90 && velocity.y < 0)
                    isGrounded = true;
                // otherwise, hit an object, set x vel to 0
                else
                    velocity.x = 0;
            }
        }
    }
    





    // =============================================================== //
    // ========  Helper Methods  ===================================== //
    // =============================================================== //


    private bool GotInput(KeyCode[] input, bool getKeyDown = false)
    {
        for(int i = 0; i < input.Length; i++) {
            if(getKeyDown) {
                if(Input.GetKeyDown(input[i]))
                    return true;
            }
            else if(Input.GetKey(input[i])) {
                return true;
            }
        }

        return false;
    }
}
