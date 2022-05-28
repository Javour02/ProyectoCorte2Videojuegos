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
    [SerializeField] AudioClip myAudio2;
    [SerializeField] GameObject bul;
    [SerializeField] float bulletSpeed;
    Rigidbody2D mybody;
    Animator myAnim;
    float starttime;
    bool wall;
    public static bool isGrounded = true;

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
        RaycastHit2D ray = Physics2D.Raycast(new Vector2(transform.position.x - (1.776025f / 2), transform.position.y), Vector2.down, 1.3f, LayerMask.GetMask("Piso"));
        RaycastHit2D ray2 = Physics2D.Raycast(new Vector2(transform.position.x + (1.776025f / 2),transform.position.y), Vector2.down, 1.3f, LayerMask.GetMask("Piso"));
        Debug.DrawRay(new Vector2(transform.position.x - (1.776025f / 2), transform.position.y), Vector2.down * 1.3f, Color.red);
        Debug.DrawRay(new Vector2(transform.position.x + (1.776025f / 2), transform.position.y), Vector2.down * 1.3f, Color.red);
        //Debug.Log("Colisionando con "+ray.collider.gameObject.name);
        isGrounded = (ray.collider != null || ray2.collider != null);
        Jump();
        Fire();
    }

    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Z) && (starttime + fireRate) < Time.time)
        {
            starttime = Time.time;
            GameObject myBullet = Instantiate(bul, new Vector2(transform.position.x, transform.position.y + 0.1f), transform.rotation);
            myBullet.GetComponent<bullet>().Shoot(transform.localScale.x, bulletSpeed);
            myAnim.SetLayerWeight(1, 1);
        }else if ((starttime + fireRate + 1f) < Time.time)
        {
            myAnim.SetLayerWeight(1, 0);
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
        setEnemies.enemigos = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Jump()
    {
        if (isGrounded && !myAnim.GetBool("isJumping"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myAnim.SetBool("isJumping", true);
                AudioSource.PlayClipAtPoint(myAudio2, new Vector2(transform.position.x, transform.position.y));
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
        if (wall)
        {
            float dirH = Input.GetAxis("Horizontal");
            Vector2 scale = transform.localScale;
            Vector2 right = new Vector2(1, 1);
            Vector2 left = new Vector2(-1, 1);
            if (scale == right && dirH < 0 || scale == left && dirH > 0)
            {
                mybody.velocity = new Vector2(dirH * speed, mybody.velocity.y);
            }
            else
            {
                mybody.velocity = new Vector2(0, mybody.velocity.y);
            }
        }
        else
        {
            float dirH = Input.GetAxis("Horizontal");
            if (dirH != 0)
            {
                mybody.velocity = new Vector2(dirH * speed, mybody.velocity.y);
                myAnim.SetBool("isRunning", true);
                if (dirH < 0)
                {
                    transform.localScale = new Vector2(-1, 1);
                    //transform.eulerAngles = new Vector2(0, 180);
                }
                else
                {
                    transform.localScale = new Vector2(1, 1);
                }
            }
            else
            {
                myAnim.SetBool("isRunning", false);
            }
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

    private void OnCollisionExit2D(Collision2D collision)
    {
       wall = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isGrounded && collision.gameObject.layer == 3)
        {
            wall = true;
            Debug.Log(wall);
        }

        if (isGrounded)
        {
            wall = false;
        }
    }
}
