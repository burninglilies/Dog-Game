using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ThirdTime.Utils;
using TMPro;
using Jacovone;

public class HoldButtonScript : MonoBehaviour
{
    static public TTEvent<ButtonState> ButtonClick = new TTEvent<ButtonState>();

    public CanvasGroup holdButtonContainer;

    static public bool holdButtonAllowed { get; private set; }

    public void SetHoldButtonAllowed(bool isAllowed, bool releaseIfSetFalse = false)
    {
        holdButtonAllowed = isAllowed;

        if (isAllowed)
        {
            holdButtonContainer.gameObject.SetActive(true);
        }

        else
        {
            if(releaseIfSetFalse)
                ButtonClick.Raise(ButtonState.RELEASED);

            holdButtonContainer.gameObject.SetActive(false);
        }
    }

    public void PointerDown()
    {
        ButtonClick.Raise(ButtonState.HOLD);
    }

    public void PointerUp()
    {
        ButtonClick.Raise(ButtonState.RELEASED);
    }
}

public enum ButtonState
{
    HOLD,
    RELEASED
}
