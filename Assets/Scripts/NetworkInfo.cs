﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkInfo : NetworkBehaviour {

    [SyncVar]
    public int PlayersConnected;
    [SyncVar]
    public int PlayersReady;
    [SyncVar]
    public bool AllPlayersReady;

    [SyncVar]
    public bool Classe1;
    [SyncVar]
    public bool Classe2;
    [SyncVar]
    public bool Classe3;
    [SyncVar]
    public bool Classe4;

    public bool[] ClassesInUse;

    [SyncVar]
    public int BossNumber;

    [SyncVar]
    public int PlayersDead;
    [SyncVar]
    public bool BossDied;

    public bool LocalReady;

    public static NetworkInfo NI;

    [SerializeField]
    GameObject DeadCanvas;

    private void Awake()
    {
        if (NI == null)
        {
            NI = this;
        } else if (NI != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ClassesInUse = new bool[4];

        Classe1 = true;
        Classe2 = true;
        Classe3 = true;
        Classe4 = true;
    }

    [Command]
    public void CmdBecomeReady()
    {
        CheckForReady();
        BossNumber = Random.Range(0, 3);
        //PlayersReady++;
    }

    [Command]
    public void CmdBecomeUnReady()
    {
        PlayersReady--;
    }
    [Command]
    public void CmdPlayerDied()
    {
        PlayersDead++;
    }
    [Command]
    public void CmdBossDied()
    {
        BossDied = true;
    }

    [Command]
    public void CmdSelectSlot(int I)
    {
        BossNumber = Random.Range(0, 3);
        //CheckForReady();
        if (I == 0)
        {
            Classe1 = false;
        }
        else if (I == 1)
        {
            Classe2 = false;
        }
        else if (I == 2)
        {
            Classe3 = false;
        }
        else if (I == 3)
        {
            Classe4 = false;
        }
    }

    [Command]
    public void CmdSelectSlot1()
    {
        BossNumber = Random.Range(0, 3);
        //CheckForReady();
        Classe1 = false;
    }
    [Command]
    public void CmdSelectSlot2()
    {
        BossNumber = Random.Range(0, 3);
        //CheckForReady();
        Classe2 = false;
    }
    [Command]
    public void CmdSelectSlot3()
    {
        BossNumber = Random.Range(0, 3);
        
        Classe3 = false;
    }
    [Command]
    public void CmdSelectSlot4()
    {
        BossNumber = Random.Range(0, 3);
        
        Classe4 = false;
    }

    public void CmdToggleReady()
    {
        if (LocalReady)
        {
            PlayersReady--;
        } else
        {
            PlayersReady++;
        }
    }
    [Server]
    private void CheckForReady()
    {
        BossNumber = Random.Range(0, 3);
        GameObject[] PlayerObjects;
        PlayerObjects = GameObject.FindGameObjectsWithTag("Player");

        PlayersConnected = PlayerObjects.Length;
        PlayersReady = 0;
        for (int i = 0;i < PlayerObjects.Length;i++)
        {
            if (PlayerObjects[i].GetComponent<Character>().CharacterStatus == 1)
            {
                PlayersReady++;
            }
        }

        
    }


    private void Update()
    {
        ClassesInUse[0] = Classe1;
        ClassesInUse[1] = Classe2;
        ClassesInUse[2] = Classe3;
        ClassesInUse[3] = Classe4;
        if (isServer)
        {
            if (PlayersConnected > 3)
            {
                if (PlayersReady == PlayersConnected)
                {
                    AllPlayersReady = true;
                }
                else
                {
                    AllPlayersReady = false;
                }
            }
        }
        if (BossDied)
        {
            DeadCanvas.SetActive(true);
        }
    }

    public void CmdConnected()
    {
        Debug.Log("Player Connected");
        
        PlayersConnected++;
    }


    [Server]
    private void OnPlayerConnected(NetworkPlayer player)
    {
        Debug.Log("Player Connected");
        PlayersConnected++;
    }
    [Server]
    private void OnPlayerDisconnected(NetworkPlayer player)
    {
        CmdBecomeUnReady();
        PlayersConnected--;
    }
}
