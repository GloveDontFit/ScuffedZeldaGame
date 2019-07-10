using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public Signal pauseGame;
    public GameObject pauseCanvas;
    public Item Bow;
    bool canShoot;
    public FloatValue currentMagicCost;
    public FloatValue currentMagicAmount;
    public Signal useMana;
    public float moveX;
    public float moveY;
    public GameObject arrow;
    public Inventory playerInventory;
    public SpriteRenderer recievedItemSprite;
    public PlayerState currentState;
    Animator anim;
    public float speed;
    float horizontalInput;
    float verticalInput;
    Rigidbody2D rb;
    Vector2 maxVelocity;
   public Vector3 change;
    public float attackDelayTime;
    bool isAttacking;
    public float attackRange;
    public LayerMask damagable;
    public float damage;
    public float playerHealth;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;
   public VectorValue StartingPosition;
    // Start is called before the first frame update
    private void Awake()
    {
      
    }
    void Start()
    {
       transform.position= StartingPosition.initialValue;
        currentState = PlayerState.walk;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        maxVelocity = new Vector2(speed, speed);
        anim.SetFloat("moveX", StartingPosition.moveX);
        anim.SetFloat("moveY", StartingPosition.moveY);

    }

    // Update is called once per frame
    public enum PlayerState
        {
        walk,
        attack,
        interact,
        stagger,
        idle,
       
        }
    public void checkMana()
    {
       
        if (currentMagicAmount.RuntimeValue>0)
        {
            StartCoroutine(ShootProjectile());
           
           
           
           
        }
        
    }
  public void  CheckForPause()
    {
        if (Input.GetButtonDown("PauseButton"))
        {
           
            pauseCanvas.SetActive(true);
            pauseGame.Raise();
        }
    }
    private void Update()
    {
        CheckForPause();
    }
    private void FixedUpdate()
    {
        moveX=(anim.GetFloat("moveX"));
       moveY= anim.GetFloat("moveY");
        if (currentState==PlayerState.interact)
        {
            return;
        }
        playerHealth = currentHealth.initialValue;
    
   
       
        change = Vector2.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

       
       
        if (Input.GetButtonDown("Attack")&&currentState!=PlayerState.attack&&currentState!=PlayerState.stagger)
        {
           
            StartCoroutine(Attack());
        }
        else if (Input.GetButtonDown("FireProjectile") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {

            if (playerInventory.CheckForItem(Bow))
            {
                checkMana();
            }
           
            
            
                
            
            
        }
        else if (currentState == PlayerState.walk||currentState==PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
 
       
       
       
      
    }
    public void RaisedItem()
    {

        if (playerInventory.currentItem != null)
        {


            if (currentState != PlayerState.interact)
            {
                Debug.Log("RaiseIt");
                currentState = PlayerState.interact;
                anim.SetBool("RecieveItem", true);
                recievedItemSprite.sprite = playerInventory.currentItem.itemSprite;

                recievedItemSprite.gameObject.SetActive(true);
                if (playerInventory.currentItem.itemName == "HeartContainer")
                {
                    Debug.Log("remove");
                    playerInventory.items.Remove(playerInventory.currentItem);
                }
            }
            else
            {

                currentState = PlayerState.walk;
                playerInventory.currentItem = null;
                anim.SetBool("RecieveItem", false);
                recievedItemSprite.gameObject.SetActive(false);
            }
        }
        
    }
    public void knock(Rigidbody2D target,float knocktime,float damage)
    {
       
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.RuntimeValue>0)
        {
           
            StartCoroutine(Knockback(target, knocktime));
        }
        else
        {
            this.gameObject.SetActive(false);
        }
       
       
    }
    IEnumerator Knockback(Rigidbody2D myRigbody, float knockbackTime)
    {


        if (myRigbody != null)
        {
            yield return new WaitForSeconds(knockbackTime);
            myRigbody.velocity = Vector2.zero;


            currentState = PlayerState.idle;
            myRigbody.velocity = Vector2.zero;
        }



    }
  
    public void CreateArrow()
    {
        Vector3 tempDirection = new Vector3(anim.GetFloat("moveX"), anim.GetFloat("moveY"));
        GameObject projectile =  Instantiate(arrow,transform.position,Quaternion.identity) as GameObject;
        currentMagicCost.RuntimeValue = projectile.GetComponent<PlayerProjectile>().manaCost;
       
       
        projectile.transform.parent = transform;
        projectile.GetComponent<PlayerProjectile>().Setup(tempDirection,ChooseArrowDirection());
        useMana.Raise();

    }
    Vector3 ChooseArrowDirection()
    {
       
        Debug.Log("MoveX=" + anim.GetFloat("moveX"));
        Debug.Log("MoveY=" + anim.GetFloat("moveY"));
        float temp = Mathf.Atan2(anim.GetFloat("moveX"), anim.GetFloat("moveY")*-1 ) *Mathf.Rad2Deg;
       

        return new Vector3(0,0,temp);
    }
    IEnumerator ShootProjectile()
    {

        Debug.Log("Shoot");
            currentState = PlayerState.attack;
            yield return null;
            CreateArrow();
            yield return new WaitForSeconds(attackDelayTime);
            if (currentState != PlayerState.interact)
            {
                currentState = PlayerState.walk;
            }
        
       
       

    }
    IEnumerator Attack()
    {
        anim.SetBool("IsAttacking", true);
        currentState = PlayerState.attack;
      yield  return null;
        anim.SetBool("IsAttacking", false);
        yield return new WaitForSeconds(attackDelayTime);
        if (currentState!=PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
     
    }
    public void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            change.x = Mathf.RoundToInt(change.x);
            change.y = Mathf.RoundToInt(change.y);
            anim.SetBool("moving", true);
            anim.SetFloat("moveX", change.x);
            anim.SetFloat("moveY", change.y);
        }
        else
        {
            anim.SetBool("moving", false);
        }
        
    }
    public void MoveCharacter()
    {
        change.Normalize();
        rb.MovePosition( transform.position + change * speed * Time.deltaTime);
    }
}
