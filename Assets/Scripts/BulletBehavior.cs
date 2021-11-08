using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    
    public float speed = 10;
    public int damage;
    public GameObject target;
    public Vector3 startPosition;
    public Vector3 targetPosition;

    private float distance;
    private float startTime;

    private GameManager gameManager;

    void Start()
    {
        startTime = Time.time;
        distance = Vector2.Distance(startPosition, targetPosition);
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();

    }

    void Update()
    {
        float timeInterval = Time.time - startTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);

        if (gameObject.transform.position.Equals(targetPosition))
        {
            if (target != null)
            {
                Transform healthBarTransform = target.transform.Find("HealthBar");
                HealthBar healthBar =
                    healthBarTransform.gameObject.GetComponent<HealthBar>();
                healthBar.currentHealth -= Mathf.Max(damage, 0);
                if (healthBar.currentHealth <= 0)
                {
                    Destroy(target);
                    Destroy(gameObject);
                    gameManager.Score += 1;
                    gameManager.Money += 50;
                }else if (healthBar.currentHealth >= 0 && healthBar.currentHealth <= healthBar.maxHealth/4)
                {
                    var renderer = healthBar.GetComponent<Renderer>();
                    renderer.material.SetColor("_Color", Color.red);
                }else if (healthBar.currentHealth >= healthBar.maxHealth/4 && healthBar.currentHealth <= healthBar.maxHealth/2)
                {
                    var renderer = healthBar.GetComponent<Renderer>();
                    renderer.material.SetColor("_Color", Color.yellow);
                }
            }
            Destroy(gameObject);
        }
    }
    

}
