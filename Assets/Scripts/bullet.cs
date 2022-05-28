using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    Rigidbody2D mybody;
    float dir;
    float speed;
    bool isMoving;
    Animator myAnim;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        mybody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.Translate(new Vector2(speed * dir, 0) * Time.deltaTime);
        }
        else
        {
            transform.Translate(new Vector2(0,0));
        }
    } 

    IEnumerator MiCorutina()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        /*
        if (myDirection != 180)
        {
            mybody.velocity = new Vector2(velocity, mybody.velocity.y);
        }
        else
        {
            mybody.velocity = new Vector2(-velocity, mybody.velocity.y);
        }*/
    }

    public void Shoot (float dir, float speed)
    {
        this.dir = dir;
        this.speed = speed;
        isMoving = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isMoving = false;
        //mybody.bodyType = RigidbodyType2D.Static;
        myAnim.SetTrigger("colisiono");
        StartCoroutine(MiCorutina());
    }
}
