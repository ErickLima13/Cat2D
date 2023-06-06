using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private int value;

    private EconomyController economy;

    private void Awake()
    {
        economy = FindObjectOfType<EconomyController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            AddCoins(value);
        }
    }

    private void AddCoins(int value)
    {
        economy.AddCoins(value);
        Destroy(gameObject, 0.2f);
    }
}
