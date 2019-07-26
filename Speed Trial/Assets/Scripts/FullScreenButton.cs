using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FullScreenButton : MonoBehaviour
{
    TextMeshProUGUI quicklyTapText;

    private void Start()
    {
        quicklyTapText = transform.parent.gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void DeactivateText()
    {
        quicklyTapText.gameObject.SetActive(false);
    }
}
