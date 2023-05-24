using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PhysicsObject
{
    [SerializeField] private float maxSpeed;
    private int direction = 1;

    // raycast
    private RaycastHit2D rightLedgeRaycastHit;
    private RaycastHit2D leftLedgeRaycastHit;
    private RaycastHit2D rightWallRaycastHit;
    private RaycastHit2D leftWallRaycastHit;
    [SerializeField] private LayerMask raycastLayerMask;
    [SerializeField] private Vector2 raycastOffset; 
    [SerializeField] private float raycastLength = 2; 

    [SerializeField] private int attackPower = 5;
    [SerializeField] public int health = 100;
    [SerializeField] private int maxHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = new Vector2(maxSpeed*direction, 0);

        // check right side ledge 
        rightLedgeRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x + 1, transform.position.y + raycastOffset.y), Vector2.down, raycastLength);
        Debug.DrawRay(new Vector2(transform.position.x + raycastOffset.x, transform.position.y + raycastOffset.y), Vector2.down*raycastLength, Color.white);

        if (rightLedgeRaycastHit.collider == null) {
            direction = -1;
        } 

        // check left side ledge
        leftLedgeRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x - 1, transform.position.y - raycastOffset.y), Vector2.down, raycastLength);
        Debug.DrawRay(new Vector2(transform.position.x - raycastOffset.x, transform.position.y - raycastOffset.y), Vector2.down*raycastLength, Color.green);

        if (leftLedgeRaycastHit.collider == null) {
            direction = 1;
        } 

        // Check for right wall
        rightWallRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, raycastLength, raycastLayerMask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y ), Vector2.right*raycastLength, Color.cyan);

        if (rightWallRaycastHit.collider != null) {
            direction = -1;
        }

        // Check for left wall
        leftWallRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, raycastLength, raycastLayerMask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y ), Vector2.left*raycastLength, Color.cyan);

        if (leftWallRaycastHit.collider != null) {
            direction = 1;
        }

        // If health is <= 0, destroy
        if (health < 0) {
            Destroy(gameObject);
        }

    }

    // If Collide with player decrease player health
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject == NewPlayer.Instance.gameObject) {
            Debug.Log("hurt");
            NewPlayer.Instance.health -= attackPower;
            NewPlayer.Instance.UpdateUI();
            direction = direction * -1;
        }
    }
}
