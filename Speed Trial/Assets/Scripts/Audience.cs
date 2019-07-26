using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audience : MonoBehaviour
{
    private SpriteRenderer[] audienceSprites;

    private Coroutine animateCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        audienceSprites = GetComponentsInChildren<SpriteRenderer>();

        animateCoroutine = StartCoroutine(AnimateAudience());
    }

    // Update is called once per frame
    IEnumerator AnimateAudience()
    {
        while (true)
        {
            audienceSprites.Reshuffle();

            for (var i = 0; i < audienceSprites.Length; i++)
            {
                audienceSprites[i].flipX = !audienceSprites[i].flipX;
            }

            yield return new WaitForSeconds(.25f);
        }
    }
}


