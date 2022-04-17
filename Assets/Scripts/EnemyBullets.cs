using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullets : MonoBehaviour
{

    float myDirection;
    [SerializeField] float velocity;
    Rigidbody2D mybody;
    // Start is called before the first frame update
    void Start()
    {
        myDirection = GetComponentInParent<Transform>().localEulerAngles.y;
        mybody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (myDirection == 180)
        {
            mybody.velocity = new Vector2(velocity, mybody.velocity.y);
        }
        else
        {
            mybody.velocity = new Vector2(-velocity, mybody.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
