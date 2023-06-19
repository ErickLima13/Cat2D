using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;

public class LoadXmlFile : MonoBehaviour
{
    public string arquivoXml;

    public List<string> interface_titulo = new();
    public List<string> interface_loja = new();

    public string idioma;

    public TextMeshProUGUI newText;

    private void Awake()
    {
        LoadXmlData();
    }

    private void LoadXmlData()
    {
        TextAsset xmlData = (TextAsset)Resources.Load(idioma + "/" + arquivoXml);
        XmlDocument xmlDocument = new();

        xmlDocument.LoadXml(xmlData.text);

        foreach (XmlNode node in xmlDocument["language"].ChildNodes)
        {
            string nodeName = node.Attributes["name"].Value;

            foreach (XmlNode n in node["textos"].ChildNodes)
            {
                switch (nodeName)
                {
                    case "interface_titulo":
                        interface_titulo.Add(n.InnerText);

                        newText.text = n.InnerText;

                    break;
                    case "interface_loja":
                        interface_loja.Add(n.InnerText);
                        break;
                }
            }
        }

    }
}
