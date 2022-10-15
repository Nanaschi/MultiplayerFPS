using TMPro;
using UnityEngine;

public class UIController
{


    public void SelectActiveUI(params RectTransform[] rectTransform)
    {
        for (int i = 0; i < rectTransform.Length; i++)
        {
            rectTransform[i].gameObject.SetActive(i == 0);
        }
    }

    public void SetText(TextMeshProUGUI textMeshProUGUI, string targetText)
    {
        textMeshProUGUI.text = targetText;
    }
}
