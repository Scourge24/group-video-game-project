using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    private VisualElement Root;
    private Button NewGameButton;
    private Button QuitButton;

    private void Start()
    {
        Root = GetComponent<UIDocument>().rootVisualElement;
        NewGameButton = Root.Q<Button>(nameof(NewGameButton));
        QuitButton = Root.Q<Button>(nameof(QuitButton));

        NewGameButton.clicked += NewGameButton_clicked;
        QuitButton.clicked += QuitButton_clicked;
    }

    private void NewGameButton_clicked()
    {
        LoadingScreenController.LoadSceneWithLoadingScreen("SampleScene");
    }

    private void QuitButton_clicked()
    {
        Application.Quit();
    }
}
