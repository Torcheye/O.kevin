using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int scene;
    public PointSystem pointSystem;

    [SerializeField] private GameObject kevin;
    
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
        kevin.GetComponent<KevinController>().BootUp();
        StartCoroutine(test());
    }

    public void SetAttributingState(bool attributing)
    {
        kevin.GetComponent<KevinController>().SetAttributingState(attributing);
        pointSystem.gameObject.SetActive(attributing);
    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(4);
        SetAttributingState(true);
    }
}