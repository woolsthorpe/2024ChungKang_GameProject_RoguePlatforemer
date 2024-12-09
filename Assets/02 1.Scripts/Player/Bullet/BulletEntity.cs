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

    [Header("������")]
    [SerializeField] protected int damage;
    [SerializeField][Range(0,100)] protected int generateCritical_Percent;//ġ��Ÿ �߻�Ȯ��
    [SerializeField][Range(0,100)] protected int criticalMulitplier;// ��ü �������� ���ۼ�Ʈ�� �߰��� ����

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

    //update���� ȣ�� ����
    protected void ShotBullet()
    {
       // rigid.AddForce(direction*force,ForceMode2D.Impulse);
    }
    private void Update()
    {
        rigid.velocity = direction.normalized * force;
        Vector3 viewPos = mainCam.WorldToViewportPoint(transform.position);

        // �þ� ������ ����� ����
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
       
    }


    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground"))
        {
            StartCoroutine(destroyBullet());
        }
        else if(collision.CompareTag("ENEMY"))
        {
            try{
            collision.GetComponent<Monster>().OnHit(damage);
            StartCoroutine(destroyBullet());
            }catch{
                
            }
        }

    }
    
}
