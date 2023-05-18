using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewPlayer : PhysicsObject
{
    [SerializeField] private float maxSpeed = 1;
    [SerializeField] private float jumpPower = 10;
    [SerializeField] public int coinsCollected = 0;
    [SerializeField] public int maxHealth = 100;
    [SerializeField] public int health = 100;
    public Vector2 healthBarOrigSize;

    // Inventory 
    public Dictionary<string, Sprite> inventory = new Dictionary<string, Sprite>();

    public Image inventoryItemImage;

    public Sprite inventoryItemBlank;
    public Sprite keySprite;
    public Sprite keyGemSprite;

    // Collectables 
    public Text coinsText;
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        coinsText = GameObject.Find("CoinsCounter").GetComponent<Text>();

        // find health bar UI size
        healthBarOrigSize = healthBar.rectTransform.sizeDelta;

        UpdateUI();
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

        // Set healthBar width to a percent of its org value
        healthBar.rectTransform.sizeDelta = new Vector2(healthBarOrigSize.x * ((float)health/(float)maxHealth), healthBar.rectTransform.sizeDelta.y);

    }

    public void AddInventoryItem(string inventoryName, Sprite image) {
        inventory.Add(inventoryName, image);

        // Blank inventory sprite will swap to the key sprite
        inventoryItemImage.sprite = inventory[inventoryName];
    }

    public void RemoveInventoryItem(string inventoryName) {
        inventory.Remove(inventoryName);

        // Blank inventory sprite will swap to the key sprite
        inventoryItemImage.sprite = inventoryItemBlank;
    }
}
