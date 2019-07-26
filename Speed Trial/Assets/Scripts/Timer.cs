using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Jacovone;

public class Timer : MonoBehaviour
{
    //Timer animation
    private Animator timerAnim;

    //Timer text
    public TextMeshProUGUI timerText;
    private float seconds;

    [SerializeField]
    private float penaltySecondsAdded = 3f;

    private float timeAhead;
    private float timeWastedOnScreen;

    // Start is called before the first frame update
    void Start()
    {
        FloorMeter.OnJump += HandleMeterStop;
        Seesaw.OnDogSeesawStop += HandleSeesawStop;

        timerText = GetComponentInChildren<TextMeshProUGUI>();
        timerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.gameOn)
        {
            seconds = Time.timeSinceLevelLoad - timeWastedOnScreen % 60f + timeAhead;

            timerText.text = seconds.ToString("00.0");
        }

        else
        {
            timeWastedOnScreen = Time.timeSinceLevelLoad % 60f;
        }
    }

    private void HandleMeterStop(JumpAccuracy proto)
    {
        switch (proto)
        {
            case JumpAccuracy.PERFECT:
                timerAnim.SetTrigger("Success");
                break;
                    
            case JumpAccuracy.EARLY:
                timerAnim.SetTrigger("Fail");
                break;

            case JumpAccuracy.LATE:
                timerAnim.SetTrigger("Regular");
                 
                break;

            case JumpAccuracy.PENALTY:
                timerAnim.SetTrigger("Penalty");
                timeAhead += penaltySecondsAdded;
                break;

            default:
                break;
        }
    }

    private void HandleSeesawStop(dogStopAccuracy accuracy)
    {
        switch (accuracy)
        {
            case dogStopAccuracy.PERFECT:

                timerAnim.SetTrigger("Success");
                break;

            case dogStopAccuracy.EARLY:

                timerAnim.SetTrigger("Regular");
                break;

            case dogStopAccuracy.PENALTY:

                timerAnim.SetTrigger("Penalty");
                timeAhead += penaltySecondsAdded;
                break;

            default:

                break;
        }
    }

    private void OnDestroy()
    {
        FloorMeter.OnJump -= HandleMeterStop;
        Seesaw.OnDogSeesawStop -= HandleSeesawStop;
    }
}
