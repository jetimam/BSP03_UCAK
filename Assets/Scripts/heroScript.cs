using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heroScript : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    private Vector3 movement;
    private float speed = 2;
    private float camHeight, camWidth;
    
    // Start is called before the first frame update
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        Camera camera = Camera.main;

        camHeight = 2 * camera.orthographicSize;
        camWidth = camHeight * camera.aspect;
    }

    // Update is called once per frame
    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        movement = new Vector3(x, y, 0); //The movement vector

        if (movement.x > 0) //Checks if player is moving right.
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movement.x < 0) //Checks if player is moving left.
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (transform.position.y < camHeight/2 && //Checks if the player is in the camera.
            transform.position.y > -camHeight/2 && 
            transform.position.x < camWidth/2  &&
            transform.position.x > -camWidth/2)
        {
            transform.Translate((movement * speed) * Time.deltaTime); //Moves the player.
        }
    }
}
