using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float roomWidth = 18;

    private Transform player;

    private Vector2 roomBounds;
    private Vector2 currentRoomBoundsX;
    private Vector2 currentRoomBoundsY;


    private void Start()
    {
        // get player
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // calculate bounds
        roomBounds = new Vector2(roomWidth, (roomWidth / 4.0f) * 3.0f); // because we have a 4:3 aspect ratio, calculate height from width
        currentRoomBoundsX = new Vector2(-roomBounds.x, roomBounds.x);
        currentRoomBoundsY = new Vector2(-roomBounds.y, roomBounds.y);
    }


    private void Update()
    {
        // check left
        if(player.position.x < currentRoomBoundsX.x) {
            UpdateBounds(Vector2.left * roomBounds.x * 2);
        }
        // check right
        else if(player.position.x > currentRoomBoundsX.y) {
            Debug.Log("moving right!");
            UpdateBounds(Vector2.right * roomBounds.x * 2);
        }
        // check down
        else if(player.position.y < currentRoomBoundsY.x) {
            UpdateBounds(Vector2.down * roomBounds.y * 2);
        }
        // check up
        else if(player.position.y > currentRoomBoundsY.y) {
            UpdateBounds(Vector2.up * roomBounds.y * 2);
        }
    }


    private void UpdateBounds(Vector2 move)
    {
        // move camera
        transform.Translate(new Vector3(move.x, move.y, 0));

        // update our bounds
        currentRoomBoundsX += Vector2.one * move.x;
        currentRoomBoundsY += Vector2.one * move.y;
    }
}
