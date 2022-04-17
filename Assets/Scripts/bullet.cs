using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] float velocity;
    Transform reference;
    float myDirection;
    Rigidbody2D mybody;
    Animator myAnim;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        mybody = GetComponent<Rigidbody2D>();
        myDirection = GetComponentInParent<Transform>().localEulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator MiCorutina()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (myDirection != 180)
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
        mybody.velocity = new Vector2(0, 0);
        transform.position = new Vector2(transform.position.x, transform.position.y);
        mybody.bodyType = RigidbodyType2D.Static;
        myAnim.SetBool("colisiono", true);
        StartCoroutine(MiCorutina());
    }
}
