using UnityEngine;
using System.Collections;

public class GameMenuController : MonoBehaviour {

    public GameObject menuPanel;
    public GameObject operationPanel;
    public GameObject difficultyLevelPanel;
    public GameObject characterSelectPanel;
    public Color opertaionPanelColor;
    
    
    public void OnMenuPlay() {
        menuPanel.SetActive(false);
        operationPanel.SetActive(true);
    }
    public void OnOperationPlay() {
        operationPanel.GetComponent<UISprite>().color = opertaionPanelColor;
        difficultyLevelPanel.SetActive(true);
    }
    public void OnDifficultyLevelClick() {
        operationPanel.SetActive(false);
        difficultyLevelPanel.SetActive(false);
        characterSelectPanel.SetActive(true);

    }
    public void OnCharacterSelect() {
    }
    void update() {
        float xFactor = Screen.width / 720f;
        float yFactor = Screen.height / 1280f;

        Camera.main.rect = new Rect(0, 0, 1, xFactor / yFactor);
        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }
    }
        
}
