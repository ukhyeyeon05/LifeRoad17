using UnityEngine;
using UnityEngine.UI;

public class IntroUIManager : MonoBehaviour
{
    public Button buttonNewGame;
    public Button buttonContinue;
    public Button buttonOptions;
    public GameObject panelOptions;

    void Start()
    {
        buttonNewGame.onClick.AddListener(OnNewGameClicked);
        buttonContinue.onClick.AddListener(OnContinueClicked);
        buttonOptions.onClick.AddListener(ToggleOptionsPanel);

        buttonContinue.interactable = PlayerPrefs.HasKey("SavedPuzzle");
        panelOptions.SetActive(false);
    }

    public void OnNewGameClicked()
    {
        GameManager.Instance.NewGame();
    }

    public void OnContinueClicked()
    {
        GameManager.Instance.LoadGame();
    }

    public void ToggleOptionsPanel()  
    {
        panelOptions.SetActive(!panelOptions.activeSelf);
    }
}
