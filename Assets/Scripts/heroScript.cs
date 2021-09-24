using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heroScript : MonoBehaviour
{

    private BoxCollider2D boxCollider;

    private Vector3 moveDelta;
    
    // Start is called before the first frame update
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        moveDelta = new Vector3(x, y, 0); //The movement vector

        if (moveDelta.x > 0) //Checks if player is moving right.
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveDelta.x < 0) //Checks if player is moving left.
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        transform.Translate(moveDelta * Time.deltaTime); //Moves the player.

    }
}
