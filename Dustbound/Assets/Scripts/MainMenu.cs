using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    string playerName;
    [SerializeField] public TMPro.TMP_InputField inputField;

    public void UpdateName()
    {
        playerName = inputField.text;
        Debug.Log(playerName);
    }

    public void OnPlayPressed()
    {
        SceneManager.LoadScene("TestScene");
    }
}
