using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;

    [SerializeField] private float speed;

    private int idWaypoint;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[0].position;
        idWaypoint++;
    }

    // Update is called once per frame
    void Update()
    {
        MobilePlatform();
    }

    private void MobilePlatform()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[idWaypoint].position, speed * Time.deltaTime);

        if (transform.position == waypoints[idWaypoint].position)
        {
            idWaypoint++;

            if (idWaypoint == waypoints.Length)
            {
                idWaypoint = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            if (player.groundCheckR.position.y > transform.position.y)
            {
                player.transform.parent.parent = transform;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            if (player.groundCheckR.position.y > transform.position.y)
            {
                player.transform.parent.parent = null;
            }
        }
    }
}
