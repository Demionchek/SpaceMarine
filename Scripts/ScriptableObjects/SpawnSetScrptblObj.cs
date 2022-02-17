using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnSetScrptblObj", menuName = "ScriptableObjects/SpawnSetScriptableObject", order = 1)]
public class SpawnSetScrptblObj : ScriptableObject
{
    [Header("Use for debug & testing")]
    [Tooltip("1-19 , 0 - default")]
    [SerializeField] private int _setRound;
    public int SetRound
    {
        get
        {
            return _setRound;
        }
    }
}
