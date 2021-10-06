using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovementScript : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;

    private float speed = 2;
    private float camHeight, camWidth, hitboxHeight, hitboxWidth;
    
    // Start is called before the first frame update
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        boxCollider = GetComponent<BoxCollider2D>();

        hitboxHeight = boxCollider.size.x / 2; //length from the center to the edge

        Camera camera = Camera.main;

        camHeight = camera.orthographicSize - hitboxHeight; //length from the center to the edge
        camWidth = camHeight * camera.aspect;
    }

    // Update is called once per frame
    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(x, y, 0); //The movement vector

        if (movement.x > 0) //Checks if player is moving right.
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movement.x < 0) //Checks if player is moving left.
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (transform.position.y < camHeight && //Checks if the player is in the camera.
            transform.position.y > -camHeight && 
            transform.position.x < camWidth  &&
            transform.position.x > -camWidth)
        {
            transform.Translate((movement * speed) * Time.deltaTime); //Moves the player.
        }
        else
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

            transform.Translate(wallVector * Time.deltaTime); //Moves the player
        }
    }
}