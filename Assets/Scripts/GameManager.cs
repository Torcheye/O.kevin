using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject kevin;
    public PointSystem pointSystem;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        kevin = GameObject.Find("Kevin");

        kevin.GetComponent<KevinAnimator>().BootUp();
        StartCoroutine(test());
    }

    public void SetAttributingState(bool attributing)
    {
        kevin.GetComponent<KevinAnimator>().SetAttributingState(attributing);
        pointSystem.gameObject.SetActive(attributing);
    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(4);
        SetAttributingState(true);
    }
}