using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLog : Log
{
    public bool patrol;
    public int currentWaypoint;
    [SerializeField] public Transform[] waypoints;

    // Start is called before the first frame update
    void FixedUpdate()
    {


        Patrol();
        CheckDistance();




    }
    public override void CheckDistance()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance < chaseRange && distance > attackRange)
        {
            patrol = false;
            if ((currentState == EnemyState.idle || currentState == EnemyState.walk) && currentState != EnemyState.stagger)
            {

                if (currentState == EnemyState.idle)
                {
                    StartCoroutine(WakeUp());
                }
                if (contextRestart == false)
                {
                    contextRestart = true;
                    StartCoroutine(ContextCombat());
                }


                ChangeState(EnemyState.walk);
                temp = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
                changeAnim(temp - transform.position);

                rb.MovePosition(temp);
            }
            
        }
        else if (distance > chaseRange)
        {
            patrol = true;
           
            contextRestart = false;
           
        }

    }
    public void Patrol()
    {
      
        bool destinationReached=false;
        if (patrol==true)
        

        
        

        
        {
            destinationReached = false;
            ChangeState(EnemyState.walk);
            float distance = Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position);
            temp = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position, speed * Time.deltaTime);
            rb.MovePosition(temp);
            changeAnim(temp - transform.position);
            
            if (distance<=0.5)
            {
                destinationReached = true;
                currentWaypoint++;
                
            }
            if (currentWaypoint == waypoints.Length && destinationReached == true)
            {
                currentWaypoint = 0;
            }
        }
       

        }
       
        

       
        
    }




    
