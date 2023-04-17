using UnityEngine;

public class GameManager : MonoBehaviour
{
    private FadeTransition _fadeTransition;

    // Start is called before the first frame update
    void Start()
    {
        _fadeTransition = FindObjectOfType<FadeTransition>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _fadeTransition.StarFade(1);
        }
    }
}
