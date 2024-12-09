using JY.PlatformerBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataController : MonoBehaviour
{
    [SerializeField] private CharacterMovementData moveData;
    [SerializeField] private PlayerWeapon_GunData weaponData;
    

    private CharacterMovement movement;
    private CharacterJump jump;
    private CharacterMovementData beforeData;
    private PlayerWeaponGun gun;

    private void Awake()
    {
        movement = GetComponentInChildren<CharacterMovement>();
        jump = GetComponentInChildren<CharacterJump>();
        gun = GetComponentInChildren<PlayerWeaponGun>();

        InstallPresetData();

        if (moveData != beforeData)
            InstallPresetData();
    }

    private void InstallPresetData()
    {
        movement.maxSpeed = moveData.maxSpeed;
        movement.maxAcceleration = moveData.maxAcceleration;
        movement.maxDecceleration = moveData.maxDecceleration;

        movement.maxAirAcceleration = moveData.maxAirAcceleration;
        movement.maxAirDecceleration = moveData.maxAirDecceleration;

        movement.maxTurnSpeed = moveData.maxTurnSpeed;
        movement.maxAirTurnSpeed = moveData.maxAirTurnSpeed;



        jump.jumpHeight = moveData.jumpHeight;
        jump.timeToJumpApex = moveData.timeToJumpApex;

        jump.jumpUpGravityScale = moveData.jumpUpGravityScale;
        jump.jumpFallGravityScale = moveData.jumpFallGravityScale;

        jump.variableJumpHeight = moveData.variableJumpHeight;
        jump.jumpCutOff = moveData.jumpCutOff;

        jump.airJumpCount = moveData.airJumpCount;

        gun.maxDelay = weaponData.fireMaxDelay;
        gun.bulletCount = weaponData.bulletCount;
        gun.shotSpeed = weaponData.shootSpeed;
    }
}
