using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewPlayer : PhysicsObject
{
    [SerializeField] private float maxSpeed = 1;
    [SerializeField] private float jumpPower = 10;
    [SerializeField] public int coinsCollected = 0;
    [SerializeField] public int health = 100;
    private  Vector2 healthBarOrigSize;

    public Text coinsText;
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        coinsText = GameObject.Find("CoinsCounter").GetComponent<Text>();

        // find health bar UI size
        healthBarOrigSize = healthBar.rectTransform.sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, 0);

        // If player presses jump set vel to jump power value     
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpPower;
        }

        }
// Updaet UI elements
    public void UpdateUI()
    {
        // set Coins UI
        coinsText.text = coinsCollected.ToString();

        // Set health UI to 100
        // healthbar.rectTransform.sizeDelta.x /healthBarOrigSize.x;
        healthBar.rectTransform.sizeDelta = new Vector2(100, healthBar.rectTransform.sizeDelta.y);
        // Debug.Log(healthBar.rectTransform.sizeDelta.x /healthBarOrigSize.x);
    }
}
