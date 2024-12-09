using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [field: SerializeField] public int maxHP { get; set; }
    [field: SerializeField] public int currentHP { get; private set; }
    [field: SerializeField] public int isDie { get; private set; }

    [Header("HP Change option")]
    [SerializeField] private float invincibilityDuration;
    //피격,치유시 무적시간
    public bool isDamaged { get; private set; }
    [Header("Camera Shake")]
    [SerializeField] private Vector2 shakeIntencity;
    [SerializeField] private float shakeVolume;
    [SerializeField] private float shakeDuration;



    void Start()
    {
        currentHP = maxHP;
        isDamaged = false;
    }

    void GetDamaged(float damage,Transform hitPos)
    {
        
    }

    
    //컬러 스왑
    //카메라 쉐이킹
    //넉백 설정
}
