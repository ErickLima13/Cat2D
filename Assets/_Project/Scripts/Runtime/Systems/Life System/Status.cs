using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour,ILifeController
{
    private GameObject hitPrefab;

    public int maxLife;

    private void Start()
    {
        hitPrefab = Resources.Load<GameObject>("CollisionHit");
    }

    public void HealthChange(int value)
    {
        GameObject temp = Instantiate(hitPrefab,transform.position,Quaternion.identity);

        Destroy(temp,0.5f);

        maxLife -= value;

        if (maxLife <= 0)
        {
            Destroy(gameObject);
        }
    }
}
