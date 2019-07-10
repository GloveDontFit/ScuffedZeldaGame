using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{

    [Header("Attributes")]
    public string enemyName;
   
    float defaultSpeed;

    [HideInInspector] public Vector3 temp;
    [HideInInspector] public Vector3 waypoint;
   
    [Header("Bools")]


    [HideInInspector] public bool contextRestart;
   
    [HideInInspector] public GameObject target;
    [HideInInspector] public Vector3 moveDirection;
    [SerializeField]
    [Header("Attack Mechanics")]
    public float chaseRange;
    public float attackRange;
    public float buffer;

    public bool IsChasing = false;
    
   
   
   
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        currentState = EnemyState.idle;


        anim.SetBool("IsAwake", true);

        rb = GetComponent<Rigidbody2D>();
        defaultSpeed = speed;



    }
   
    // Update is called once per frame





    //  CheckMovementAndPauses();





    void FixedUpdate()
    {
        CheckDistance();
        

    }

 
    public   virtual void CheckDistance()
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance < chaseRange && distance > attackRange)
            {
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

                contextRestart = false;
                StartCoroutine(GoToSleep());
            }
        }
      
        // Start is called before the first frame update
      public  IEnumerator GoToSleep()
        {
            speed = 0;
            ChangeState(EnemyState.idle);
            anim.SetBool("IsAwake", false);
            yield return new WaitForSeconds(0.3f);
            IsChasing = true;
        }
       public IEnumerator WakeUp()
        {
            speed = 0;
            anim.SetBool("IsAwake", true);
            yield return new WaitForSeconds(0.3f);
            SetDefaultSpeed();
            IsChasing = true;
        }







    }



    



