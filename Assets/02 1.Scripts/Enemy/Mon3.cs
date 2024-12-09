using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mon3 : Monster
{
    Animator ani;
    [SerializeField]
    GameObject ATKPOS;
    [SerializeField]
    GameObject ATKobj;
    float ATKDelay = 0f;
    float MAXDelay = 3f;
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
    void ATTACK(){
        Vector2 newPos = Player.transform.position - ATKPOS.transform.position;
        float rotZ = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;
        Instantiate(ATKobj,ATKPOS.transform.position,Quaternion.Euler(0, 0, rotZ));
    }
    void Start()
    {
        HP = 7;
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(sleep == false){
        ATKDelay += 1f*Time.deltaTime;
        Vector2 dir = Player.transform.position - transform.position;
        if(dir.x>0){
            transform.localScale = new Vector3(1f,1f,1f);
            
        } else{
            transform.localScale = new Vector3(-1f,1f,1f);
        }
            if(ATKDelay >MAXDelay){
                ani.SetTrigger("Attack");
                ATKDelay = 0f;
            }
            if(Vector2.Distance(transform.position,Player.transform.position) > sleeprange*1.5){
                
                sleep = true;
            }
        }else{
            if(Vector2.Distance(transform.position,Player.transform.position) < sleeprange){
                sleep = false;
            }
        }
    }
}
