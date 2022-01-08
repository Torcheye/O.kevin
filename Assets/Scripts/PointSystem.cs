using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointSystem : MonoBehaviour
{
    [SerializeField] private const int MaxPointsAvailable = 10;
    private int Happiness { get; set; }
    private int Sadness { get; set; }
    private int Fear { get; set; }
    private int Anger { get; set; }
    private int PointsAvailable { get; set; }

    private List<TextMeshProUGUI> _valueList;
    private TextMeshProUGUI _pointsAvailabelText;

    private void Start()
    {
        PointsAvailable = MaxPointsAvailable;
        _pointsAvailabelText = GameObject.Find("PointsAvailable").GetComponent<TextMeshProUGUI>();
        _valueList = GetComponentsInChildren<TextMeshProUGUI>().ToList()
            .FindAll(ugui => ugui.CompareTag("PointSystem.Value"));

        var buttons = GetComponentsInChildren<Button>().ToList()
            .FindAll(button => button.CompareTag("PointSystem.Minus") || button.CompareTag("PointSystem.Plus"));
        foreach (var button in buttons)
        {
            button.onClick.AddListener(() => UpdateValue(button.name, button.CompareTag("PointSystem.Plus")));
        }
    }

    private void UpdateValue(string valueName, bool plus)
    {
        if ((PointsAvailable == MaxPointsAvailable && !plus) || (PointsAvailable == 0 && plus)) return;
        
        var text = _valueList.Find(ugui => ugui.name == valueName);
        var increment = plus ? 1 : -1;

        switch (valueName)
        {
            case "Happiness":
                Happiness += increment;
                text.text = Happiness.ToString();
                break;
            case "Sadness":
                Sadness += increment;
                text.text = Sadness.ToString();
                break;
            case "Fear":
                Fear += increment;
                text.text = Fear.ToString();
                break;
            case "Anger":
                Anger += increment;
                text.text = Anger.ToString();
                break;
        }

        PointsAvailable -= increment;
        _pointsAvailabelText.text = PointsAvailable.ToString();
    }
}