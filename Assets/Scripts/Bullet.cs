﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;



public class Bullet : NetworkBehaviour {
    [SyncVar]
    public int Damage;
    [SyncVar]
    public bool Heal;
    [SyncVar]
    public int Team;
    public GameObject Shooter;

    private void Start()
    {
        Destroy(gameObject, 2);
        Debug.Log("Bullet Spawned!");
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != Shooter)
        {
            if (other.gameObject.tag == "Player")
            {
                if (isServer)
                {
                    if (other.gameObject.GetComponent<Character>().Team != Team)
                    {
                        Character CHA = other.gameObject.GetComponent<Character>();
                        CHA.TakeDamage(Damage);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}

