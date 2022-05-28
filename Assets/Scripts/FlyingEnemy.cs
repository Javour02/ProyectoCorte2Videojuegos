using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingEnemy : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] float life;
    [SerializeField] AudioClip myAudio;
    Animator myAnim;
    AIPath myPath;

    // Start is called before the first frame update
    void Start()
    {
        setEnemies.enemigos++;
        myPath = GetComponent<AIPath>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
    }

    void ChasePlayer()
    {
        /* float d = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log("Distancia con jugador: " + d);
        if (d < 8)
        {

        }
        Debug.DrawLine(transform.position, player.transform.position, Color.red);*/

        Collider2D col = Physics2D.OverlapCircle(transform.position, 8f, LayerMask.GetMask("Player"));
        if (col != null)
        {
            myPath.isStopped = false;
        }
        else
        {
            myPath.isStopped=true;
        }

    }

    IEnumerator MiCorutina()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 8f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            life--;
        }
        if(life == 0)
        {
            setEnemies.enemigos--;
            AudioSource.PlayClipAtPoint(myAudio, new Vector2(transform.position.x, transform.position.y));
            myAnim.SetBool("died", true);
            StartCoroutine(MiCorutina());
        }
    }
}
