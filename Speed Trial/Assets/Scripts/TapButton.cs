using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThirdTime.Utils;
using TMPro;

public class TapButton : MonoBehaviour
{
    public CanvasGroup tapButtonContainer;

    static public bool tapButtonAllowed { get; private set;}

    public void SetTapButtonAllowed(bool isAllowed)
    {
        tapButtonAllowed = isAllowed;

        if (isAllowed)
        {
            tapButtonContainer.gameObject.SetActive(true);
        }

        else
        {
            tapButtonContainer.gameObject.SetActive(false);
        }
    }
}