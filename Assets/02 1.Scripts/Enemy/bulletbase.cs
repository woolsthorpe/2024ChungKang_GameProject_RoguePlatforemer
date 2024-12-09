using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class bulletbase : MonoBehaviour
{
    [SerializeField]
    float bulletspeed = 0.1f;
    float damage = 1;
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
      //  Debug.Log("collision!");
        if(other.gameObject.CompareTag("Ground")){
            Destroy(gameObject);
        }else if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<CharacterHealth>().GetDamaged(damage,transform);
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(bulletspeed,0,0);
    }
}
