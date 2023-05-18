using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private NewPlayer newPlayer;
    [SerializeField] private string requiredInventoryItemString;

    // Start is called before the first frame update
    void Start()
    {
        newPlayer = GameObject.Find("Player").GetComponent<NewPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collison)
    {
        if(collison.gameObject.name == "Player") {
            // if Player has key then destroy 
            if (newPlayer.inventory.ContainsKey(requiredInventoryItemString)) {
                Destroy(gameObject);
            }
        }
    }

}
