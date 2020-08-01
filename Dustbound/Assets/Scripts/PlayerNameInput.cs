using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerNameInput : MonoBehaviour {
    [Header("UI")]
    [SerializeField] private TMP_InputField nameInputField = null;
    [SerializeField] private Button continueButton = null;

    public static string DisplayName { get; private set; }
    private const string playerPrefsNameKey = "PlayerName";

    private void Start() => SetUpInputField();

    private void SetUpInputField() { // check if previously used name exists
        if (!PlayerPrefs.HasKey(playerPrefsNameKey)) { return; }

        // use previously used name
        string defaultName = PlayerPrefs.GetString(playerPrefsNameKey);
        nameInputField.text = defaultName;
        SetPlayerName(defaultName);
	}

    public void SetPlayerName(string name) {
        // if player hasn't typed a name, button cannot be pressed
        continueButton.interactable = !string.IsNullOrEmpty(nameInputField.text);
	}

    public void SavePlayerName() {
        DisplayName = nameInputField.text;
        PlayerPrefs.SetString(playerPrefsNameKey, DisplayName);
	}
}