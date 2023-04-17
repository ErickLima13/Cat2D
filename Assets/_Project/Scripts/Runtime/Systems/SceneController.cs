using UnityEngine;

public class SceneController : MonoBehaviour
{
    private AudioController _audioController;

    private FadeTransition _fadeTransition;

    // Start is called before the first frame update
    void Start()
    {
        _fadeTransition = FindAnyObjectByType<FadeTransition>();
        _audioController = FindAnyObjectByType<AudioController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(_audioController.ChangeMusic(_audioController.start));
            _fadeTransition.StarFade(2);
        }
    }
}
