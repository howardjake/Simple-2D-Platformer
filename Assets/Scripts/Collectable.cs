using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // variables
    enum ItemType { Coin, Health, Ammo, InventoryItem };
    [SerializeField] private ItemType itemType;
    [SerializeField] NewPlayer newPlayer;

    [SerializeField] private string inventoryStrName;
    [SerializeField] private Sprite inventorySprite;

    // Start is called before the first frame update
    void Start()
    {
        // Find Player Script
        newPlayer = GameObject.Find("Player").GetComponent<NewPlayer>();
    }

    // Update is called once per frame
    void Update()
    {

    }



    // on collision 
    private void OnTriggerEnter2D(Collider2D collison)
    {
        // When Coin
        if (itemType == ItemType.Coin)
        {
            // When Player Touches
            if (collison.gameObject.name == "Player")
            {
                // +1 Coin
                newPlayer.coinsCollected += 1;

            }
        }
        // When Health
        else if (itemType == ItemType.Health)
        {
            if (newPlayer.health < 100) {
                newPlayer.health += 1;
            }
        }
        else if (itemType == ItemType.Ammo)
        {
            Debug.Log("im ammo");
        }
        else if (itemType == ItemType.InventoryItem)
        {
            newPlayer.AddInventoryItem(inventoryStrName, newPlayer.keySprite);
        }
        else
        {
            Debug.Log("im a default inventory item");
        }
        // Update UI
        newPlayer.UpdateUI();

        // Remove Coin
        Destroy(gameObject);
    }
}
