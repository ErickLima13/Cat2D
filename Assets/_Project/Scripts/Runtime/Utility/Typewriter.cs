using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Typewriter : MonoBehaviour
{
    public float delay;
    public TextMeshProUGUI text;

    public string phrase;
    public string[] phrases;

    public int idPhrase;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(TypewriterMethod(phrase));
        StartCoroutine(TypewriterMethodWithArrays());
    }


    private IEnumerator TypewriterMethod(string txt)
    {

        for(int i = 0; i < txt.Length; i++)
        {
            print(txt[i]);
            yield return new WaitForSeconds(delay);
        }
    }

    private IEnumerator TypewriterMethodWithArrays()
    {
        for(int idF = 0; idF < phrases.Length; idF++)
        {
            text.text = "";

            for (int letter = 0; letter < phrases[idF].Length; letter++)
            {
                //print(phrases[idF][letter]);
                text.text += phrases[idF][letter];
                yield return new WaitForSeconds(delay);
            }

            yield return new WaitForSeconds(1);
        }

        

    }

}
