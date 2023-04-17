using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeTransition : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private int idScene;

    public void StarFade(int index)
    {
        idScene = index;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComlete()
    {
        SceneManager.LoadScene(idScene);
    }
}
