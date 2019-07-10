using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayerInArea : MonoBehaviour
{
    [Header("Attributes")]
    public string enemyName;

    float defaultSpeed;
   
   
    [HideInInspector] public Vector3 temp;
    [HideInInspector] public Vector3 waypoint;
    
    [Header("Bools")]
    public bool inBoundary = false;

    [HideInInspector] public bool contextRestart;
    [HideInInspector] public Animator anim;
    [HideInInspector] public GameObject target;
    [HideInInspector] public Vector3 moveDirection;
    [SerializeField]
    [Header("Attack Mechanics")]
    public Transform homePosition;
    public float chaseRange;
    public float attackRange;
    public float buffer;
    public Collider2D boundary;
    public bool IsChasing = false;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerMovement>().gameObject;
    }
    private void FixedUpdate()
    {
        CheckDistance();
        if (GetComponent<Patrol>() != null && GetComponent<Patrol>().isPatrolling == true)
        {
            GetComponent<Patrol>().PatrolPath();
        }
    }
    public virtual void CheckDistance()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);


        if (distance < chaseRange && distance > attackRange&&boundary.bounds.Contains(target.transform.position))
        {
            inBoundary = true;
            if (GetComponent<Patrol>() != null)
            {
                GetComponent<Patrol>().isPatrolling = false;
            }
            if ((GetComponent<Enemy>().currentState == EnemyState.idle || GetComponent<Enemy>().currentState == EnemyState.walk) && GetComponent<Enemy>().currentState != EnemyState.stagger)
            {

                if (GetComponent<Enemy>().currentState == EnemyState.idle)
                {
                    GetComponent<Enemy>().WakeUp();
                }
                if (contextRestart == false)
                {
                    contextRestart = true;
                    StartCoroutine(GetComponent<Enemy>().ContextCombat());
                }


                GetComponent<Enemy>().ChangeState(EnemyState.walk);
                temp = Vector3.MoveTowards(transform.position, target.transform.position, GetComponent<Enemy>().speed * Time.deltaTime);
                GetComponent<Enemy>().changeAnim(temp - transform.position);

                GetComponent<Enemy>().rb.MovePosition(temp);

            }



        }
        else if (distance > chaseRange|| !boundary.bounds.Contains(target.transform.position))
        {
            inBoundary = false;
            if (Vector3.Distance(transform.position, homePosition.transform.position) <= 1)
            {
                GetComponent<Enemy>().GoToSleep();
            }
            else if (Vector3.Distance(transform.position, homePosition.transform.position) > 1)
            

            
            {
                contextRestart = false;
                GetComponent<Enemy>().ChangeState(EnemyState.walk);
                temp = Vector3.MoveTowards(transform.position, homePosition.transform.position, GetComponent<Enemy>().speed * Time.deltaTime);
                GetComponent<Enemy>().changeAnim(temp - transform.position);

                GetComponent<Enemy>().rb.MovePosition(temp);



            }
            if (GetComponent<Patrol>() != null)
            {
                GetComponent<Patrol>().isPatrolling = true;
            }
           
        }
    }



    // Update is called once per frame
    void Update()
    {

    }
}
