using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using UnityEngine;
using Random=UnityEngine.Random;
public class flowerBullet : MonoBehaviour
{
    [SerializeField]
    float bulletspeed = 0.05f;
    float damage = 1;
    Animator ani;
    [SerializeField]
    GameObject Mon1;
    [SerializeField]
    GameObject Mon2;
    bool Stop;
    void Start()
    {
        ani = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("collision!");
        if(other.gameObject.CompareTag("Ground")){
            Stop = true;
            if(Random.Range(1,3)==1){
            ani.SetTrigger("Mon1");
            }else{
            ani.SetTrigger("Mon2");
            }
        }else if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<CharacterHealth>().GetDamaged(damage,transform);
        }
    }
    public void Mon1return(){
        Instantiate(Mon1,transform.position,quaternion.identity);
    }

    public void Mon2return(){
        Instantiate(Mon2,transform.position,quaternion.identity);
    }
    public void dt(){
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if(Stop == false){
        transform.Translate(bulletspeed,0,0);
        }
    }
}
