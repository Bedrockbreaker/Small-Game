using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement: MonoBehaviour
{
    public float speed = 3;
    public BoxCollider2D cameraBounds;
    public Camera theCamera;

    private Rigidbody2D rb;
    private BoxCollider2D cameraBox;
    private Vector2 minBounds;
    private Vector2 maxBounds;
    private float halfHeight;
    private float halfWidth;

	void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        cameraBox = gameObject.GetComponentInChildren<BoxCollider2D>();

        minBounds = cameraBounds.bounds.min;
        maxBounds = cameraBounds.bounds.max;
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
        cameraBox.size = new Vector2(Screen.width / 10, Screen.height / 10);

	}

    //Move the background when the player reaches a certain invisible box.
    //This keeps the player in the center of the screen, without the background always moving.
    void Update ()
    {

        //Movement
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed);

        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (Input.GetAxisRaw("Vertical") == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        

        float clampX = Mathf.Clamp(theCamera.transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampY = Mathf.Clamp(theCamera.transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
        theCamera.transform.position = new Vector3(clampX, clampY, theCamera.transform.position.z);
    }
}
