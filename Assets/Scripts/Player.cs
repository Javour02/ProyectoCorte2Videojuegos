using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float fireRate;
    [SerializeField] AudioClip myAudio;
    [SerializeField] GameObject bul;
    Rigidbody2D mybody;
    Animator myAnim;
    float starttime;
    bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        mybody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        //StartCoroutine(MiCorutina());
        starttime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, 1.3f, LayerMask.GetMask("Piso"));
        Debug.DrawRay(transform.position, Vector2.down * 1.3f, Color.red);
        //Debug.Log("Colisionando con "+ray.collider.gameObject.name);
        isGrounded = (ray.collider != null);
        Jump();
        Fire();
    }

    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            myAnim.SetLayerWeight(1, 1);
            if ((starttime+fireRate)<Time.time)
            {
                starttime = Time.time;
                Instantiate(bul, transform.position, transform.rotation);
                StartCoroutine(MiCorutina());
            }
        }
    }

    void firemessage()
    {
        Debug.Log("Hola hola que tal");
    }

    IEnumerator MiCorutina()
    {
        Debug.Log("Esperando para pasar de animación");
        yield return new WaitForSeconds(0.5f);
        myAnim.SetLayerWeight(1, 0);
    }

    IEnumerator MiCorutina2()
    {
        Time.timeScale = 0;
        float FirstTime = Time.realtimeSinceStartup + 1f;
        while (Time.realtimeSinceStartup < FirstTime)
        {
            yield return 0;
        }
        Debug.Log("LLegue");
        AudioSource.PlayClipAtPoint(myAudio, new Vector2(transform.position.x, transform.position.y));
        float SecondTime = Time.realtimeSinceStartup + 1f;
        while (Time.realtimeSinceStartup < SecondTime)
        {
            yield return 0;
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Jump()
    {
        if (isGrounded && !myAnim.GetBool("isJumping"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myAnim.SetBool("isJumping", true);
                mybody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
        if(mybody.velocity.y != 0 && !isGrounded)
        {
            myAnim.SetBool("isJumping", true);
        }
        else myAnim.SetBool("isJumping", false);

    }

    private void FixedUpdate()
    {
        float dirH = Input.GetAxis("Horizontal");
        mybody.velocity = new Vector2(dirH * speed, mybody.velocity.y);

        if(dirH != 0)
        {
            myAnim.SetBool("isRunning", true);
            if (dirH < 0)
            {
                transform.eulerAngles = new Vector2(0, 180);
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 0);
                //transform.localScale = new Vector2(1, 1);
            }
        }
        else
        {
            myAnim.SetBool("isRunning", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 10 || collision.gameObject.layer == 9)
        {
            myAnim.SetBool("isDead", true);
            StartCoroutine(MiCorutina2());        
        }
    }
}
