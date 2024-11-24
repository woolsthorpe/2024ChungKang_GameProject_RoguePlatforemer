using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Gun Data")]
public class PlayerWeapon_GunData : ScriptableObject
{
    public float shootSpeed;
    public float fireMaxDelay;
    public int bulletCount;
}
