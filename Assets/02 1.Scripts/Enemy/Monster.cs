using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    protected bool sleep = true;
    protected bool Stop = false;
    protected float sleeprange = 10f;
    protected GameObject Player;
    public float HP = 1;
    protected float ATKDMG = 1;
    protected void Awake() {
        Player = GameObject.Find("Player Character");
    }

    public abstract void OnHit(float DMG);
}
