using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class PathFollow : MonoBehaviour
{

    public Transform[] Waypoints;
    [SerializeField] public float moveSpeed = 2f;



    private int waypointIndex = 0;
    private Vector2 firstPosition;
    private Vector2 positionToGo;

    public bool gameEnded;


    private void Update ()
    {
        Move();
        
    }
    

    private void Move()
    {
        if (waypointIndex < Waypoints.Length)
        {

            transform.position = Vector2.MoveTowards(transform.position,
                Waypoints[waypointIndex].transform.position,
                moveSpeed * Time.deltaTime);

            if (transform.position == Waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;

                if (waypointIndex < Waypoints.Length)
                {
                    Rotate();
                    moveSpeed = 1.7f;
                }
                else if(waypointIndex >= Waypoints.Length && gameEnded==false)
                {
                    Destroy(gameObject);
                    GameManager gameManager =
                        GameObject.Find("GameManager").GetComponent<GameManager>();
                    gameManager.Lives -= 1;
                    if (gameManager.Lives == 0)
                    {
                        gameEnded=true;
                    }
                }

            }
            

        }

    }

    private void Rotate()
    {
        Animator animator = gameObject.GetComponentInChildren<Animator>();
        
        
        
        firstPosition = Waypoints[waypointIndex-1].transform.position;
        positionToGo = Waypoints[waypointIndex].transform.position;



        if (firstPosition.x == positionToGo.x)
        {
            animator.SetFloat("xDirection", 0);
                 
                animator.SetFloat("yDirection", positionToGo.y - firstPosition.y);

                
        }else
        {
            animator.SetFloat("yDirection", 0);

            animator.SetFloat("xDirection", positionToGo.x - firstPosition.x);



        }
    }
    
    public float DistanceToGoal()
    {
        float distance = 0;
        if (waypointIndex < Waypoints.Length-1)
        {
            distance += Vector2.Distance(
                gameObject.transform.position, 
                Waypoints[waypointIndex + 1].transform.position);
        }
            
        for (int i = waypointIndex + 1; i < Waypoints.Length - 1; i++)
        {
            Vector2 startPosition = Waypoints[i].transform.position;
            Vector2 endPosition = Waypoints [i + 1].transform.position;
            distance += Vector2.Distance(startPosition, endPosition);
        }

        return distance;
    }
}