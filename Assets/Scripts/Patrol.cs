using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public bool isPatrolling;
    public int currentWaypoint;
    [SerializeField] public Transform[] waypoints;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void PatrolPath()
    {

        bool destinationReached = false;
        if (isPatrolling == true)
        {
            destinationReached = false;
            if (GetComponent<Enemy>().currentState != EnemyState.stagger)
            {

                GetComponent<Enemy>().ChangeState(EnemyState.walk);
                float distance = Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position);
                Vector3 temp = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position, GetComponent<Enemy>().speed * Time.deltaTime);
                GetComponent<Enemy>().rb.MovePosition(temp);
                GetComponent<Enemy>().changeAnim(temp - transform.position);

                if (distance <= 0.5)
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
    // Update is called once per frame
    void Update()
    {
        PatrolPath();
    }
}
