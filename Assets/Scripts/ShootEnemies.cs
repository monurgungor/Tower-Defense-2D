using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class ShootEnemies : MonoBehaviour
{
    public List<GameObject> enemiesInRange;
    private float lastShotTime;
    private TowerStats towerStats;
    private Vector3 bulletPosition;
    public bool isMage = false;

    void Start()
    {

        bulletPosition = new Vector3(transform.position.x,transform.position.y+0.4f,0);
        
        enemiesInRange = new List<GameObject>();
        lastShotTime = Time.time;
        towerStats = gameObject.GetComponentInChildren<TowerStats>();
    }

    void Update()
    {
        GameObject target = null;
        float minimalEnemyDistance = float.MaxValue;
        foreach (GameObject enemy in enemiesInRange)
        {
            float distanceToGoal = enemy.GetComponent<PathFollow>().DistanceToGoal();
            if (distanceToGoal < minimalEnemyDistance)
            {
                target = enemy;
                minimalEnemyDistance = distanceToGoal;
            }
        }
        if (target != null)
        {
            if (Time.time - lastShotTime > towerStats.CurrentStage.fireRate)
            {

                Shoot(target.GetComponent<Collider2D>());
                lastShotTime = Time.time;
                

            }

        }
        else
        {        
            Animator animator = 
                towerStats.CurrentStage.visuals.GetComponent<Animator>();
            animator.SetTrigger("Idle");
        }
    }
    void OnEnemyDestroy(GameObject enemy)
    {
        enemiesInRange.Remove(enemy);
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
            EnemyDestructionDelegate del =
                other.gameObject.GetComponent<EnemyDestructionDelegate>();
            del.enemyDelegate += OnEnemyDestroy;
        }
    }
    void OnTriggerExit2D (Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
            EnemyDestructionDelegate del =
                other.gameObject.GetComponent<EnemyDestructionDelegate>();
            del.enemyDelegate -= OnEnemyDestroy;
        }
    }

    void Shoot(Collider2D target)
    {

        GameObject bulletPrefab = towerStats.CurrentStage.bullet;
        Vector3 startPosition = gameObject.transform.position;
        Vector3 targetPosition = target.transform.position;
        startPosition.z = bulletPrefab.transform.position.z;
        targetPosition.z = bulletPrefab.transform.position.z;

        GameObject newBullet = (GameObject)Instantiate (bulletPrefab, bulletPosition, Quaternion.identity);
        
        BulletBehavior bulletBehavior = newBullet.GetComponent<BulletBehavior>();
        newBullet.transform.position = startPosition;
        BulletBehavior bulletComp = newBullet.GetComponent<BulletBehavior>();
        bulletComp.target = target.gameObject;


        Vector2 direction = target.transform.position - newBullet.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        newBullet.transform.rotation = Quaternion.Slerp(newBullet.transform.rotation, rotation, 10 * Time.deltaTime);

        bulletComp.startPosition = startPosition;
        bulletComp.targetPosition = targetPosition;


        SpriteRenderer spriteRenderer = 
            towerStats.CurrentStage.visuals.GetComponent<SpriteRenderer>();
        Animator animator = 
            towerStats.CurrentStage.visuals.GetComponent<Animator>();

        if (gameObject.transform.position.x < target.transform.position.x)
        {
            spriteRenderer.flipX = true;
            animator.SetTrigger("Shot");
        }
        else
        {
            spriteRenderer.flipX = false;
            animator.SetTrigger("Shot");
        }

    }
}
