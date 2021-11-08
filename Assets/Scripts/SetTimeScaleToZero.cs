using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTimeScaleToZero : MonoBehaviour
{
    [SerializeField] public float timeScaleToSet;
    private GameObject raycastBlocker;
    public bool startGame;
    public GameObject starterToDeactivate;
    public void deactivateRaycastBlock()
    {
        raycastBlocker = GameObject.Find("RaycastBlocker");

        if (startGame)
        {
            raycastBlocker.SetActive(false);
        }
        
        starterToDeactivate.SetActive(false);

    }

    public void setTimeScale()
    {
        Time.timeScale = timeScaleToSet;
    }
    

}
