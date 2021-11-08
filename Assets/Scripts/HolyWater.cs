using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HolyWater : MonoBehaviour
{
    private float coolDown = 10;
    public float coolDownTimer=0;

    public Text HolyWaterLabel;
    public GameObject HolyWaterPrefab;
    private GameObject holyWater;
    private GameManager gameManager;
    private bool isAvailable = true;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    private void Update()
    {
        if (coolDownTimer <= 0)
        {
            isAvailable = true;
        }else if (coolDownTimer > 0)
        {
            isAvailable = false;
            coolDownTimer -= Time.deltaTime;
            HolyWaterLabel.text = Math.Round(coolDownTimer).ToString();
        }

    }

    public bool lastHolyWaterDeleted()
    {
        if (GameObject.FindGameObjectWithTag("HolyWater") == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    void OnMouseDown()
    {
        if (isAvailable && lastHolyWaterDeleted())
        {
            holyWater = (GameObject) Instantiate(HolyWaterPrefab, transform.position, Quaternion.identity);
            coolDownTimer = coolDown;


        }
        
    }
}
