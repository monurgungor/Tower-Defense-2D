using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTower : MonoBehaviour
{
    private GameManager gameManager;

    private TowerStats nextStage;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();


    }
    void OnMouseUp()
    {
        if (CanUpgrade())
        {
            gameObject.transform.parent.GetComponent<TowerStats>().IncreaseStage();
            gameManager.Money -= gameObject.transform.parent.gameObject.GetComponent<TowerStats>().CurrentStage.cost;
            gameObject.GetComponent<Animator>().SetTrigger("Upgraded");
        }
    }
    
    private bool CanUpgrade()
    {
        TowerStage nextStage = gameObject.transform.parent.GetComponent<TowerStats>().GetNextStage();
        if (nextStage != null)
        {
            return gameManager.Money >= nextStage.cost;
        }

        return false;
    }
}
