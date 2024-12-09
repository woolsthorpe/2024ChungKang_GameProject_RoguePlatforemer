using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class  BulletEntity :MonoBehaviour
{
    

     protected Rigidbody2D rigid;
     protected Vector3 direction;
    [SerializeField] protected float destroyTime=30;
    [SerializeField] protected float force;

    [Header("데미지")]
    [SerializeField] protected int damage;
    [SerializeField][Range(0,100)] protected int generateCritical_Percent;//치명타 발생확률
    [SerializeField][Range(0,100)] protected int criticalMulitplier;// 전체 데미지의 몇퍼센트를 추가로 줄지

    private Camera mainCam;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        StartCoroutine(destroyBullet(destroyTime));
        mainCam = Camera.main;
    }
    protected int CalculateDamage(int damage,int criPercent,int multiplier)
    {
        if (Random.Range(0, 100) <= criPercent)
        {
            return damage + ((damage * multiplier) / 100);
        }
        else
            return damage;
    }

    //update에서 호출 예정
    protected void ShotBullet()
    {
       // rigid.AddForce(direction*force,ForceMode2D.Impulse);
    }
    private void Update()
    {
        rigid.velocity = direction.normalized * force;
        Vector3 viewPos = mainCam.WorldToViewportPoint(transform.position);

        // 시야 영역을 벗어나면 제거
        if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
        {
            StartCoroutine(destroyBullet(0.1f));
        }
    }
    
    public void DefaultBulletShot(Vector3 dir,float force)
    {
        
        direction = dir;
        this.force = force;
    }

    IEnumerator destroyBullet(float time=0)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
        Debug.Log("삭제 " + time);
    }


    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground"))
        {
            StartCoroutine(destroyBullet());
        }
        //else if(collision.CompareTag("Enemy"))
        //{

        //}

    }
    
}
