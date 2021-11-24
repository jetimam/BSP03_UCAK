using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;

    private Rigidbody2D rigidBody; //The components of the game object.
    private CircleCollider2D circleCollider;
    private SpriteRenderer spriteRenderer;

    private float camHeight, camWidth, hitboxHeight, hitboxWidth;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        InitializeBounds(circleCollider);
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(x, y, 0);
    
        transform.Translate((movement * playerSpeed) * Time.deltaTime);
    }

    public void InitializeBounds(CircleCollider2D circleCollider)
    {
        Camera camera = Camera.main;

        hitboxHeight = circleCollider.radius / 2; //length from the center to the edge

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
        if (movement.y > 0)
        {
            transform.Translate((movement * playerSpeed) * Time.deltaTime);
        }
        else
        {
            transform.Translate((movement * playerSpeed) * Time.deltaTime);
        }
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

    public void PlayerRotate(Vector3 movement, SpriteRenderer spriteRenderer)
    {
        spriteRenderer.flipX = (movement.x < 0);
    }
}