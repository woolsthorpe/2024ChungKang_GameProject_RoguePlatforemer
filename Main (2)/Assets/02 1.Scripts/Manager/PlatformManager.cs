using JY.PlatformerBase;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] private Transform PlayerPos;
    [SerializeField] private GroundPlatform[] platformList;
    void Start()
    {
        if(PlayerPos ==null)
        {
            PlayerPos = GameObject.FindWithTag("Player").transform;
        }

        platformList = GetComponentsInChildren<GroundPlatform>();
        for(int i=0;i<platformList.Length;i++)
        {
            platformList[i].SetPlayerPos(PlayerPos);
        }
    }

   
}
