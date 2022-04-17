using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float FireRate;
    SpriteRenderer mySprite;
    BoxCollider2D myCollider;
    Animator myAnim;
    float starttime;
    [SerializeField] float life;
    bool isOnRange = true;
    
    bool direction;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        starttime = Time.time;
        mySprite = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<BoxCollider2D>();
        if (transform.localEulerAngles.y == 180)
        {
            direction = true;
        }
        else{
            direction = false;
        }
         
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ray;
        if (direction)
        {
            ray = Physics2D.Raycast(transform.position, Vector2.right, 15f, LayerMask.GetMask("Player"));
            Debug.DrawLine(transform.position, Vector2.right * 15f, Color.red);
        }
        else
        {
            ray = Physics2D.Raycast(transform.position, Vector2.left, 15f, LayerMask.GetMask("Player"));
            Debug.DrawRay(transform.position, Vector2.left * 15f, Color.red);
        }
        
        //Debug.Log("Colisionando con "+ray.collider.gameObject.name);
        isOnRange = (ray.collider != null);
        Shoot();
    }

    void Shoot()
    {
        
        if (isOnRange)
        {
            
            if ((starttime + FireRate) < Time.time)
            {
                Debug.Log("Hola");
                starttime = Time.time;
                Instantiate(bullet, transform.position, transform.rotation);
            }
        }
    }

    IEnumerator MiCorutina()
    {
        yield return new WaitForSeconds(0.5f);
        transform.position = new Vector2(transform.position.x, transform.position.y-0.7f);
        myAnim.SetBool("finish_dead", true);
        Destroy(myCollider);
        Destroy(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            life--;
        }
        if (life == 0)
        {
            myAnim.SetBool("isDead", true);
            StartCoroutine(MiCorutina());
        }
    }
}
