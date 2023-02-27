using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour {
    public SaveManager saveManager;
    public TextMeshProUGUI nameField;
    public TextMeshProUGUI idField;

    public void StartGame() {
        if (nameField.text.Length < 1 || idField.text.Length < 1) return;
        saveManager.intializeLog(nameField.text, idField.text);
        SceneManager.LoadScene("Game");
    }
}