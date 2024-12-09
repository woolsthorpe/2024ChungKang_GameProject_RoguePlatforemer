using JY.PlatformerBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    private SpriteRenderer spriteRender;
    private CharacterMovement movement;
    private CharacterAnimator animator;
    [field: SerializeField] public int maxHP { get; set; }
    [field: SerializeField] public int currentHP { get; private set; }
    [field: SerializeField] public bool isDie { get; private set; }

    [Header("HP Change option")]
    [SerializeField] private bool isInvincible;
    [SerializeField] private float invincibilityDuration;
    [SerializeField] private float colorSwapTime;
    [SerializeField] private Color hitColor;
    public bool isDamaged { get; private set; }

    [Header("Knockback")]
    [SerializeField] private float knockAmount;
    [SerializeField] private float knockTime;
    [Header("Camera Shake")]
    [SerializeField] private Vector2 shakeIntencity;
    [SerializeField] private float shakeVolume;
    [SerializeField] private float shakeDuration;



    void Start()
    {
        spriteRender = GetComponentInChildren<SpriteRenderer>();
        movement = GetComponent<CharacterMovement>();
        animator = GetComponent<CharacterAnimator>();

        currentHP = maxHP;
        isDamaged = false;
        isDie = false;
        isInvincible = false;
        UIManager.instance.ChangeHeartIcon(currentHP, maxHP);
    }

    public void GetDamaged(float damage,Transform hitPos)
    {
        try{
        if (isInvincible||isDie)
            return;

        currentHP -= (int)damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UIManager.instance.ChangeHeartIcon(currentHP, maxHP);

        if (currentHP<=0)
        {
            isDie = true;
            animator.CharatcerDeath();
            StartCoroutine(ColorSwap());
        }
        else
        {
            StartCoroutine(ColorSwap());
            animator.CharacterDamaged();
           movement.Damaged_knockback(transform.position- hitPos.position,knockAmount);
        }
        }catch{
            
        }
      
    }

    
    IEnumerator ColorSwap()
    {
        float currentTime = 0;
        Color orginColor = spriteRender.color;
        isInvincible = true;

        while(currentTime<invincibilityDuration)
        {
            spriteRender.color = hitColor;
            yield return new WaitForSeconds(colorSwapTime);
            spriteRender.color = orginColor;
            yield return new WaitForSeconds(colorSwapTime);
            currentTime += Time.deltaTime;
        }

        spriteRender.color = orginColor;
        isInvincible = false;

    }

}
