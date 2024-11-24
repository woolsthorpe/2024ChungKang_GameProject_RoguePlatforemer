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
    //�ǰ�,ġ���� �����ð�
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

    
    //�÷� ����
    //ī�޶� ����ŷ
    //�˹� ����
}
