using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    public Text coinsText;
    public Image healthBar;
    public Image inventoryItemImage;

    // Singleton 
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<GameManager>();
            return instance;
        }
    }

    private void Awake()
    {
        if (GameObject.Find("New Game Manager")) Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        // retain file on new scene load
        DontDestroyOnLoad(gameObject);
        gameObject.name = "New Game Manager";

    }

    // Update is called once per frame
    void Update()
    {

    }
}
