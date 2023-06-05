using UnityEngine;

public class IABasic : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Transform[] waypoints;

    [SerializeField] private Transform enemy;

    [SerializeField] private bool isLookLeft;

    [SerializeField] private float speed;

    private int idWaypoint;

    void Start()
    {
        spriteRenderer = enemy.gameObject.GetComponent<SpriteRenderer>();

        enemy.position = waypoints[0].position;
        idWaypoint++;
    }

    void Update()
    {
        if(enemy!= null)
        {
            MoveEnemy();
        }        
    }

    private void MoveEnemy()
    {
        enemy.position = Vector3.MoveTowards(enemy.position, waypoints[idWaypoint].position, speed * Time.deltaTime);

        if (enemy.position == waypoints[idWaypoint].position)
        {
            idWaypoint++;

            if (idWaypoint == waypoints.Length)
            {
                idWaypoint = 0;
            }
        }

        if (waypoints[idWaypoint].position.x < enemy.position.x && !isLookLeft)
        {
            Flip();
        }
        else if(waypoints[idWaypoint].position.x > enemy.position.x && isLookLeft)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isLookLeft = !isLookLeft;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

}
