using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class setEnemies : MonoBehaviour
{
    TMP_Text myText;
    public static int enemigos = 0;
    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<TMP_Text>();
        Debug.Log(myText.text);
    }

    // Update is called once per frame
    void Update()
    {
        ContarEnemigos();
        if (enemigos == 0)
        {
            CambiarEscena();
        }
    }

    void ContarEnemigos()
    {

        myText.text = "Enemigos restantes = "+enemigos;
    }

    IEnumerator MiCorutina2()
    {
        Time.timeScale = 0;
        float FirstTime = Time.realtimeSinceStartup + 1f;
        while (Time.realtimeSinceStartup < FirstTime)
        {
            yield return 0;
        }
        Time.timeScale = 1;
        SceneManager.LoadScene("Ganador");
    }

    void CambiarEscena()
    {
        StartCoroutine(MiCorutina2());
    }
}
