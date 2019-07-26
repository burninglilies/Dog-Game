using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jacovone;

public class DogBehaviour : MonoBehaviour
{
    public static string currentObstacle { get; private set; }
    private string seeSaw = "Seesaw";
    private string hurdle = "Hurdle";

    public Rigidbody dogRB { get; private set; }

    [SerializeField]
    private PathMagic pathMagicScript;

    public Coroutine changeSpeedCoroutine;

    private Coroutine slowDownTimeCoroutine;

    private Coroutine speedUpTimeCoroutine;

    private Animator dogAnim;

    private float standardTimeScale = 1f;

    private float slowDownTimeScale = 0f;

    private float changeTimeScaleSpeed = 1.7f;

    private float speed = 0f;

    [SerializeField]
    private HoldButtonScript holdButtonScript;

    [SerializeField]
    private TapButton tapButtonScript;

    [SerializeField]
    private GameObject fullScreenButton;

    public static float dogSpeed { get; private set; }

    [SerializeField]
    private FloorMeter[] floorMeters;
    private int currentMeterIndex;

    [SerializeField]
    private int[] waypointJumpNumbers;
    private int currentJumpIndex;

    private bool manuallyRun;

    // Start is called before the first frame update
    void Awake()
    {
        dogAnim = GetComponentInChildren<Animator>();   
        dogRB = GetComponentInChildren<Rigidbody>();

        FloorMeter.OnJump += HandleTap;

        if (tapButtonScript != null)
            tapButtonScript.SetTapButtonAllowed(false);

        if (holdButtonScript != null)
            holdButtonScript.SetHoldButtonAllowed(false);

        standardTimeScale = 1f;
    }

    private void HandleTap(JumpAccuracy accuracy)
    {
        switch (accuracy)
        {
            case JumpAccuracy.PERFECT:

                if (currentObstacle == hurdle)
                    HurdleObstacle(1, 0.1f);
                break;

            case JumpAccuracy.LATE:
                if (currentObstacle == hurdle)
                    HurdleObstacle(0.5f, 0.07f);
                break;

            case JumpAccuracy.EARLY:
                if (currentObstacle == hurdle)
                    HurdleObstacle(0f, 0.04f);
                break;

            case JumpAccuracy.PENALTY:
                if (currentObstacle == hurdle)
                    HurdleObstacle(0f, 0.04f);
                break;

            default:

                break;
        }

        currentMeterIndex++;
        currentJumpIndex++;
        SpeedUpTime();
    }

    public void ObstacleIdentifier(string obstacle)
    {
        currentObstacle = obstacle;

        if (currentObstacle == hurdle)
        {
            if (tapButtonScript != null)
                tapButtonScript.SetTapButtonAllowed(true);

            else
            Debug.LogWarning("There is no reference to the tap button script.");

            if (holdButtonScript != null)
                holdButtonScript.SetHoldButtonAllowed(false);

            if (manuallyRun)
            {
                if (changeSpeedCoroutine != null)
                    StopCoroutine(changeSpeedCoroutine);

                changeSpeedCoroutine = StartCoroutine(SetVelocityBias(0.1f, 0.5f));
                manuallyRun = false;
            }

            SlowDownTime();

            dogRB.constraints = RigidbodyConstraints.FreezeAll;
        }

        else if (currentObstacle == seeSaw)
        {
            if (holdButtonScript != null)
                holdButtonScript.SetHoldButtonAllowed(true);

            else
                Debug.LogWarning("There is no reference to the hold button script.");

            if (tapButtonScript != null)
                tapButtonScript.SetTapButtonAllowed(false);

            dogRB.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
    }

    private void HurdleObstacle(float jumpHeight, float velocity)
    {
        SetJumpHeight(jumpHeight);
        changeSpeedCoroutine = StartCoroutine(SetVelocityBias(velocity));
    }

    private void SpeedUpTime()
    {
        if (slowDownTimeCoroutine != null)
            StopCoroutine(slowDownTimeCoroutine);

        speedUpTimeCoroutine = StartCoroutine(Helpers.ChangeTimeScale(standardTimeScale, changeTimeScaleSpeed));
        tapButtonScript.SetTapButtonAllowed(false);
    }

    private void SlowDownTime()
    {
        if (speedUpTimeCoroutine != null)
            StopCoroutine(speedUpTimeCoroutine);

        slowDownTimeCoroutine = StartCoroutine(Helpers.ChangeTimeScale(slowDownTimeScale, changeTimeScaleSpeed));
    }

    public void IncreaseDogSpeed()
    {
        if (changeSpeedCoroutine != null)
            StopCoroutine(changeSpeedCoroutine);

        changeSpeedCoroutine = StartCoroutine(SetVelocityBias(0.1f));
    }

    public void DecreaseDogSpeed(float time = .5f)
    {
        if (changeSpeedCoroutine != null)
            StopCoroutine(changeSpeedCoroutine);

        changeSpeedCoroutine = StartCoroutine(SetVelocityBias(0f, time));
    }

    public void SetVelocityBiasInspector(float targetVelocity)
    {
        if(changeSpeedCoroutine != null)
            StopCoroutine(changeSpeedCoroutine);

        changeSpeedCoroutine = StartCoroutine(SetVelocityBias(targetVelocity));
    }

    public void SetDogConstraints()
    {
        dogRB.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void SetJumpHeight(float height)
    {
        pathMagicScript.Waypoints[waypointJumpNumbers[currentJumpIndex] - 1].SetLocalYPosition(height);
             
        pathMagicScript.UpdatePathSamples();
    }

    public void AnimateFloorMeter()
    {
        if (floorMeters[currentMeterIndex] != null)
        {
            floorMeters[currentMeterIndex].PlayAnimation();
        }
    }

    public void OnTap()
    {
        if (floorMeters[currentMeterIndex] != null)
            floorMeters[currentMeterIndex].Jump();
    }

    public void FullScreenTap()
    {
        print("Tapped!");

        if (changeSpeedCoroutine != null)
           StopCoroutine(changeSpeedCoroutine);

        if (speed < 0.1f)
        {
            speed += 0.02f;
            speed = Mathf.Clamp(speed, 0, 0.1f);
            manuallyRun = true;
        }

        else
        {
            fullScreenButton.SetActive(false);
            manuallyRun = false;
        }

        changeSpeedCoroutine = StartCoroutine(SetVelocityBias(speed, 1f));
    }

    public IEnumerator SetVelocityBias(float targetVelocityBias, float time = 3f)
    {
        float lerpTime = time;
        float lerpCurrentTime = 0f;

        float startingValue = pathMagicScript.VelocityBias;

        while (lerpCurrentTime < lerpTime)
        {
            lerpCurrentTime += Time.unscaledDeltaTime;
            float a = lerpCurrentTime / lerpTime;

            pathMagicScript.VelocityBias = Mathf.Lerp(startingValue, targetVelocityBias, a);
            dogAnim.SetFloat("DogSpeed", Helpers.MapRange(pathMagicScript.VelocityBias, 0f, 0.1f, 0, 1));

            dogSpeed = pathMagicScript.VelocityBias;

            yield return null;
        }
    }

    private void OnDisable()
    {
        FloorMeter.OnJump -= HandleTap;
    }
}
