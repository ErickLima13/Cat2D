using UnityEngine;

public class LoadGame : MonoBehaviour
{
    private AudioController _audioController;
    private FadeTransition _fadeTransition;

    private bool _verify;

    // Start is called before the first frame update
    void Start()
    {
        _audioController = FindAnyObjectByType<AudioController>();
        _fadeTransition = FindAnyObjectByType<FadeTransition>();

        _audioController._audioSourceMusic.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_verify && !_audioController._audioSourceMusic.isPlaying)
        {
            _verify = true;
            StartCoroutine(_audioController.ChangeMusic(_audioController.grass));
            _fadeTransition.StarFade(3);
            _audioController._audioSourceMusic.loop = true;
        }
    }
}
