using UnityEngine;

public class RotatePlatform : MonoBehaviour
{
    [SerializeField] private Transform circle;
  
    [SerializeField] private float speed;


    // Update is called once per frame
    void Update()
    {
        PlatformRevolving();
    }

    private void PlatformRevolving()
    {
        float newSpeed = speed * Time.deltaTime;

        circle.Rotate(Vector3.forward * newSpeed);

        transform.Rotate(-Vector3.forward * newSpeed);
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
