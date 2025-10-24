using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    public Button startBtn;

    void Start()
    {
        startBtn.onClick.AddListener(()=> { 
             SceneManager.LoadScene("MainScene");
        });
    }
   
    void Update()
    {
        
    }
}
