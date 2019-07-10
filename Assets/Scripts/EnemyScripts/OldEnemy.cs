using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldEnemy : Enemy
{
    Animator anim;
    GameObject target;
    Rigidbody2D rb;
   public bool IsChasing=false;
   public bool wander;
    public float speed;
     float defaultSpeed;
    public float moveDelay;
    public float randomX;
    Vector3 temp;
    Vector3 waypoint;
  
    Vector3 moveDirection;
   public float chaseRange;
    public float attackRange;
    public float buffer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="WeaponHitBox")
        {
            GetComponent<Health>().DealDamage(FindObjectOfType<PlayerMovement>().damage);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
       
        rb = GetComponent<Rigidbody2D>();
        defaultSpeed = speed;
      
        
        
    }
    public void StopMoving()
    {
        speed = 0;
    }

    public void SetDefaultSpeed()
    {
        speed = defaultSpeed;
    }
    // Update is called once per frame
    private void ChangeState(EnemyState newState)
    {
        if (currentState!=newState)
        {
            currentState = newState;
        }
    }
    void FixedUpdate()
    {
        
        

        float distance = Vector3.Distance(transform.position, target.transform.position);
       
        if (distance<chaseRange&&distance>attackRange)
        {
            if ((currentState==EnemyState.idle||currentState==EnemyState.walk)&&currentState!=EnemyState.stagger)
            {
                
                if (currentState==EnemyState.idle)
                {
                    StartCoroutine(WakeUp());
                }
                ChangeState(EnemyState.walk);
                 temp = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
               changeAnim(temp - transform.position);
                Debug.Log(temp );
                rb.MovePosition(temp);
              
            }
            
           
           
        }
        else if (distance>chaseRange)
        {
            StartCoroutine(GoToSleep());
        }
       


       

        




        //  CheckMovementAndPauses();

    }
    private void textCalculation()
    {
        if (target)
        {

        }
    }
   
    private void changeAnim(Vector2 direction)
    {
        direction = direction.normalized;
        anim.SetFloat("MoveX", direction.x);
        anim.SetFloat("MoveY", direction.y);
        /*
        if (Mathf.Abs(direction.x)>Mathf.Abs(direction.y))
        {
            if (direction.x>0)
            {
                SetAnimFloat(Vector2.right);
            }
            else if(direction.x<0)
                {
                SetAnimFloat(Vector2.left);
            }
           
        }
        else if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
        {
            if (direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                SetAnimFloat(Vector2.down);
            }
          
        }
        */
    }

    IEnumerator GoToSleep()
    {
        speed = 0;
        ChangeState(EnemyState.idle);
        anim.SetBool("IsAwake", false);
        yield return new WaitForSeconds(0.3f);
        IsChasing = true;
    }
    IEnumerator  WakeUp()
    {
        speed = 0;
        anim.SetBool("IsAwake", true);
        yield return new WaitForSeconds(0.3f);
        SetDefaultSpeed();
        IsChasing = true;
    }
    
    

   
   
}
