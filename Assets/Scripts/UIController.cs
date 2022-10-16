using TMPro;
using UnityEngine;

public class UIController
{
    private LobbyMenuView _lobbyMenuView;

    public UIController(LobbyMenuView lobbyMenuView)
    {
        _lobbyMenuView = lobbyMenuView;
        Debug.Log(_lobbyMenuView.gameObject);
    }
    public void SelectActiveUI(params RectTransform[] rectTransform)
    {
        for (int i = 0; i < rectTransform.Length; i++)
        {
            rectTransform[i].gameObject.SetActive(i == 0);
        }
    }
    
    public void SelectActiveUI(RectTransform rectTransform, RectTransform[] rectTransforms)
    {
        for (int i = 0; i < rectTransforms.Length; i++)
        {
            rectTransforms[i].gameObject.SetActive(rectTransform == rectTransforms[i]);
        }
    }

    public void SetText(TextMeshProUGUI textMeshProUGUI, string targetText)
    {
        textMeshProUGUI.text = targetText;
    }
}
