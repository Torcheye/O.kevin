using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointSystem : MonoBehaviour
{
    [SerializeField] private int maxPointsAvailable = 10;
    private int PointsAvailable { get; set; }
    private Emotion[] _emotions;

    private List<TextMeshProUGUI> _valueList;
    private TextMeshProUGUI _pointsAvailableText;

    private void Start()
    {
        _emotions = new Emotion[4];
        for (var i = 0; i < 4; i++)
        {
            _emotions[i] = new Emotion(0);
        }

        PointsAvailable = maxPointsAvailable;
        _pointsAvailableText = GameObject.Find("PointsAvailable").GetComponent<TextMeshProUGUI>();
        _valueList = GetComponentsInChildren<TextMeshProUGUI>().ToList()
            .FindAll(ugui => ugui.CompareTag("PointSystem.Value"));
        
        var buttons = GetComponentsInChildren<Button>().ToList()
            .FindAll(button => button.CompareTag("PointSystem.Minus") || button.CompareTag("PointSystem.Plus"));
        foreach (var button in buttons)
        {
            button.onClick.AddListener(() => UpdateValue(button.name, button.CompareTag("PointSystem.Plus")));
        }

        var doneButton = GameObject.Find("Done").GetComponent<Button>();
        doneButton.onClick.AddListener(() => { Toggle(true); });
    }

    private void UpdateValue(string valueName, bool plus)
    {
        if (PointsAvailable == maxPointsAvailable && !plus || PointsAvailable == 0 && plus) return;

        var text = _valueList.Find(ugui => ugui.name == valueName);
        var increment = plus ? 1 : -1;

        var emotion = valueName switch
        {
            "Happiness" => _emotions[0],
            "Sadness" => _emotions[1],
            "Fear" => _emotions[2],
            "Anger" => _emotions[3],
            _ => throw new ArgumentOutOfRangeException(nameof(valueName), valueName, null)
        };

        if (emotion.Value == 0 && !plus) return;
        emotion.Value += increment;
        text.text = emotion.Value.ToString();

        PointsAvailable -= increment;
        _pointsAvailableText.text = PointsAvailable.ToString();
    }

    private void Toggle(bool down)
    {
        const Ease ease = Ease.OutExpo;
        const float timing = .4f;
        var canvasGroup = GetComponent<CanvasGroup>();
        DOTween.To(() => canvasGroup.alpha, value => canvasGroup.alpha = value, down ? 0 : 1, timing)
            .SetEase(ease);
        transform.DOMoveY(transform.position.y - 200, timing).SetEase(ease);

        var kevinTransform = GameManager.Instance.Kevin.transform;
        kevinTransform.DOMoveY(down ? 0 : 2.5f, timing).SetEase(ease);
    }

    private class Emotion
    {
        public int Value { get; set; }

        public Emotion(int v)
        {
            Value = v;
        }
    }
}