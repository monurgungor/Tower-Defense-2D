using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomTowerPlacement : MonoBehaviour
{
    private GameObject tower;
    public GameObject towerPrefab;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    private bool isPlaceable()
    {

        int cost = towerPrefab.GetComponent<TowerStats>().stages[0].cost;

        if (gameManager.i == gameManager.towerPlaces.Length)
        {
            return false;
        }
        gameManager.randomIndex = Random.Range(0, gameManager.towerPlaces.Length);
        if (gameManager.randomIndex != 0 && gameManager.i<gameManager.towerPlaces.Length)
        {
            while (gameManager.pickedRandomNumbers.Contains(gameManager.randomIndex))
            {
                gameManager.randomIndex = Random.Range(0, gameManager.towerPlaces.Length);
            }
            return !gameManager.pickedRandomNumbers.Contains(gameManager.randomIndex) && gameManager.Money >= cost && !gameManager.gameOver;
        }else if(gameManager.randomIndex == 0 && gameManager.firstPlaceIsEmpty && gameManager.i<gameManager.towerPlaces.Length)
        {
            gameManager.firstPlaceIsEmpty = false;
            return gameManager.Money >= cost && !gameManager.gameOver;

        }
        else
        {
            return false;
        }

    

        
    }
    
    public void PlaceRandomTower()
    {
        if (gameManager.i<=gameManager.towerPlaces.Length)
        {
            if (!isPlaceable())
            {
                return;
            }
        
            if(isPlaceable())
            {
                tower = (GameObject) Instantiate(towerPrefab, gameManager.towerPlaces[gameManager.randomIndex].transform.position, Quaternion.identity) as GameObject;
                gameManager.Money -= tower.GetComponent<TowerStats>().CurrentStage.cost;
                gameManager.pickedRandomNumbers.Add(gameManager.randomIndex);
                gameManager.i++;
            }
            return;
        }
        
    }

}
