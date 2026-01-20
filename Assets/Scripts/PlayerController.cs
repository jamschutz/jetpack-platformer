using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // public
    public float speed;

    // components
    private Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        void Move(Vector2 dir)
        {
            rb.linearVelocity += dir * speed;
        }

        // reset movement
        rb.linearVelocity = Vector2.zero;

        // get input, and move
        if(Input.GetKey(KeyCode.A)) Move(Vector2.left);
        if(Input.GetKey(KeyCode.D)) Move(Vector2.right);
    }
}
