using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PhysicsObject
{
    [Header("Attributes")]
    [SerializeField] private int attackPower = 5;
    [SerializeField] public int health = 100;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float maxSpeed;

    [Header("Raycasts")]
    [SerializeField] private LayerMask raycastLayerMask; //Which layers are visible to raycast 
    [SerializeField] private float raycastLength = 2;
    [SerializeField] private Vector2 raycastOffset;
    private RaycastHit2D rightLedgeRaycastHit;
    private RaycastHit2D leftLedgeRaycastHit;
    private RaycastHit2D rightWallRaycastHit;
    private RaycastHit2D leftWallRaycastHit;

    private int direction = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = new Vector2(maxSpeed * direction, 0);

        // check right side ledge 
        // Debug.DrawRay(new Vector2(transform.position.x + raycastOffset.x, transform.position.y + raycastOffset.y), Vector2.down*raycastLength, Color.white);
        rightLedgeRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x + 1, transform.position.y + raycastOffset.y), Vector2.down, raycastLength);
        if (rightLedgeRaycastHit.collider == null)
        {
            direction = -1;
        }

        // check left side ledge
        // Debug.DrawRay(new Vector2(transform.position.x - raycastOffset.x, transform.position.y - raycastOffset.y), Vector2.down*raycastLength, Color.green);
        leftLedgeRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x - 1, transform.position.y - raycastOffset.y), Vector2.down, raycastLength);
        if (leftLedgeRaycastHit.collider == null)
        {
            direction = 1;
        }


        // Check for right wall
        // Debug.DrawRay(new Vector2(transform.position.x, transform.position.y ), Vector2.right*raycastLength, Color.cyan);
        rightWallRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, raycastLength, raycastLayerMask);
        if (rightWallRaycastHit.collider != null)
        {
            direction = -1;
        }

        // Check for left wall
        // Debug.DrawRay(new Vector2(transform.position.x, transform.position.y ), Vector2.left*raycastLength, Color.cyan);
        leftWallRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, raycastLength, raycastLayerMask);
        if (leftWallRaycastHit.collider != null)
        {
            direction = 1;
        }

        // If health is <= 0, destroy
        if (health < 0)
        {
            Destroy(gameObject);
        }

    }

    // If Collide with player decrease player health
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject == NewPlayer.Instance.gameObject)
        {
            // Hurt Player 
            NewPlayer.Instance.health -= attackPower;
            NewPlayer.Instance.UpdateUI();
            direction = direction * -1;
        }
    }
}
