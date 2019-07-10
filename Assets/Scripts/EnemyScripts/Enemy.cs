using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    walk,
    attack,
    stagger,
    idle,
}
public class Enemy : MonoBehaviour
{
    public Powerup droppedLoot;
    public LootTable lootTable;
    public Signal deathSignal;
    public GameObject target;
    public GameObject deathEffect;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    public float speed;
    float defaultSpeed;
    public Signal contextSwitcher;
    public Signal context;
    public Vector2 homePosition;
    public EnemyState currentState;
    public float health;
   
    public int moveSpeed;
    public int baseAttack;


    public float moveDelay;
 
    public FloatValue maxHealht;
    
    // Start is called before the first frame update
    private void Awake()
    {
        homePosition = transform.position;
        target = FindObjectOfType<PlayerMovement>().gameObject;
        health = maxHealht.initialValue;
    }
    
    public void DropLoot()
    {
        if (lootTable!=null)
        {
            droppedLoot = lootTable.LootPowerUp();
            if (droppedLoot != null)
            {
                Instantiate(droppedLoot, transform.position, Quaternion.identity);
            }
        }
        
      
    }
    private void OnDisable()
    {
        DropLoot();
        TellParent();
    }
    private void OnEnable()
    {
        currentState = EnemyState.idle;
        transform.position = homePosition;
        health = maxHealht.initialValue;
    }
    private void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
          
            deathSignal.Raise();
            PlayDeathAnimation();
            this.gameObject.SetActive(false);
        }
    }
    public void TellParent()
    {
        DungeonEnemyRoom parentRoom = GetComponentInParent<DungeonEnemyRoom>();
        if (parentRoom!=null)
        {
            parentRoom.CheckEnemies();
        }
       
    }
    public void knock(Rigidbody2D myRigbody, float knockTime, float damage)
    {

        StartCoroutine(Knockback(myRigbody, knockTime));
        takeDamage(damage);
    }
    IEnumerator Knockback(Rigidbody2D myRigbody, float knockbackTime)
    {


        if (myRigbody != null)
        {
            yield return new WaitForSeconds(knockbackTime);
            myRigbody.velocity = Vector2.zero;


            GetComponent<Enemy>().currentState = EnemyState.idle;
            myRigbody.velocity = Vector2.zero;

        }



    }
    private void Start()
    {
        anim = GetComponent<Animator>();
       
        currentState = EnemyState.idle;


        anim.SetBool("IsAwake", true);

        rb = GetComponent<Rigidbody2D>();
        defaultSpeed = speed;
    }
    public IEnumerator ContextCombat()
    {

        contextSwitcher.Raise();
        context.Raise();
        yield return new WaitForSeconds(1);


        context.Raise();
        contextSwitcher.Raise();

    }
    // Start is called before the first frame update
    public void GoToSleep()
    {
        StartCoroutine(Sleep());
    }

    public void WakeUp()
    {
        StartCoroutine(Wake());
    }
    public IEnumerator Sleep()
    {
        
        //speed = 0;
        ChangeState(EnemyState.idle);
        anim.SetBool("IsAwake", false);
        yield return new WaitForSeconds(0.3f);
      
    }
    public IEnumerator Wake()
    {
       // speed = 0;
        anim.SetBool("IsAwake", true);
        yield return new WaitForSeconds(0.3f);
        SetDefaultSpeed();
       
    }
    public void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }


    public void changeAnim(Vector2 direction)
    {
        direction = direction.normalized;
        anim.SetFloat("MoveX", direction.x);
        anim.SetFloat("MoveY", direction.y);

    }

   
    public void StopMoving()
    {
        speed = 0;
    }
    public void PlayDeathAnimation()
    {
       GameObject effect = Instantiate(deathEffect,gameObject.transform.position,Quaternion.identity);
        Destroy(effect, 0.5f);
    }

    public void SetDefaultSpeed()
    {
        speed = defaultSpeed;
    }
}

