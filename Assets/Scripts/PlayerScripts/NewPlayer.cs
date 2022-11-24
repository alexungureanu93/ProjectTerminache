using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NewPlayer : PhysicsObject
{

    [Header("Attributes")]
    [SerializeField] private float horizontalSpeed; //movement speed on x axis
    [SerializeField] private float verticalSpeed; //jump speed
    [SerializeField] private int attackPower;
    [SerializeField] private float attackDuration;


    [Header("Inventory")]
    private int inventoryCounter = 0;
    private int maxHealth = 100;
    [SerializeField] private int coinsCollected;
    [SerializeField] private int health;


    [Header("References")]
    [SerializeField] private GameObject attackBox;
    private Vector2 healthBarOriginalSize;
    public List<Sprite> inventoryItems = new List<Sprite>();


    //singleton instantiation 
    private static NewPlayer instance;
    public static NewPlayer Instance 
    {
        get 
        {
            if (instance == null) instance = GameObject.FindObjectOfType<NewPlayer>();
            return instance;
        }
    }

    public int CoinsCollected { get => coinsCollected; set => coinsCollected = value; }
    public int Health { get => health; set => health = value; }
    public GameObject AttackBox { get => attackBox; set => attackBox = value; }
    public int AttackPower { get => attackPower; set => attackPower = value; }

    // Start is called before the first frame update

    private void Awake()
    {
        if (GameObject.Find("New Player")) Destroy(gameObject);
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        gameObject.name = "New Player";

        healthBarOriginalSize = GameManager.Instance.HealthBar.rectTransform.sizeDelta;
        UpdateUI();
        SetSpawnPosition();
    }

    public void SetSpawnPosition()
    {
        transform.position = GameObject.Find("Spawn Location").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = new Vector2(Input.GetAxis("Horizontal") * horizontalSpeed, 0);
        if(targetVelocity.x<-.01)
        {
            transform.localScale =new Vector3 (-1, transform.localScale.y);
        }
        else if(targetVelocity.x>.01) 
        {
            transform.localScale = new Vector3(1 ,transform.localScale.y);
        }
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = verticalSpeed;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            SwitchInventory();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(ActivateAttack());
        }
        Die();
    }

    private void Die()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Level1");
        }
    }
    private IEnumerator ActivateAttack()
    {
        attackBox.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        attackBox.SetActive(false);
    }

    public void UpdateUI()
    {
        GameManager.Instance.CoinText.text = CoinsCollected.ToString();

        float healthBarSize = healthBarOriginalSize.x * ((float)Health / (float)maxHealth);
        GameManager.Instance.HealthBar.rectTransform.sizeDelta = new Vector2(healthBarSize, GameManager.Instance.HealthBar.rectTransform.sizeDelta.y);
    }

    public void AddInventoryItem(Sprite imageName) 
    {
        inventoryItems.Add(imageName);
    }
    public void RemoveInventoryItem(Sprite imageName)
    {
        inventoryItems.Remove(imageName);
        if (GameManager.Instance.inventoryDisplayed.sprite == imageName)
        {
            GameManager.Instance.inventoryDisplayed.sprite = inventoryItems[0];
        }

    }

    private void SwitchInventory()
    {
 
        if (inventoryCounter < inventoryItems.Count)
        {
           GameManager.Instance.inventoryDisplayed.sprite = inventoryItems[inventoryCounter];
           inventoryCounter++;
        }
        else
        {
            inventoryCounter = 0;
        }
    }
}


