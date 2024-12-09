using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JY.PlatformerBase
{
    public class CharacterMovementDataController : MonoBehaviour
    {
        [SerializeField] private CharacterMovementData data;

        private CharacterMovement movement;
        private CharacterJump jump;

        private CharacterMovementData beforeData;


        private void Awake()
        {
            movement = GetComponent<CharacterMovement>();
            jump = GetComponent<CharacterJump>();

            InstallPresetData();

            if (data != beforeData)
                InstallPresetData();
        }

        private void InstallPresetData()
        {
            movement.maxSpeed = data.maxSpeed;
            movement.maxAcceleration = data.maxAcceleration;
            movement.maxDecceleration = data.maxDecceleration;

            movement.maxAirAcceleration = data.maxAirAcceleration;
            movement.maxAirDecceleration = data.maxAirDecceleration;

            movement.maxTurnSpeed =  data.maxTurnSpeed;
            movement.maxAirTurnSpeed = data.maxAirTurnSpeed;



            jump.jumpHeight = data.jumpHeight;
            jump.timeToJumpApex = data.timeToJumpApex;

            jump.jumpUpGravityScale = data.jumpUpGravityScale;
            jump.jumpFallGravityScale = data.jumpFallGravityScale;

            jump.variableJumpHeight = data.variableJumpHeight;
            jump.jumpCutOff = data.jumpCutOff;

            jump.airJumpCount = data.airJumpCount;

            
        }
    }
}
