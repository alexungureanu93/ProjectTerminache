using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text coinText;
    [SerializeField] private Image healthBar;
    public Image inventoryDisplayed;
    //singleton instantiation 
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<GameManager>();
            return instance;
        }
    }

    public Image HealthBar { get => healthBar; set => healthBar = value; }
    public Text CoinText { get => coinText; set => coinText = value; }

    private void Awake()
    {
        if (GameObject.Find("New Game Manager")) Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        gameObject.name = "New Game Manager";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
