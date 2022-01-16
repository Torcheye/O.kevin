using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int scene;
    public PointSystem pointSystem;
    public Inventory inventory;

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
        StartCoroutine(Test());
    }

    public void SetAttributingState(bool attributing)
    {
        kevin.GetComponent<KevinController>().SetAttributingState(attributing);
        pointSystem.gameObject.SetActive(attributing);
    }

    public void GenerateEgg(string n)
    {
        inventory.AddEgg(new Egg(n, 1));
    }

    private IEnumerator Test()
    {
        yield return new WaitForSeconds(4);
        SetAttributingState(true);
    }
}