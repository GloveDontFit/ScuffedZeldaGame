using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    [Header("Attributes")]
    public string enemyName;
  
    float defaultSpeed;
   public bool hasIdleAnimtation;
    [HideInInspector] public Vector3 temp;
    [HideInInspector] public Vector3 waypoint;
    public float attackDelay;
    [Header("Bools")]
    public bool meleeEnemy;
    bool canAttack;
    bool isAttacking;
    [HideInInspector] public bool contextRestart;
    [HideInInspector] public Animator anim;
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
        target = FindObjectOfType<PlayerMovement>().gameObject;
    }
    private void FixedUpdate()
    {
       
        CheckDistance();
        if (GetComponent<Patrol>() != null && GetComponent<Patrol>().isPatrolling ==true)
        {
            GetComponent<Patrol>().PatrolPath();
        }
    }
    public virtual void CheckDistance()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);

        
        if (distance < chaseRange && distance > attackRange)
        {
            if (meleeEnemy)
            {
                GetComponent<Enemy>().anim.SetBool("IsAttacking", false);
            }
            if (GetComponent<Patrol>() != null)
            {
                GetComponent<Patrol>().isPatrolling = false;
            }
         if ((GetComponent<Enemy>().currentState == EnemyState.idle || GetComponent<Enemy>().currentState == EnemyState.walk) && GetComponent<Enemy>().currentState != EnemyState.stagger)
            {

                if (GetComponent<Enemy>().currentState == EnemyState.idle&&hasIdleAnimtation)
                {
                    GetComponent<Enemy>().WakeUp();
                }
                if (contextRestart == false)
                {
                    contextRestart = true;
                    StartCoroutine(GetComponent<Enemy>().ContextCombat());
                }


                GetComponent<Enemy>(). ChangeState(EnemyState.walk);
                temp = Vector3.MoveTowards(transform.position, target.transform.position, GetComponent<Enemy>().speed * Time.deltaTime);
               GetComponent<Enemy>().changeAnim(temp - transform.position);

               GetComponent<Enemy>().rb.MovePosition(temp);

            }



        }
        else if (distance > chaseRange)
        {
            if (GetComponent<Patrol>() != null)
            {
                GetComponent<Patrol>().isPatrolling = true;
            }
            else
            {
                contextRestart = false;
                if (hasIdleAnimtation)
                {
                    GetComponent<Enemy>().GoToSleep();
                }
                
            }
        }
        else if (distance<=attackRange&&meleeEnemy)
        {


            if (isAttacking==false)
            {
                StartCoroutine(AttackCo());
            }
               
            
            
        }
        
    }
    public IEnumerator AttackCo()
    {
        isAttacking = true;
        GetComponent<Enemy>().currentState = EnemyState.attack;
        GetComponent<Enemy>().anim.SetBool("IsAttacking", true);
     
        yield return new WaitForSeconds(0.3f);
        GetComponent<Enemy>().currentState = EnemyState.walk;
            GetComponent<Enemy>().anim.SetBool("IsAttacking", false);
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;




    }
   

   
    // Update is called once per frame
    void Update()
    {
        
    }
}
