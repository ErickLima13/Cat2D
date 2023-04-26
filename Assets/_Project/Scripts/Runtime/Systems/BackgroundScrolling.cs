using UnityEngine;


public enum Choices
{
    Player,
    Menu
}

public class BackgroundScrolling : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    [SerializeField] private GameObject player;

    [SerializeField] private float speed = 0.02f;

    [SerializeField] private Choices choice;

    private void Start()
    {
        meshRenderer= GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        InfiniteBackground();
    }

    private void InfiniteBackground()
    {
        switch (choice)
        {
            //case Choices.Player:
            //    meshRenderer.material.mainTextureOffset += new Vector2(player.GetComponent<Player>().direction * speed * Time.deltaTime, 0);
            //    break;
            case Choices.Menu:
                meshRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
                break;
        }
    }
}
