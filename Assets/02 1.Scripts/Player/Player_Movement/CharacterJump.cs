using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace JY.PlatformerBase
{
    public class CharacterJump : MonoBehaviour
    {
        private Rigidbody2D rigid;
        private CharacterGround ground;
        private Vector2 currentVelocity;

       
        [Header("Jump Settings")]
        [Range(2f, 5.5f)] public float jumpHeight;
        [Range(0.2f, 2f)] public float timeToJumpApex;

        [Range(0f, 5f)] public float jumpUpGravityScale;
        [Range(1f, 10f)] public float jumpFallGravityScale;

        [Range(0f, 1f)] public float jumpHangGravityMult=1;
        public float jumpHangTimeThreshold=0;

        [Space(10)]
        [Header("Option")]
        public bool variableJumpHeight;
        [Range(1f, 10f)] public float jumpCutOff;
        [SerializeField] private float fallSpeedLimit=1000;

        [Range(0f,1f)]public float jumpBufferTime=0.1f;
        [Range(0f, 1f)] public float coyoteTime=0.1f;

        public int airJumpCount=0;
        [SerializeField]private int currentJumpCount;
        //공중 더블점프


        [Header("Calculations")]
        [SerializeField] private float jumpForce;
        [SerializeField] private float gravityMultiplier;
        private float gravityStrength;
        private float defaultGravityScale;

        [Header("Current State")]
        [SerializeField] private bool onGround;
        [SerializeField] private bool startJump;
        [SerializeField] private bool pressingJump;
        [SerializeField] private bool currentlyJumping;

        [SerializeField] private float currentBufferTime;
        [SerializeField] private float currentCoyoteTime;

        //default
        private float initialJumpForce;
        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
            ground = GetComponent<CharacterGround>();
            defaultGravityScale = 1;

            //jumpForce = Mathf.Sqrt(-2f * Physics2D.gravity.y * rigid.gravityScale * jumpHeight);

            // float gravityStrength = (-2 * jumpHeight) / (timeToJumpApex * timeToJumpApex);
            // defaultGravityScale = (gravityStrength / Physics2D.gravity.y);
            // jumpForce = Mathf.Sqrt(-2f * Physics2D.gravity.y * defaultGravityScale * jumpHeight);

        }


        private void InputJump()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                startJump = true;
                pressingJump = true;
            }
            else if(Input.GetKeyUp(KeyCode.Space))
            {
                pressingJump =false;
            }

        }

        private void Update()
        {
            //gravity 대입해주는 함수가 필요
            SetPhysics();

            InputJump();

            onGround = ground.GetOnGround();

            

            //점프 버퍼
            if (jumpBufferTime>0 && startJump)
            {
                currentBufferTime += Time.deltaTime;

                if (currentBufferTime > jumpBufferTime)
                {
                    startJump = false;
                    currentBufferTime = 0;
                }
            }

            //코요태 점프
            if (!currentlyJumping && !onGround)
                currentCoyoteTime += Time.deltaTime;
            else
                currentCoyoteTime = 0;

           
        }
        private void SetPhysics()
        {
            Vector2 newGravity =new Vector2(0, (-2 * jumpHeight) / (timeToJumpApex * timeToJumpApex));
            gravityStrength = (newGravity.y / Physics2D.gravity.y);
            rigid.gravityScale = gravityStrength * gravityMultiplier;
            Debug.Log($"{rigid.gravityScale} = {gravityStrength} * {gravityMultiplier}");

        }
      
        private void FixedUpdate()
        {
            currentVelocity = rigid.velocity;
            if (startJump && onGround)
            {
               
                DoAJump();
                rigid.velocity = currentVelocity;

                return;
            }

            CalculateGravity();


            if (onGround)
            {
                currentlyJumping = false;
                currentJumpCount = airJumpCount;
            }

               
        }

        private void DoAJump()
        {
            if( (currentCoyoteTime>=0&&currentCoyoteTime<=coyoteTime))
            {
                startJump = false;
                currentBufferTime = 0;
                currentCoyoteTime = 0;
                currentJumpCount = (currentJumpCount > 0) ? currentJumpCount - 1 : currentJumpCount;

                jumpForce = Mathf.Sqrt(-2f * Physics2D.gravity.y * gravityStrength * jumpHeight);
                //Debug.Log($"{jumpForce} = {Physics2D.gravity.y} * {rigid.gravityScale} * {jumpHeight}");
                //jumpForce = Mathf.Abs(gravityStrength) * timeToJumpApex;


                if (currentVelocity.y < 0f)
                {
                   jumpForce -= currentVelocity.y;
                }

                currentVelocity.y += jumpForce;
              //  rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                currentlyJumping = true;
                Debug.Log("점프");
            }
        }

        private void CalculateGravity()
        {

            //하강
            if (rigid.velocity.y < -0.01f)
            {
                if (onGround )
                    gravityMultiplier = defaultGravityScale;
                else
                    gravityMultiplier = jumpFallGravityScale;
            }
            //중간
            else if (currentlyJumping &&Mathf.Abs(rigid.velocity.y) < jumpHangTimeThreshold)
            {
                gravityMultiplier = defaultGravityScale * jumpHangGravityMult;
                Debug.Log("jump hang"+ defaultGravityScale * jumpHangGravityMult);
            }
            //상승
            else if (rigid.velocity.y > 0.01f)
            {
                if (onGround&&!currentlyJumping)
                    gravityMultiplier = defaultGravityScale;

                if (variableJumpHeight)
                {
                    if (pressingJump && currentlyJumping)
                    {
                       
                        gravityMultiplier = jumpUpGravityScale;
                    }
                    else
                    {
                        gravityMultiplier = jumpCutOff;
                    }
                }
                else
                {
                    gravityMultiplier = jumpUpGravityScale;
                }
            }
            else
            {
                gravityMultiplier = defaultGravityScale;
            }

            rigid.velocity = new Vector3(rigid.velocity.x, Mathf.Clamp(rigid.velocity.y, -fallSpeedLimit, jumpForce));
        }
    }
}
