using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour
{
    Button startButton;
    // Start is called before the first frame update
    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        startButton = root.Q<Button>("start-game");
        startButton.clicked += startGame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
