using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using ThirdTime.Utils;
using TMPro;

public class PrototypeMeter : MonoBehaviour
{
    static public TTEvent<ProtoAccuracy> OnMeterStop = new TTEvent<ProtoAccuracy>();

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
    public TextMeshProUGUI tapLabel;
    
    private Tweener _barMoveTweener;
    private bool _meterInProgress;
    private bool _isMeterAllowed;

    public ProtoAccuracy playerResult;

    private void Start()
    {
        ResultText.text = ""; 
        MeterContainer.transform.localScale = new Vector3(0,1,1);
    }

    public void SetMeterAllowed(bool isAllowed)
    {
        _isMeterAllowed = isAllowed;

        if (isAllowed)
        {
            MeterContainerGroup.alpha = 1f;
            MeterContainerGroup.blocksRaycasts = true;
            //tapLabel.alpha = 1f;
            StartMeter();
        }
        else
        {
            MeterContainerGroup.alpha = .2f;
            MeterContainerGroup.blocksRaycasts = false;
            //tapLabel.alpha = 0f;
        }    
    }

    public void OnMeterButtonPressed()
    {
        /*if (_meterInProgress)
        {
            StopMeter(); 
        }
        else if (_isMeterAllowed)
        {
            if (!_meterInProgress)
            {
                StartMeter();
            }
        }*/

        if(_isMeterAllowed)
        {
            StopMeter();
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
        _barMoveTweener = MovementBar.DOLocalMoveY(256, BarMoveTime).SetEase(BarEase).OnComplete(StopMeter).SetUpdate(UpdateType.Normal, true);
    }

    public void StopMeter()
    {
        _meterInProgress = false;   

        if (_barMoveTweener != null)
            _barMoveTweener.Kill();

        MovementBar.transform.DOPunchScale(new Vector3(.05f, .05f, .05f), .25f).SetUpdate(UpdateType.Normal, true);

        OnMeterStop.Raise(DetermineMeterAccuracy());
    }

    ProtoAccuracy DetermineMeterAccuracy()
    {
        ProtoAccuracy retAccuracy = ProtoAccuracy.GOOD;

        int stopY = Mathf.RoundToInt(MovementBar.localPosition.y);

        Debug.Log("Bar stopped at " + MovementBar.localPosition.y);

        Color toColor = Color.white;
                                                                          //HotKey to get it perfect!! (Test only)
        if (MinMaxPerfect.end >= stopY && MinMaxPerfect.start <= stopY || Input.GetKey(KeyCode.T))
        {
            ResultText.text = "PERFECT!";
            toColor = PerfectColor;
            retAccuracy = ProtoAccuracy.BEST;
        }                                                                //HotKey to get it perfect!! (Test only)
        else if (MinMaxOkay.end >= stopY && MinMaxOkay.start <= stopY || Input.GetKey(KeyCode.Y))
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
        ResultText.DOFade(0, 1).SetUpdate(UpdateType.Normal, true);
        ResultText.transform.localPosition = Vector3.zero;
        ResultText.transform.DOLocalMoveY(100, .75f).SetUpdate(UpdateType.Normal, true);


        foreach (Image img in ImagesToColor)
        {
            Color origColor = img.color;
            img.color = toColor;
            img.DOColor(origColor, 1f).SetUpdate(UpdateType.Normal, true);
        }

        MeterContainer.DOScaleX(0, .25f).SetDelay(.45f).SetUpdate(UpdateType.Normal, true);

        return retAccuracy;
    }
}

public enum ProtoAccuracy
{
    NULL,
    GOOD,
    BETTER,
    BEST
}
