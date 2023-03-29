using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public SaveManager saveManager;
    public TMP_InputField nameField;
    public TMP_InputField idField;

    public void StartGame()
    {
        if(nameField.text.Length < 1 || idField.text.Length < 1) return;
        saveManager.intializeLog(nameField.text, idField.text);
        SaveManager.SavePlayer(nameField.text, int.Parse(idField.text));
        SceneManager.LoadScene("Game");
    }
}