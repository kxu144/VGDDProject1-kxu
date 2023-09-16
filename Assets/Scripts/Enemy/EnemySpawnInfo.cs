using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnInfo
{
    #region Editor Variables
    [SerializeField] private String m_name;
    public String EnemyName {
        get {
            return m_name;
        }
    }

    [SerializeField] private GameObject m_go;
    public GameObject EnemyGO {
        get {
            return m_go;
        }
    }

    [SerializeField] private float spawntime;
    public float TimeToNextSpawn {
        get {
            return spawntime;
        }
    }

    [SerializeField] private int spawnnum;
    public int NumToSpawn {
        get {
            return spawnnum;
        }
    }
    #endregion
}
