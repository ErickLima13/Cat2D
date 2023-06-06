using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsUI;

    private EconomyController economy;

    private void OnEnable()
    {
        economy.OnUpdateCoins += UpdateCoins;
    }

    private void OnDisable()
    {
        economy.OnUpdateCoins -= UpdateCoins;
    }

    private void Awake()
    {
        economy = FindObjectOfType<EconomyController>();

        UpdateCoins(economy.GetCoins());
    }

    public void UpdateCoins(int value)
    {
        print(value);

        value = economy.GetCoins();
        coinsUI.text = " x " + value.ToString();
    }
}
