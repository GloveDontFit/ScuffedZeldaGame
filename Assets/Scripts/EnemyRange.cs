using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    public Collider2D[] ranges;
    Rigidbody2D missleRigbody;
    Vector3 temp;
    Vector3 direction;
    GameObject missle;
  public  float missleSpeed;
   public float shootDelay;
    bool canShoot=true;
    public GameObject throwable;
    public float attackRange;
   public bool isShooting;
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {

      
        target = FindObjectOfType<PlayerMovement>().gameObject;
    }
    private void OnEnable()
    {
        
        canShoot = false;
        StartCoroutine(WaitToShoot());
    }
    IEnumerator WaitToShoot()
    {
        yield return new WaitForSeconds(2f);
        canShoot = true;
    }
    // Update is called once per frame
    void Update()
    {
        
        

        if (GetComponent<Enemy>().currentState!=EnemyState.stagger)
        {

            GetComponent<Patrol>().isPatrolling = true;
        }
        else
        {
            GetComponent<Patrol>().isPatrolling = false;
        }
     
         direction = new Vector3( GetComponent<Enemy>().anim.GetFloat("MoveX"), GetComponent<Enemy>().anim.GetFloat("MoveY"));
       
        
        

      
        
           
           
           
            StartCoroutine(ShootPlayer());
           
        
       

    }
    public Vector3 GiveDirection()
    {
        return direction;
    }
    IEnumerator ShootPlayer()
    {

        if (canShoot==true)
        {
           
            canShoot = false;
            
            GameObject missle = Instantiate(throwable, transform.position, Quaternion.identity);
            missle.transform.parent = gameObject.transform;
            if (direction == Vector3.zero)
            {
                missle.SetActive(false);
            }


            yield return new WaitForSeconds(shootDelay);
            canShoot = true;
        }
       
    }
  
}
