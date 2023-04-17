using UnityEngine;

public class GameManager : MonoBehaviour
{
    private FadeTransition _fadeTransition;

    // Start is called before the first frame update
    void Start()
    {
        _fadeTransition = FindAnyObjectByType<FadeTransition>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
