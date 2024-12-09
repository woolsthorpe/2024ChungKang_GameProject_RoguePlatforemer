using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mon1 : Monster
{
    Animator ani;
    public override void OnHit(float DMG)
    {
        HP -= DMG;
        if(HP <=0){
            ani.SetTrigger("Death");
            Destroy(this);
        }else{
            sleep = false;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            Debug.Log("플레이어랑 만남!");
            other.gameObject.GetComponent<CharacterHealth>().GetDamaged(ATKDMG,transform);
        }
        
    }
    void Start()
    {
        HP = 3;
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = Player.transform.position - transform.position;
        if(dir.x>0){
            transform.Translate(0.03f,0,0);
            transform.localScale = new Vector3(1f,1f,1f);
            
        } else{
            transform.Translate(-0.03f,0,0);
            transform.localScale = new Vector3(-1f,1f,1f);
        }
    }
}
