using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThirdTime.Utils;
using Jacovone;

public class Seesaw : MonoBehaviour
{
    public static TTEvent<dogStopAccuracy> OnDogSeesawStop = new TTEvent<dogStopAccuracy>();

    private Rigidbody seeSawRB;

    private bool dogStopped;
    private bool early;
    private bool resultDetermined;

    private GameObject dog;

    private Coroutine rotateCoroutine;
    private Coroutine limitXRotCoroutine;

    [SerializeField]
    private HoldButtonScript holdButtonScript;

    private Animator blinkingImageAnim;

    private void Start()
    {
        seeSawRB = GetComponent<Rigidbody>();
        OnDogSeesawStop += disableHoldButton;
        blinkingImageAnim = gameObject.GetComponentInChildren<Animator>();

        early = true;
    }

    void disableHoldButton(dogStopAccuracy result)
    {
        holdButtonScript.SetHoldButtonAllowed(false, true);
    }

    public void StartBlinkAnim()
    {
        blinkingImageAnim.SetBool("Blink", true);
    }

    public void StopBlinkAnim()
    {
        blinkingImageAnim.SetBool("Blink", false);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Dog")
        {
            early = false;

            StopBlinkAnim();

            if (dogStopped && !resultDetermined)
            {
                OnDogSeesawStop.Raise(dogStopAccuracy.PERFECT);
                resultDetermined = true;
            }
        }
    }

    private IEnumerator LimitXRotation(GameObject affectedGameObject,float min, float max)
    {
        while(true)
        {
            if (affectedGameObject.transform.localRotation.eulerAngles.x < 300)
            {
                Vector3 currentRotation = affectedGameObject.transform.localRotation.eulerAngles;
                currentRotation.x = Mathf.Clamp(currentRotation.x, min, max);
                affectedGameObject.transform.localRotation = Quaternion.Euler(currentRotation);
            }

            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Dog")
        {
            dog = collision.gameObject;
            limitXRotCoroutine = StartCoroutine(LimitXRotation(dog, -55, 55));
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Dog")
        {
            if (DogBehaviour.dogSpeed <= 0.0f)
            {
                seeSawRB.useGravity = true;
                seeSawRB.isKinematic = false;
                dogStopped = true;

                if(early && !resultDetermined)
                {
                    OnDogSeesawStop.Raise(dogStopAccuracy.EARLY);
                    resultDetermined = true;
                }
            }

            else
            {
                dogStopped = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "Dog")
        { 
            if (!resultDetermined)
            {
                OnDogSeesawStop.Raise(dogStopAccuracy.PENALTY);
                resultDetermined = true;
            }

            StopCoroutine(limitXRotCoroutine);
            StartCoroutine(other.transform.LerpRotation(Quaternion.Euler(new Vector3(Quaternion.identity.eulerAngles.x,
                                                                                        other.transform.rotation.eulerAngles.y,
                                                                                        other.transform.rotation.eulerAngles.z)), 0.5f));
        }
    }

    private void OnDisable()
    {
        OnDogSeesawStop -= disableHoldButton;
    }
}

public enum dogStopAccuracy 
{
    PERFECT,
    EARLY,
    PENALTY
}
