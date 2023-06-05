using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsUI;

    private PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        UpdateCoins();
    }

    private void UpdateCoins()
    {
        coinsUI.text = " x " + player.coins.ToString();
    }

}
