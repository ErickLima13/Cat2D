using UnityEngine;

public class StoreController : MonoBehaviour
{
    public Sprite[] sprites;

    public string[] awards;

    public GameObject storePanel;

    private void OnEnable()
    {
        ItemData.OnOpenStore += OpenStore;
    }

    private void OnDisable()
    {
        ItemData.OnOpenStore -= OpenStore;
    }

    void Start()
    {

    }


    void Update()
    {
        print(awards[0]);
    }

    private void OpenStore()
    {
        Time.timeScale = 0;
        storePanel.SetActive(true);
    }

    public void CloseStore()
    {
        Time.timeScale = 1;
        storePanel.SetActive(false);
    }

    public void BuyItem(int name)
    {
        switch (name)
        {
            case 0:
                print(name); 
                break;
            case 1:
                print(name);
                break;
            case 2:
                print(name);
                break;
        }
    }
}
