using System;
using DG.Tweening;
using UnityEngine;

public class UISettings : MonoBehaviour
{
    public const float PointSystemToggleTransitionTime = .4f;
    public const Ease PointSystemToggleTransitionEase = Ease.OutExpo;
    public const float SceneSwipeTransitionTime = .4f;
    public const Ease SceneSwipeTransitionEase = Ease.OutExpo;
    public static float objectsSwipeMoveScalerToUI = 0;

    private void Start()
    {
        var cam = Camera.main;
        if (cam != null)
        {
            var deltaPos = cam.ScreenToWorldPoint(new Vector2(Screen.width / 2f, 0)) -
                           cam.ScreenToWorldPoint(new Vector2(-Screen.width / 2f, 0));
            objectsSwipeMoveScalerToUI = deltaPos.x / Screen.width;
        }
    }
}