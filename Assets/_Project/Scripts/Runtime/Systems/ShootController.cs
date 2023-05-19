using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public int damageBall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Status status))
        {
            Destroy(gameObject, 0.2f);
            status.HealthChange(damageBall);
        }
    }
}
