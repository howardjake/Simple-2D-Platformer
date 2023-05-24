using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private string requiredInventoryItemString;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collison)
    {
        if(collison.gameObject == NewPlayer.Instance.gameObject) {
            // if Player has key then destroy 
            if (NewPlayer.Instance.inventory.ContainsKey(requiredInventoryItemString)) {
                Destroy(gameObject);
            }
        }
    }

}
