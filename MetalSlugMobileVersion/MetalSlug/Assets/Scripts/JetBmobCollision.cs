using UnityEngine;
using System.Collections;

public class JetBmobCollision : MonoBehaviour {
    public GameObject jetExplosion;
    private GameObject obj;
    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Ground"||other.gameObject.tag == "Player")
        {
           
            obj=Instantiate(jetExplosion, transform.position, Quaternion.Euler(0, 0, 90)) as GameObject;
           Destroy(obj,0.8f);
        }
        

    }
    
}
