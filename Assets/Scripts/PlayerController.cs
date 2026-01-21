using UnityEngine;
using UnityEngine.InputSystem;

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



    // ------------ private ----------- //
    private BoxCollider2D collider;
    private Vector2 velocity;

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
        Move();
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
