using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // variables
    enum ItemType { Coin, Health, Ammo, InventoryItem };
    [SerializeField] private ItemType itemType;
    [SerializeField] private string inventoryStrName;
    [SerializeField] private Sprite inventorySprite;

    // Start is called before the first frame update
    void Start()
    {

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
            if (collison.gameObject == NewPlayer.Instance.gameObject)
            {
                // +1 Coin
                NewPlayer.Instance.coinsCollected += 1;

            }
        }
        // When Health
        else if (itemType == ItemType.Health)
        {
            if (NewPlayer.Instance.health < 100)
            {
                NewPlayer.Instance.health += 1;
            }
        }
        else if (itemType == ItemType.Ammo)
        {
            Debug.Log("im ammo");
        }
        else if (itemType == ItemType.InventoryItem)
        {
            NewPlayer.Instance.AddInventoryItem(inventoryStrName, NewPlayer.Instance.keySprite);
        }
        else
        {
            Debug.Log("im a default inventory item");
        }
        // Update UI
        NewPlayer.Instance.UpdateUI();

        // Remove Coin
        Destroy(gameObject);
    }
}
