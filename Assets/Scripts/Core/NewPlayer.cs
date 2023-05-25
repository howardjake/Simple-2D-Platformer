using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewPlayer : PhysicsObject
{
    [Header("Attributes")]
    [SerializeField] public int attackPower = 20;
    [SerializeField] private float attackDuration = 0.1f;
    [SerializeField] private float jumpPower = 10;
    [SerializeField] private float maxSpeed = 1;

    [Header("Inventory")]
    [SerializeField] public int coinsCollected = 0;
    [SerializeField] public int health = 100;
    [SerializeField] public int maxHealth = 100;

    [Header("References")]
    [SerializeField] private GameObject attackBox;
    private Vector2 healthBarOrigSize;    
    public Dictionary<string, Sprite> inventory = new Dictionary<string, Sprite>();
    public Sprite inventoryItemBlank; //Default inventory item slot sprite
    public Sprite keySprite; //Key Inventory item  sprite 
    public Sprite keyGemSprite; //KeyGem Inventory item  sprite 

    // Singleton 
    private static NewPlayer instance;
    public static NewPlayer Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<NewPlayer>();
            return instance;
        }
    }

    private void Awake()
    {
        if (GameObject.Find("New Player")) Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        gameObject.name = "New Player";

        GameManager.Instance.coinsText = GameObject.Find("CoinsCounter").GetComponent<Text>();

        // find health bar UI size
        healthBarOrigSize = GameManager.Instance.healthBar.rectTransform.sizeDelta;

        UpdateUI();
        SetSpawnPosition();
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

        // flip player localScale when moveflipped
        if (targetVelocity.x < -.01)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (targetVelocity.x > .01)
        {
            transform.localScale = new Vector2(1, 1);
        }

        // if press "Fire1" then set attack box to active. otherwise  set active to false
        if (Input.GetButtonDown("Fire1"))
        {
            // Start Coroutine Attack
            StartCoroutine(ActivateAttack());
        }

        // Check player health is <= 0
        if (health <= 0)
        {
            Die();
        }
    }

    // Activate Attack
    public IEnumerator ActivateAttack()
    {
        attackBox.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        attackBox.SetActive(false);
    }

    // Update UI elements
    public void UpdateUI()
    {
        // set Coins UI
        GameManager.Instance.coinsText.text = coinsCollected.ToString();

        // Set healthBar width to a percent of its org value
        GameManager.Instance.healthBar.rectTransform.sizeDelta = new Vector2(healthBarOrigSize.x * ((float)health / (float)maxHealth), GameManager.Instance.healthBar.rectTransform.sizeDelta.y);

    }

    public void SetSpawnPosition()
    {
        
        transform.position = GameObject.Find("SpawnLocation").transform.position;
    }

    public void Die()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void AddInventoryItem(string inventoryName, Sprite image)
    {
        inventory.Add(inventoryName, image);

        // Blank inventory sprite will swap to the key sprite
        GameManager.Instance.inventoryItemImage.sprite = inventory[inventoryName];
    }

    public void RemoveInventoryItem(string inventoryName)
    {
        inventory.Remove(inventoryName);

        // Blank inventory sprite will swap to the key sprite
        GameManager.Instance.inventoryItemImage.sprite = inventoryItemBlank;
    }
}
