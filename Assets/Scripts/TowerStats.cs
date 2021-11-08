using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TowerStage
{
    public int cost;
    public GameObject visuals;
    public GameObject bullet;
    public float fireRate = 1;

}
public class TowerStats : MonoBehaviour
{

    public List<TowerStage> stages;
    private TowerStage currentStage;

    public TowerStage CurrentStage
    {
        get
        {
            return currentStage;
        }
        set
        {
            currentStage = value;
            int currentStageIndex = stages.IndexOf(currentStage);

            GameObject stageVisualization = stages[currentStageIndex].visuals;
            for (int i = 0; i < stages.Count; i++)
            {
                if (stageVisualization != null)
                {
                    if (i == currentStageIndex)
                    {
                        stages[i].visuals.SetActive(true);
                    }
                    else
                    {
                        stages[i].visuals.SetActive(false);
                    }
                }
            }
        }
    }

    void OnEnable()
    {
        CurrentStage = stages[0];
    }

    public TowerStage GetNextStage()
    {
        int currentStageIndex = stages.IndexOf(currentStage);
        int maxStageIndex = stages.Count - 1;
        if (currentStageIndex < maxStageIndex)
        {
            return stages[currentStageIndex + 1];
        }
        else
        {
            return null;
        }
    }

    public void IncreaseStage()
    {
        int currentStageIndex = stages.IndexOf(currentStage);
        if (currentStageIndex < stages.Count - 1)
        {
            CurrentStage = stages[currentStageIndex + 1];
        }
    }
    

}
