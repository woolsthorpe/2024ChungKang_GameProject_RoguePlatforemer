
using System.Collections;
using UnityEngine;

namespace JY.PlatformerBase
{
    
    public class CharacterMovement : MonoBehaviour
    {
        private Rigidbody2D rigid;
        private CharacterGround ground;
        private CharacterHealth health;

        [Header("Movement Settings")]
        [Range(0f, 50f)] public float maxSpeed=9.01f;
        [Range(0f, 100f)] public float maxAcceleration;
        [Range(0f, 100f)] public float maxDecceleration;

        [Range(0f, 100f)] public float maxAirAcceleration;
        [Range(0f, 100f)] public float maxAirDecceleration;

        [Range(0f, 100f)] public float maxTurnSpeed;
        [Range(0f, 100f)] public float maxAirTurnSpeed;

        public float friction;
        [Header("Options")]
        [SerializeField] private bool useAcceleration=true;
        private float maxSpeedChange;

     
      
        [SerializeField] private Vector2 desireVelocity;
        [SerializeField] private bool isPressingKey;
        [SerializeField] private bool onGround;
        [field: SerializeField] public float inputDirectinoX { private set; get; }
        public Vector2 currentVelocity;
        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
            ground = GetComponent<CharacterGround>();
            health = GetComponent<CharacterHealth>();
            
        }

        private void InputMovement()
        {
            //���Ŀ� InputAction ����� ����
            inputDirectinoX = Input.GetAxisRaw("Horizontal");
        }
        private void Update()
        {
            InputMovement();

            if (health.isDie)
                return;


            //Turn
            if (inputDirectinoX != 0)
            {
                isPressingKey = true;
                transform.localScale = new Vector3(Mathf.Sign(inputDirectinoX), 1, 1);
            }
            else
                isPressingKey = false;

            desireVelocity = new Vector2(inputDirectinoX,0f) * Mathf.Max(maxSpeed-friction,0f);
          //  Debug.Log(rigid.velocity.y);
        }

        public void Damaged_knockback(Vector2 direction,float amount)
        {
            
            rigid.velocity = direction.normalized * amount;
           
        }
     
        
        private void FixedUpdate()
        {
        
            currentVelocity = rigid.velocity;
            onGround = ground.GetOnGround();


            if (health.isDie)
                return;

            if (useAcceleration)
                RunWIthAcceleration();
            else
            {
                if (onGround)
                    RunWithoutAcceleration();
                else
                    RunWIthAcceleration();
            }
                
        }


        private void RunWIthAcceleration()
        {
         

            if (isPressingKey)
            {
                if (Mathf.Sign(inputDirectinoX) != Mathf.Sign(currentVelocity.x))
                    maxSpeedChange = onGround ? maxTurnSpeed : maxAirTurnSpeed;
                else
                    maxSpeedChange = onGround ?maxAcceleration:maxAirAcceleration;
            }
            else
                maxSpeedChange = onGround ? maxDecceleration : maxAirDecceleration;

            currentVelocity.x = Mathf.MoveTowards(currentVelocity.x,desireVelocity.x,maxSpeedChange*Time.deltaTime);
            rigid.velocity = currentVelocity;
        }
        private void RunWithoutAcceleration()
        {
            currentVelocity.x = desireVelocity.x;
            rigid.velocity = currentVelocity;
        }
    }
}
