using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerWeaponGun : MonoBehaviour
{
    [SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField] private float speed = 4f;
    [SerializeField] private Transform character;
    [SerializeField] private Transform shotPos;

    private bool inputMouse;
    private float angle;

    private Vector3 mouseDir;
    [Header("Shoot")]
    public BulletEntity bulletObj;
    [SerializeField] private float currentDelay;
    [field: SerializeField] public float maxDelay { get; set; }
    [field: SerializeField] public float shotSpeed{get;set;}
    [field: SerializeField] public int bulletCount { get; set; }
   
   


    void Start()
    {
        inputMouse = false;
    }

   
    void Update()
    {
        InputKeys();

        if(AbleShot())
        {
            BulletShot();
        }

        currentDelay += Time.deltaTime;
    }
    private void LateUpdate()
    {
        FollowCharacter();
        RotateWeaporn();
    }

    private void InputKeys()
    {
        inputMouse = UnityEngine.Input.GetMouseButton(0);
    }
    bool AbleShot()
    {
        return (inputMouse && currentDelay >= maxDelay);
    }
    private void BulletShot()
    {
        currentDelay = 0;
        Instantiate(bulletObj,shotPos.position,Quaternion.Euler(0,0,angle)).DefaultBulletShot(transform.up,shotSpeed);
        //�ӽ������� default�� ����
        //���� ���� ����
    }


    void FollowCharacter()
    {
    float characterDirection = Mathf.Sign(character.localScale.x);
    //transform.localScale = new(characterDirection, 1, 1);

    var targetPosition = character.position +
        new Vector3(offset.x * characterDirection, offset.y+1);

    var delta = targetPosition - transform.position;

    transform.position += delta* speed * Time.deltaTime;
    }

    void RotateWeaporn()
    {
        mouseDir = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
        angle = Mathf.Atan2(mouseDir.y-this.transform.position.y,mouseDir.x-this.transform.position.x)*Mathf.Rad2Deg;

        if (mouseDir.x < transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);

         this.transform.rotation = Quaternion.AngleAxis(angle-90,Vector3.forward);
   
    }
}
