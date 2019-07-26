using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class PrototypeMeter : MonoBehaviour
{
    [Header("Inspector component links")]
    public CanvasGroup MeterContainerGroup; 
    public Transform MeterContainer; 
    public Transform MovementBar;

    [Header("Colors")]
    public Color PerfectColor; 
    public Color GreatColor; 
    public Color BadColor;
    public Image[] ImagesToColor; 

    [Header("Tunable Values")]
    public RangeInt MinMaxPerfect = new RangeInt(188, 4);
    public RangeInt MinMaxOkay = new RangeInt(197, 23);
    public Ease BarEase;
    public float BarMoveTime;
    public Text ResultText;
    
    private Tweener _barMoveTweener;
    private bool _meterInProgress;
    private bool _isMeterAllowed; 

    private void Start()
    {
        SetMeterAllowed(true);
        ResultText.text = ""; 
        MeterContainer.transform.localScale = new Vector3(0,1,1); 
    }

    public void SetMeterAllowed(bool isAllowed)
    {
        _isMeterAllowed = isAllowed;

        if (isAllowed)
        {
            MeterContainerGroup.alpha = 1f;
        }
        else
        {
            MeterContainerGroup.alpha = .2f; 
        }
        
    }

    public void OnMeterButtonPressed()
    {
        if (_meterInProgress)
        {
            StopMeter(); 
        }
        else if (_isMeterAllowed)
        {
            if (!_meterInProgress)
            {
                StartMeter();
            }
        }
    }

    private void StartMeter()
    {        
        _meterInProgress = true;

        ResultText.text = ""; 

        MeterContainer.gameObject.SetActive(true);
        MeterContainer.localScale = Vector3.one; 

        if (_barMoveTweener != null)
        {
            _barMoveTweener.Kill();
        }

        // reset the movement bar to the bottom so it can tween back up. 
        MovementBar.localPosition = new Vector3(MovementBar.localPosition.x, 0, MovementBar.localPosition.z); 
        _barMoveTweener = MovementBar.DOLocalMoveY(256, BarMoveTime).SetEase(BarEase).OnComplete(StopMeter);
    }

    public void StopMeter()
    {
        _meterInProgress = false;

        if (_barMoveTweener != null)
            _barMoveTweener.Kill();

        MovementBar.transform.DOPunchScale(new Vector3(.05f, .05f, .05f), .25f);

        DetermineMeterAccuracy();         
    }

    ProtoAccuracy DetermineMeterAccuracy()
    {
        ProtoAccuracy retAccuracy = ProtoAccuracy.GOOD;
    
        int stopY = Mathf.RoundToInt(MovementBar.localPosition.y);

        Debug.Log("Bar stopped at " + MovementBar.localPosition.y);

        Color toColor = Color.white; 

        if (MinMaxPerfect.end >= stopY && MinMaxPerfect.start <= stopY)
        {
            ResultText.text = "PERFECT!";
            toColor = PerfectColor;
            retAccuracy = ProtoAccuracy.BEST;
        }
        else if (MinMaxOkay.end >= stopY && MinMaxOkay.start <= stopY)
        {
            ResultText.text = "SOLID!";
            toColor = GreatColor; 
            retAccuracy = ProtoAccuracy.BETTER;
        }
        else if (stopY > MinMaxOkay.end)
        {
            ResultText.text = "LATE!";
            toColor = BadColor; 
            retAccuracy = ProtoAccuracy.GOOD;
        }
        else if (stopY < MinMaxPerfect.start)
        {
            ResultText.text = "EARLY!"; 
            toColor = BadColor; 
            retAccuracy = ProtoAccuracy.GOOD;
        }

        ResultText.color = toColor;
        ResultText.DOFade(0, 1);
        ResultText.transform.localPosition = Vector3.zero; 
        ResultText.transform.DOLocalMoveY(100, .75f);


        foreach (Image img in ImagesToColor)
        {
            Color origColor = img.color;
            img.color = toColor;
            img.DOColor(origColor, 1f);
        }

        MeterContainer.DOScaleX(0, .25f).SetDelay(.45f);

        return retAccuracy;
    }
    
    
}

public enum ProtoAccuracy
{
    GOOD,
    BETTER,
    BEST
}
