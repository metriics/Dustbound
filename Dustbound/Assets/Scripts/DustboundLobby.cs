﻿using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class DustboundLobby : NetworkManager
{
    [Scene] [SerializeField] private string menuScene = string.Empty;

    [Header("Room")]
    [SerializeField] private LobbyPlayer LobbyPlayerPrefab = null;

    public static event Action OnClientConnected;
    public static event Action OnClientDisconnected;

    public override void OnStartServer() => spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();

    public override void OnStartClient() {
        var spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs");

        foreach (var prefab in spawnablePrefabs) {
            ClientScene.RegisterPrefab(prefab);
		}
	}

	public override void OnClientConnect(NetworkConnection conn) {
		base.OnClientConnect(conn);

        OnClientConnected?.Invoke();
	}

	public override void OnClientDisconnect(NetworkConnection conn) {
		base.OnClientDisconnect(conn);

        OnClientDisconnected?.Invoke();
	}

	public override void OnServerConnect(NetworkConnection conn) {
		if (numPlayers >= maxConnections) {
			conn.Disconnect();
			return;
		}

		if (SceneManager.GetActiveScene().name != menuScene) { // not in lobby
			conn.Disconnect();
			return;
		}
	}

	public override void OnServerAddPlayer(NetworkConnection conn) {
		if (SceneManager.GetActiveScene().name == menuScene) {
			LobbyPlayer playerInstance = Instantiate(LobbyPlayerPrefab);
			NetworkServer.AddPlayerForConnection(conn, playerInstance.gameObject);
		}
	}
}
