using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Jacovone;

public class ImpactText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI impactTextPrefab;

    [SerializeField]
    private Vector2 offset;

    [SerializeField]
    private Transform targetPoint;

    [SerializeField]
    private Transform parentTransform;

    [SerializeField]
    private Color perfectColor, okayColor, badColor;

    Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();

        FloorMeter.OnJump += HandleMeterStop;
        Seesaw.OnDogSeesawStop += HandleHoldButton;
    }

    private void HandleMeterStop(JumpAccuracy proto)
    {
        switch (proto)
        {
            case JumpAccuracy.PERFECT:
                CreateImpactText("Perfect!", perfectColor);
                break;

            case JumpAccuracy.EARLY:
                CreateImpactText("Early", badColor);
                break;

            case JumpAccuracy.LATE:
                CreateImpactText("Late", okayColor);
                break;

            case JumpAccuracy.PENALTY:
                CreateImpactText("Penalty!", badColor);
                break;

            default:
                break;
        }
    }

    void HandleHoldButton(dogStopAccuracy accuracy)
    {
        switch(accuracy)
        {
            case dogStopAccuracy.PERFECT:
                CreateImpactText("Perfect!", perfectColor);
                break;

            case dogStopAccuracy.EARLY:
                CreateImpactText("Early", okayColor);
                break;

            case dogStopAccuracy.PENALTY:
                CreateImpactText("Penalty!", badColor);
                break;

            default:

                break;
        }
    }

    void CreateImpactText(string text, Color color)
    {
        TextMeshProUGUI impactText = Instantiate(impactTextPrefab, parentTransform);
        offset = new Vector2(Random.Range(-3,4), Random.Range(-3, 4));
        impactText.rectTransform.anchoredPosition += offset * 100;
        impactText.text = text;
        impactText.color = color;
        Destroy(impactText.gameObject, 1f);
    }

    private void OnDestroy()
    {
        Seesaw.OnDogSeesawStop -= HandleHoldButton;
        FloorMeter.OnJump -= HandleMeterStop;
    }
}
