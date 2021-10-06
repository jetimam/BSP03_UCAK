using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovementScript : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;

    private float playerSpeed = 2;
    private float camHeight, camWidth, hitboxHeight, hitboxWidth;
    
    // Start is called before the first frame update
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        boxCollider = GetComponent<BoxCollider2D>();

        InitializeBounds(boxCollider);
    }

    // Update is called once per frame
    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(x, y, 0); //The movement vector

        PlayerRotate(movement);

        if (InCamera(camHeight, camWidth))
        {
            MoveAgent(movement);
        }
        else
        {
            WallPush(camHeight, camWidth);
        }
    }

    public void InitializeBounds(BoxCollider2D boxCollider)
    {
        Camera camera = Camera.main;

        hitboxHeight = boxCollider.size.x / 2; //length from the center to the edge

        camHeight = camera.orthographicSize - hitboxHeight; //length from the center to the edge
        camWidth = camHeight * camera.aspect;
    }

    public bool InCamera(float camHeight, float camWidth)
    {
        return (transform.position.y < camHeight &&
        transform.position.y > -camHeight &&
        transform.position.x < camWidth &&
        transform.position.x > -camWidth);
    }

    public void MoveAgent(Vector3 movement)
    {
        transform.Translate((movement * playerSpeed) * Time.deltaTime);
    }

    public void WallPush(float camHeight, float camWidth)
    {
        Vector3 wallVector;

            if (transform.position.y > camHeight || transform.position.y < -camHeight)
            {
                wallVector = new Vector3(0, 0 - transform.position.y, 0); //Pushes the player from the upper and lower walls
            }
            else
            {
                wallVector = new Vector3(0 - transform.position.x, 0, 0); //Pushes the player from the left and right walls
            }

            transform.Translate(wallVector * Time.deltaTime);
    }

    public void PlayerRotate(Vector3 movement)
    {
        if (movement.x > 0) //Checks if player is moving right.
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movement.x < 0) //Checks if player is moving left.
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}