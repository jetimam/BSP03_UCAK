using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heroScript : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    private Vector3 movement;
    private float speed = 1;
    
    // Start is called before the first frame update
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        movement = new Vector3(x, y, 0); //The movement vector
        rigidBody.velocity = movement * speed;

        if (movement.x > 0) //Checks if player is moving right.
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movement.x < 0) //Checks if player is moving left.
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        transform.Translate(movement * Time.deltaTime); //Moves the player.

        //4.850 IS THE CAMERA BOUNDARY POSITION

    }
}
