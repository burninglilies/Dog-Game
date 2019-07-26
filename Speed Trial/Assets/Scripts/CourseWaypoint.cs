using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CourseWaypoint
{
    public Vector3 Position;

    public Quaternion Rotation;

    public GameObject PreviewObject;

    public CourseWaypoint (Vector3 position, Quaternion rotation, GameObject previewObject)
    {
        Position = position;

        Rotation = rotation;

        PreviewObject = previewObject;
    }

    public void HidePreviewObject()  
	{
        PreviewObject.transform.DOScale(0, .5f).OnComplete(() =>
         {
             PreviewObject.SetActive(false);
         });
	}

}
