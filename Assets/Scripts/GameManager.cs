using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;



public class GameManager : MonoBehaviour
{
    public bool gameOver = false;
    [HideInInspector] public bool firstPlaceIsEmpty=true;
    [HideInInspector] public List<int> pickedRandomNumbers;
    [HideInInspector] public int i, randomIndex;
    
    
    
    
    [SerializeField] GameObject gameOverText;

    public GameObject nextWaveLabel;
    
    public GameObject[] towerPlaces;

    private GameManager gameManager;

        
        
    

    public Text LivesLeftLabel;
    private int lives; 
    public int Lives
    {
        get
        {
            return lives;
        }
        set
        {
            lives = value;
            LivesLeftLabel.text = lives.ToString();
            if (lives <= 0 && !gameOver)
            {
                nextWaveLabel.SetActive(false);
                gameOver = true;
                gameOverText.GetComponent<Animator>().SetBool("gameOver", true);

            }
        }
    }

    private int score;
    public Text scoreLabel;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            scoreLabel.text = score.ToString();
        }
    }
    
    private int money;
    public Text moneyLabel;
    public int Money
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
            moneyLabel.text = money.ToString();
        }
    }
    public Text waveLabel;
    private int wave;
    
    public int Wave
    {
        get
        {
            return wave;
        }
        set
        {
            wave = value;
            if (!gameOver)
                nextWaveLabel.GetComponent<Animator>().SetTrigger("NextWaveIncoming");
        }
    }
    

    
    
    public static GameManager Instance = null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }


    void Start()
    {
        Time.timeScale = 0f;
        gameManager = GameManager.Instance;

        pickedRandomNumbers = new List<int>( new int[towerPlaces.Length] );
        towerPlaces = GameObject.FindGameObjectsWithTag("TowerPlace");

        Money = 1050;
        Wave = 0;
        Lives = 1;
        Score = 0;

    }

}
