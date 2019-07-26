using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float smoothTime = 0.125f;
    [SerializeField]
    private Vector3 offset;

    private Vector3 velocity = Vector3.zero;
    private bool follow;

    private float targetStartingY, cameraStartingY;

    private void Start()
    {
        //targetStartingY = target.position.y;
        //cameraStartingY = transform.position.y;
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        //desiredPosition.y = cameraStartingY;

        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        transform.position = smoothedPosition;

		transform.LookAt(target);//new Vector3(target.position.x, targetStartingY, target.position.z));
    }
}

public static class Helpers
{
    static public void SetLocalXPosition(this Transform transform, float newxpos)
    {
        transform.localPosition = new Vector3(newxpos, transform.localPosition.y, transform.localPosition.z);
    }

    static public void SetLocalYPosition(this Transform transform, float newypos)
    {
        transform.localPosition = new Vector3(transform.localPosition.y, newypos, transform.localPosition.z);
    }

    static public void SetLocalYPosition(this Jacovone.Waypoint waypoint, float newypos)
    {
        Vector3 temp = waypoint.Position;
        temp.y = newypos;
        waypoint.Position = temp;
    }

    static public IEnumerator ChangeTimeScale(float targetTimeScale, float speed)
    {
        float lerpTime = speed;
        float lerpCurrentTime = 0;

        float currentTimeScale = Time.timeScale;

        while (lerpCurrentTime < lerpTime)
        {
            lerpCurrentTime += Time.unscaledDeltaTime;

            float a = lerpCurrentTime / lerpTime;

            a = Mathf.Sin(a * Mathf.PI * 0.5f);

            Time.timeScale = Mathf.Lerp(currentTimeScale, targetTimeScale, a);

            yield return null;
        }
    }

    public static float MapRange(float srcVal, float srcMin, float srcMax, float tgtMin, float tgtMax)
    {
        srcVal = Mathf.Clamp(srcVal, srcMin, srcMax);

        float retVal = tgtMin + ((srcVal - srcMin) * (tgtMax - tgtMin)) / (srcMax - srcMin);

        if (tgtMax < tgtMin)
        {
            retVal = Mathf.Clamp(retVal, tgtMax, tgtMin);
        }
        else
        {
            retVal = Mathf.Clamp(retVal, tgtMin, tgtMax);
        }

        return retVal;
    }

    static public IEnumerator LerpRotation(this Transform transform, Quaternion targetRotation, float time = 1f)
    {
        float lerpTime = time;
        float lerpCurrentTime = 0;

        Quaternion startingRot = transform.rotation;

        while(lerpCurrentTime < lerpTime)
        {
            lerpCurrentTime += Time.unscaledDeltaTime;
            float a = lerpCurrentTime / lerpTime;

            transform.rotation = Quaternion.Slerp(startingRot, targetRotation, a);
            yield return null;
        }
    }

    public static void Reshuffle(this SpriteRenderer[] texts)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < texts.Length; t++)
        {
            SpriteRenderer tmp = texts[t];
            int r = Random.Range(t, texts.Length);
            texts[t] = texts[r];
            texts[r] = tmp;
        }
    }

    public static IEnumerator FadeColorTo(this Image img, Color newColor, float time = 1f, bool ignoreAlpha = true)
    {
        float lerpTime = time;
        float lerpCurrentTime = 0;

        Color ogColor = img.color;

        if (ignoreAlpha)
            newColor.a = ogColor.a;

        while(lerpCurrentTime < lerpTime)
        {
            lerpCurrentTime += Time.unscaledDeltaTime;
            float progress = lerpCurrentTime / lerpTime;

            img.color = Color.Lerp(ogColor, newColor, progress);

            yield return null;
        }
    }

    public static void SetColor(this Image img, Color newColor, bool ignoreAlpha = true)
    {
        if (ignoreAlpha)
            newColor.a = img.color.a;

        img.color = newColor;
    }

    public static float ProportionallyInvert(float referenceValue, float rangeMin = 0, float rangeMax = 100)
    {
        float pI = Mathf.Lerp(rangeMin, rangeMax, Mathf.InverseLerp(rangeMax, rangeMin, referenceValue));

        return pI;
    }
}
