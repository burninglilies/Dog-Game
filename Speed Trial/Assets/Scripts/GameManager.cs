using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Jacovone;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static bool gameOn { get; private set; } = false;
              
    [SerializeField]
    private PathMagic pathMagicScript;

    public static bool rotateSeeSaw;

    [SerializeField]
    private DogBehaviour dogScript;

    // Start is called before the first frame update
    void Start()
    {
        HoldButtonScript.ButtonClick += HandleHoldButton;

        gameOn = false;
        pathMagicScript.VelocityBias = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Return) && gameOn == false)
        {
            StartGame();
        }*/

#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
#endif
    }

    private void HandleHoldButton(ButtonState button)
    {
        switch (button)
        {
            case ButtonState.HOLD:
                dogScript.DecreaseDogSpeed();

                break;

            case ButtonState.RELEASED:
                dogScript.IncreaseDogSpeed();

                break;

            default:

                break;
        }
    }

    public void StartGame()
    {
        gameOn = true;
        pathMagicScript.AutoStart = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        dogScript.StopAllCoroutines();
        SceneManager.LoadScene(0);
    }

    private void StopGame()
    {
        gameOn = false;
    }

    private void OnDestroy()
    {
        HoldButtonScript.ButtonClick -= HandleHoldButton;
    }
}