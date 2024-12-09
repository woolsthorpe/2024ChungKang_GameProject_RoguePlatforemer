using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class bulletbase : MonoBehaviour
{
    [SerializeField]
    float bulletspeed = 0.05f;
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("collision!");
        if(other.gameObject.CompareTag("GROUND")){
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(bulletspeed,0,0);
    }
}
