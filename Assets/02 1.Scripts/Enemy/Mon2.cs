using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mon2 : Monster
{
    [SerializeField]
    GameObject ATKPOS;
    [SerializeField]
    GameObject ATKobj;
    float ATKDelay = 0f;
    float MAXDelay = 3f;
    float movespeed = 0.1f;
    Animator ani;
    public override void OnHit(float DMG)
    {
        throw new System.NotImplementedException();
    }
    void ATTACK(){
        Vector2 newPos = Player.transform.position - ATKPOS.transform.position;
        float rotZ = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;
        Instantiate(ATKobj,ATKPOS.transform.position,Quaternion.Euler(0, 0, rotZ));
        Debug.Log(Quaternion.Euler(0, 0, rotZ));
    }
    void Start()
    {
        HP = 10;
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(sleep == true){
            if(Vector2.Distance(transform.position,Player.transform.position) < sleeprange){
                sleep = false;
            }
        }else{
            ATKDelay += 1f*Time.deltaTime;
            Vector2 dir =  Player.transform.position - transform.position;
            if(dir.x<-4.5){
                transform.Translate(-movespeed,0,0);
            }else if(dir.x>4.5){
                transform.Translate(movespeed,0,0);
            }else if(dir.x<0&&dir.x>-4){
                transform.Translate(movespeed,0,0);
                transform.localScale = new Vector3(-1f,1f,1f);
            }else if(dir.x>0&&dir.x<4){
                transform.Translate(-movespeed,0,0);
                transform.localScale = new Vector3(1f,1f,1f);
            }
            if(dir.y>-4){
                transform.Translate(0,movespeed,0);
            }
            if(ATKDelay >MAXDelay){
                ani.SetTrigger("ATK");
                ATKDelay = 0f;
            }
            if(Vector2.Distance(transform.position,Player.transform.position) > sleeprange*3){
                
                sleep = true;
            }
        }
    }
}
