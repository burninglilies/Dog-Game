using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public bool isStart;
    public bool isTutorial;
    public bool isLevels;

    public int currentLevel;

    void MoveUp()
    {
        Application.LoadLevel(int 5);

        SceneManager.LoadScene(demo_scene);
    }




    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
