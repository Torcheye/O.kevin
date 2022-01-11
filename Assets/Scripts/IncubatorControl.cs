using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IncubatorControl : MonoBehaviour
{
    [SerializeField] private GameObject incubate, skip;
    [SerializeField] private TextMeshProUGUI timer;

    private int _hour, _min;

    private void Start()
    {
        ChangeIncubationState(false);
        
        incubate.GetComponent<Button>().onClick.AddListener(OnIncubate);
        skip.GetComponent<Button>().onClick.AddListener(OnSkip);
    }

    private void OnSkip()
    {
        ChangeIncubationState(false);
    }

    private void OnIncubate()
    {
        ChangeIncubationState(true);
        StartTimer(0, 10);
    }

    private void ChangeIncubationState(bool start)
    {
        skip.SetActive(start);
        incubate.SetActive(!start);
    }

    private void StartTimer(int hour, int min)
    {
        _hour = hour;
        _min = min;
        StartCoroutine(Countdown());
    }

    private void StopTimer()
    {
        StopCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        while (true)
        {
            timer.text = _hour.ToString("00") + ":" + _min.ToString("00");
            yield return new WaitForSecondsRealtime(1);
            
            if (_min > 0)
            {
                _min--;
            }
            else
            {
                if (_hour > 0)
                {
                    _hour--;
                    _min = 59;
                }
                else
                {
                    StopTimer();
                }
            }
        }
    }
}

//hehe:D
