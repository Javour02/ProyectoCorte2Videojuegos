using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MenuGanador : MonoBehaviour
{
    Button menu;
    // Start is called before the first frame update
    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        menu = root.Q<Button>("menu");
        menu.clicked += irAlMenu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void irAlMenu()
    {
        Destroy(GameObject.Find("Audio"));
        SceneManager.LoadScene("Menu");
    }
}
