using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JoinLobbyMenu : MonoBehaviour
{
    [SerializeField] private DustboundLobby networkManager = null;

    [Header("UI")]
    [SerializeField] private GameObject landingPagePanel = null;
    [SerializeField] private TMP_InputField ipInputField = null;
    [SerializeField] private Button joinButton = null;

	private void OnEnable() {
        DustboundLobby.OnClientConnected += HandleClientConnected;
        DustboundLobby.OnClientDisconnected += HandleClientDisconnected;
	}

	private void OnDisable() {
        DustboundLobby.OnClientConnected -= HandleClientConnected;
        DustboundLobby.OnClientDisconnected -= HandleClientDisconnected;
	}

    public void JoinLobby() {
        string ipAddress = ipInputField.text;

        networkManager.networkAddress = ipAddress;
        networkManager.StartClient();

        joinButton.interactable = false;
	}

    private void HandleClientConnected() {
        joinButton.interactable = true;

        gameObject.SetActive(false);
        landingPagePanel.SetActive(false);
	}

    private void HandleClientDisconnected() {
        joinButton.interactable = true;
	}
}
