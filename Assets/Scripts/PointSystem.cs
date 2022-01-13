using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointSystem : MonoBehaviour
{
    [SerializeField] private int maxPointsAvailable = 10;
    [SerializeField] private KevinController kevin;
    [SerializeField] private TextMeshProUGUI pointsAvailableText;
    [SerializeField] private Button doneButton;
    private int PointsAvailable { get; set; }
    private Emotion[] _emotions;

    private List<TextMeshProUGUI> _valueList;

    private void OnEnable()
    {
        _emotions = new Emotion[4];
        _emotions[0] = new Emotion(0, "Happiness");
        _emotions[1] = new Emotion(0, "Sadness");
        _emotions[2] = new Emotion(0, "Fear");
        _emotions[3] = new Emotion(0, "Anger");

        PointsAvailable = maxPointsAvailable;
        _valueList = GetComponentsInChildren<TextMeshProUGUI>().ToList()
            .FindAll(ugui => ugui.CompareTag("PointSystem.Value"));
        
        var buttons = GetComponentsInChildren<Button>().ToList()
            .FindAll(button => button.CompareTag("PointSystem.Minus") || button.CompareTag("PointSystem.Plus"));
        foreach (var button in buttons)
        {
            button.onClick.AddListener(() => UpdateValue(button.name, button.CompareTag("PointSystem.Plus")));
        }

        doneButton.onClick.AddListener(() =>
        {
            Toggle(true);
            GenerateEgg();
        });
        
        Toggle(false);
    }

    private void GenerateEgg()
    {
        var temp = new Emotion(-1, "");
        foreach (var emotion in _emotions)
        {
            if (emotion.Value > temp.Value)
                temp = emotion;
        }
        GameManager.Instance.GenerateEgg(temp.Name);
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
        pointsAvailableText.text = PointsAvailable.ToString();
    }
    
    private async void Toggle(bool down)
    {
        const Ease ease = UISettings.PointSystemToggleTransitionEase;
        const float timing = UISettings.PointSystemToggleTransitionTime;
        
        var canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.DOFade(down ? 0 : 1, timing)
            .SetEase(ease);
        var tween = transform.DOMoveY(transform.position.y - (down ? 200 : -200), timing).SetEase(ease);
        kevin.TogglePosition(down, timing, ease);
        
        await tween.AsyncWaitForCompletion();
        GameManager.Instance.SetAttributingState(!down);
        gameObject.SetActive(!down);
    }

    private class Emotion
    {
        public int Value { get; set; }
        public string Name { get; }

        public Emotion(int v, string n)
        {
            Value = v;
            Name = n;
        }
    }
}