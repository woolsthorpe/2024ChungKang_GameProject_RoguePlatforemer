using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flower : Monster
{
    [SerializeField]
    GameObject ATKPOS;
    [SerializeField]
    GameObject ATKobj;
    float ATKDelay = 0f;
    float MAXDelay = 10f;

    [SerializeField]
    float addrotation = 0f;
    public float rotZ;
    float DelayMin = 2f;
    float color = 1;
    [SerializeField]
    GameObject boss;
    public void IsStop(){
        Stop = true;
    }
    public override void OnHit(float DMG){
        if(Stop==false){
        HP -= DMG*2;
        boss.GetComponent<Monster>().OnHit(DMG*2);
        if(HP<=0){
            Stop = true;
        }
        }
        }
    
    void Start()
    {
        HP = 164;
    }
    
    void ATTACK(){
        Instantiate(ATKobj,ATKPOS.transform.position, Quaternion.Euler(0, 0, rotZ));
    }

    // Update is called once per frame
    void Update()
    {
        if(Stop==false){
        
        ATKDelay += 1f*Time.deltaTime;
        Vector2 newPos = Player.transform.position - transform.position;
        rotZ = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ+addrotation);
        float i = boss.GetComponent<boss>().HP/25;
        if(i>DelayMin){
            if(ATKDelay >i){
                ATTACK();
                ATKDelay = 0f;
            }
        }else{
            
            if(ATKDelay >DelayMin){
                ATTACK();
                ATKDelay = 0f;
            }
        }
        }else{
            color -= 0.01f;
            if(color >0.25f){
            GetComponent<SpriteRenderer>().color = new Color(color,color,color,1);
            }
            if(color<0){
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}
