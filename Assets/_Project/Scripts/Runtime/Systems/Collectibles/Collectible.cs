using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private int value;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            player.coins += value;
            Destroy(gameObject, 0.2f);
        }
    }
}
