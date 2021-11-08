using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HolyWaterSlowdown : MonoBehaviour
{
    private int destroyTime = 2;
    private bool isDestroyed;
    private HolyWater holyWater;

    private void Start()
    {
        holyWater = GameObject.Find("HolyWaterButton").GetComponent<HolyWater>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        PathFollow pathFollow = other.GetComponent<PathFollow>();
        pathFollow.moveSpeed /= 2; 
    }
    

    private void OnTriggerExit2D(Collider2D other)
    {
        PathFollow pathFollow = other.GetComponent<PathFollow>();
        pathFollow.moveSpeed *= 2;
        
        
        Destroy(gameObject, destroyTime);
        
    }
}
