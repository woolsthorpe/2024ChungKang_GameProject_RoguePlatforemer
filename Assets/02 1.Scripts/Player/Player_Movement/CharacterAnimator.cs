using JY.PlatformerBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JY.PlatformerBase
{
    public class CharacterAnimator : MonoBehaviour
    {
        private CharacterGround groundScript;
        private CharacterMovement movementScript;
        private CharacterJump jumpScript;
        private CharacterHealth healthScript;
        public Animator playerAnimator;

        [SerializeField] private GameObject characterSprite;

        [Header("Particles")]
        [SerializeField] private ParticleSystem jumpParticle;
        [SerializeField] private ParticleSystem runParticle;
        [SerializeField] private ParticleSystem landParticle;
        [Header("Squash and Stretch")]
        [SerializeField] private Vector3 jumpSquashSettings;
        [SerializeField] private Vector3 landSquashSettings;

        [SerializeField] private float landSqueezeMultiplier;
        [SerializeField] private float jumpSqueezeMultiplier;

        [SerializeField] private float landDrop;
        [Header("Calculations")]
        [SerializeField] private bool lockRunningSpeed;
        [SerializeField] private float runningSpeed;
        [SerializeField] private float maxRunningSpeed;
        [SerializeField] private bool playerOnGround;
        [SerializeField] private bool jumpSqueezing;
        [SerializeField] private bool landSqueezing;
        public bool isPlayerLooking;

        void Start()
        {
            movementScript = GetComponent<CharacterMovement>();
            jumpScript = GetComponent<CharacterJump>();
            groundScript = GetComponent<CharacterGround>();
            healthScript = GetComponent<CharacterHealth>();
          

            playerAnimator = GetComponentsInChildren<Animator>()[0];
            //  pipeAnimator = GetComponentsInChildren<Animator>()[1];
            jumpSqueezing = false;
            isPlayerLooking = false;
        }

        // Update is called once per frame
        void Update()
        {


            //animator.SetBool("onGround", groundScript.GetOnGround());

            PlayerRunning();
            CheckForLanding();

            playerAnimator.SetFloat("velocityY", movementScript.currentVelocity.y);
        }

        public void PlayerRunning()
        {
           
            if (!lockRunningSpeed)
            {
                runningSpeed = Mathf.Clamp(Mathf.Abs(movementScript.currentVelocity.x), 0, maxRunningSpeed);
                playerAnimator.SetFloat("runSpeed", runningSpeed);
               // playerAnimator.speed = runningSpeed;

                //if (movementScript.inputDirectinoX != 0 && Mathf.Sign(movementScript.inputDirectinoX) != Mathf.Sign(movementScript.currentVelocity.x)
                //    && !playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Player_Turn"))
                //{
                //    playerAnimator.ResetTrigger("turn");
                //    playerAnimator.SetTrigger("turn");
                //}
            }
            else
                playerAnimator.SetFloat("runSpeed", maxRunningSpeed);
        }
       

        public void JumpEffect()
        {


            playerAnimator.SetBool("onGround", false);
            playerAnimator.SetTrigger("jump");

            if (jumpParticle != null)
                jumpParticle.Play();

            if (!jumpSqueezing && jumpSqueezeMultiplier > 1f)
            {
                StartCoroutine(jumpSqueeze(jumpSquashSettings.x / jumpSqueezeMultiplier, jumpSquashSettings.y * jumpSqueezeMultiplier, jumpSquashSettings.z, 0, true));
              
            }
               
        }
        public void CheckForLanding()
        {
            if (!playerOnGround && groundScript.GetOnGround())
            {
                //animator.ResetTrigger("jump");
                playerOnGround = true;
                playerAnimator.SetBool("onGround", true);

                if (landParticle != null)
                    landParticle.Play();

                if (runParticle != null)
                    runParticle.Play();

                if (!landSqueezing && landSqueezeMultiplier > 1)
                    StartCoroutine(jumpSqueeze(landSquashSettings.x * landSqueezeMultiplier, landSquashSettings.y / landSqueezeMultiplier, landSquashSettings.z, landDrop, false));
            }
            else if (playerOnGround && !groundScript.GetOnGround())
            {
                playerOnGround = false;

                if (runParticle != null)
                    runParticle.Stop();
            }



        }

        public void BounceTumbling()
        {
            playerAnimator.SetBool("onGround", false);
            playerAnimator.ResetTrigger("jump");
            playerAnimator.SetTrigger("tumb");

        }
        public void BounceJump()
        {
            playerAnimator.SetBool("onGround", false);
            playerAnimator.SetTrigger("jump");
        }

        IEnumerator jumpSqueeze(float xSqueeze, float ySqueeze, float durationTime, float dropAmount, bool jumpSqueeze)
        {
            if (jumpSqueeze)
                jumpSqueezing = true;
            else
                landSqueezing = true;


            Vector3 orginSize = Vector3.one;
            Vector3 newSize = new Vector3(xSqueeze, ySqueeze, orginSize.z);

            Vector3 orginPos = Vector3.zero;
            Vector3 newPos = new Vector3(0, -dropAmount, 0);

            float currentTime = 0;
         
            while (currentTime <= 1.0f)
            {
                currentTime += Time.deltaTime / 0.01f;
                characterSprite.transform.localScale = Vector3.Lerp(orginSize, newSize, currentTime);
                characterSprite.transform.localPosition = Vector3.Lerp(orginPos, newPos, currentTime);
                yield return null;
            }

            currentTime = 0f;
           
            while (currentTime <= 1.0f)
            {
                currentTime += Time.deltaTime / durationTime;
                characterSprite.transform.localScale = Vector3.Lerp(newSize, orginSize, currentTime);
                characterSprite.transform.localPosition = Vector3.Lerp(newPos, orginPos, currentTime);
                yield return null;
            }


            if (jumpSqueeze)
                jumpSqueezing = false;
            else
                landSqueezing = false;
        }

        public void CharacterDamaged()
        {
            
        }
        public void CharatcerDeath()
        {
            playerAnimator.SetTrigger("die");
            StartCoroutine(ReTryScene());

        }
        IEnumerator ReTryScene()
        {
            yield return new WaitForSeconds(1.2f);
            SceneManagers.instance.RetryScene();

        }
    }
}
