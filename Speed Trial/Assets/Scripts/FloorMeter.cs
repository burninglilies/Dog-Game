using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ThirdTime.Utils;

public class FloorMeter : MonoBehaviour
{
    static public TTEvent<JumpAccuracy> OnJump = new TTEvent<JumpAccuracy>();

    private GameObject dog;

    private bool pressed;

    private JumpAccuracy jumpResult;
    
    private Coroutine shrinkCoroutine;

    [SerializeField]
    private Transform bigCircle;
    private Image bigCircleImage;

    [SerializeField]
    private Transform mediumCircle;
    private Image mediumCircleImage;

    [SerializeField]
    private Transform smallCircle;

    private void Start()
    {
        dog = GameObject.FindGameObjectWithTag("Dog");
        bigCircleImage = bigCircle.GetComponent<Image>();
        mediumCircleImage = mediumCircle.GetComponent<Image>();
        mediumCircleImage.SetColor(Color.red, true);
    }

    public void Jump()
    {
        pressed = true;
        StopCoroutine(shrinkCoroutine);

#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.T))
            jumpResult = JumpAccuracy.PERFECT;
#endif

        OnJump.Raise(jumpResult);
        bigCircleImage.SetColor(mediumCircleImage.color, true);
        StartCoroutine(bigCircleImage.FadeColorTo(Color.white, .5f, true));
    }

    public void PlayAnimation()
    {
        shrinkCoroutine = StartCoroutine(Shrink());
    }

    IEnumerator Shrink()
    {
        float startingDistance = Vector3.Distance(dog.transform.position, gameObject.transform.position);
        Vector3 startingScale = bigCircle.localScale;

        jumpResult = JumpAccuracy.EARLY;

        while (true)
        {
            while (bigCircle.localScale.x > mediumCircle.localScale.x)
            {
                float distance = Vector3.Distance(dog.transform.position, gameObject.transform.position);

                float a = Helpers.MapRange(distance, 1.5f, startingDistance, 1, 0);

                bigCircle.localScale = Vector3.Lerp(startingScale, mediumCircle.localScale, a);

                yield return null;
            }

            jumpResult = JumpAccuracy.PERFECT;

            mediumCircleImage.SetColor(Color.green, true);

            yield return new WaitForSecondsRealtime(.3f);

            jumpResult = JumpAccuracy.LATE;

            mediumCircleImage.SetColor(Color.yellow, true);

            while (bigCircle.localScale.x > smallCircle.localScale.x + .1f)
            {
                //                float distance = Vector3.Distance(dog.transform.position, gameObject.transform.position);

                //                float a = Helpers.MapRange(distance, 0f, startingDistance, 1, 0);

                bigCircle.localScale = Vector3.Lerp(bigCircle.localScale, smallCircle.localScale, Time.unscaledDeltaTime);

                yield return null;
            }

            mediumCircleImage.SetColor(Color.red, true);

            OnJump.Raise(JumpAccuracy.PENALTY);

            yield break;
        }
    }
}


public enum JumpAccuracy
{
    EARLY,
    PERFECT,
    LATE,
    PENALTY
}
