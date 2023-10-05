using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class textdisplay : MonoBehaviour
{
    public List<string> textLines = new List<string>();
    public TMP_Text textDisplayUI;
    public float displayDuration = 3.0f; // Time to display each line
    public float noTextDuration = 2.0f; // Time with no text between lines
    private int currentIndex = 0;

    private void Start()
    {
        // Start displaying text lines after a delay (if needed)
        StartCoroutine(DisplayTextLines());
    }

    private IEnumerator DisplayTextLines()
    {
        while (currentIndex < textLines.Count)
        {
            textDisplayUI.text = textLines[currentIndex];
            currentIndex++;

            yield return new WaitForSeconds(displayDuration);

            if (currentIndex < textLines.Count)
            {
                textDisplayUI.text = ""; // No text to display
                yield return new WaitForSeconds(noTextDuration);
            }
        }
    }
}
