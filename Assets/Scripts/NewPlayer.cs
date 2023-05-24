using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewPlayer : PhysicsObject
{
    [SerializeField] private float maxSpeed = 1;
    [SerializeField] private float jumpPower = 10;
    [SerializeField] public int coinsCollected = 0;
    [SerializeField] public int maxHealth = 100;
    [SerializeField] public int health = 100;
    public Vector2 healthBarOrigSize;
    
    [SerializeField] private GameObject attackBox;
    [SerializeField] private float attackDuration = 0.1f;
     [SerializeField] public int attackPower = 20;

    // Inventory 
    public Dictionary<string, Sprite> inventory = new Dictionary<string, Sprite>();

    public Image inventoryItemImage;

    public Sprite inventoryItemBlank;
    public Sprite keySprite;
    public Sprite keyGemSprite;

    // Collectables 
    public Text coinsText;
    public Image healthBar;

    // Singleton 
    private static NewPlayer instance;
    public static NewPlayer Instance 
    {
        get {
            if (instance == null) instance = GameObject.FindObjectOfType<NewPlayer>();
            return instance;
        }
    }

    private void Awake() {
        if (GameObject.Find("New Player")) Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        gameObject.name = "New Player";

        coinsText = GameObject.Find("CoinsCounter").GetComponent<Text>();

        // find health bar UI size
        healthBarOrigSize = healthBar.rectTransform.sizeDelta;

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
        if(targetVelocity.x < -.01) {
            transform.localScale = new Vector2(-1, 1);
        } else if (targetVelocity.x > .01) {
            transform.localScale = new Vector2(1, 1);
        }

        // if press "Fire1" then set attack box to active. otherwise  set active to false
        if(Input.GetButtonDown("Fire1")) {
            // Start Coroutine Attack
            StartCoroutine(ActivateAttack());
        }

        // Check player health is <= 0
        if (health <= 0) {
            Die();
        }
    }

    // Activate Attack
    public IEnumerator ActivateAttack() {
        attackBox.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        attackBox.SetActive(false);
    }

    // Update UI elements
    public void UpdateUI()
    {
        // set Coins UI
        coinsText.text = coinsCollected.ToString();

        // Set healthBar width to a percent of its org value
        healthBar.rectTransform.sizeDelta = new Vector2(healthBarOrigSize.x * ((float)health/(float)maxHealth), healthBar.rectTransform.sizeDelta.y);

    }

    public void SetSpawnPosition() {
        transform.position = GameObject.Find("SpawnLocation").transform.position;
    }

    public void Die()
    {
        SceneManager.LoadScene("SampleScene");
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
