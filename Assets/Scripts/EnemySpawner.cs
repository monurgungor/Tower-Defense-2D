using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class WaveInfo
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2;
    public int maxEnemies = 15;
}
public class EnemySpawner : MonoBehaviour
{
    public WaveInfo[] waves;

    public Transform[] waypoints;
    
    private Vector3 firstPoint;
    public GameObject nextWaveLabel;
    public GameObject gameWonText;
    


    public int timeBetweenWaves = 5;

    private GameManager gameManager;

    private float lastSpawnTime;
    private int enemiesSpawned = 0;
    private int currentWave;
    private EnemySpawner enemySpawner;



    void Start()
    {
        lastSpawnTime = Time.time;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        firstPoint = waypoints[0].transform.position;
         currentWave = gameManager.Wave;

    }
    private void Update()
    {
        int currentWave = gameManager.Wave;
        if (currentWave < waves.Length)
        {
            float timeInterval = Time.time - lastSpawnTime;
            float spawnInterval = waves[currentWave].spawnInterval;
            if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) ||
                 timeInterval > spawnInterval) && 
                enemiesSpawned < waves[currentWave].maxEnemies)
            {
                lastSpawnTime = Time.time;
                GameObject newEnemy = (GameObject) Instantiate(waves[currentWave].enemyPrefab, waypoints[0].transform.position, Quaternion.identity);
                
                newEnemy.GetComponent<PathFollow>().Waypoints = waypoints;
                enemiesSpawned++;
            }
            if (enemiesSpawned == waves[currentWave].maxEnemies &&
                GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                gameManager.Wave++;
                gameManager.Money = Mathf.RoundToInt(gameManager.Money * 1.1f);
                enemiesSpawned = 0;
                lastSpawnTime = Time.time;
            }
        }
        else
        {
            nextWaveLabel.SetActive(false);
            gameManager.gameOver = true;
            
            
            gameWonText.GetComponent<Animator>().SetBool("gameOver", true);
        
        }
    }
}
