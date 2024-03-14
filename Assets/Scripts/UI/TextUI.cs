using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour
{
    public Text flashingText;

    void Start()
    {
        flashingText = GetComponent<Text>();

        StartCoroutine(BlinkText());
    }

    public IEnumerator BlinkText()
    {
        while (true)
        {
            flashingText.text = "Press Any Key To Start!";
            yield return new WaitForSeconds(1f);
            flashingText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}