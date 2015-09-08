using UnityEngine;
using System.Collections;

public class GroundController : MonoBehaviour {

    public GameObject mgrenadeExplosion;
    public GameObject bombExplosionPrefab;
    public GameObject jetBombExplosion;
    public GameObject projectileExplosion;
    public AudioClip bombExplosion;
    public AudioClip enemy2bomb;
   // public GameObject projectileExplosion;
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Grenade") {
            Instantiate(mgrenadeExplosion, other.transform.position + new Vector3(0, 0.64f, 0), transform.rotation);
            Destroy(other.gameObject);
        }
     
        if (other.gameObject.tag=="Enemy3CannoBall")
        {
            
            Instantiate(bombExplosionPrefab, other.transform.position + new Vector3(0, 0.64f, 0), transform.rotation);
            AudioSource.PlayClipAtPoint(bombExplosion, new Vector3(transform.position.x,
                       transform.position.y, transform.position.z), 0.3f);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Bomb")
        {

            Instantiate(bombExplosionPrefab, other.transform.position + new Vector3(0, 0.64f, 0), transform.rotation);
            AudioSource.PlayClipAtPoint(enemy2bomb, new Vector3(transform.position.x,
                       transform.position.y, transform.position.z),0.4f);
           
            Destroy(other.gameObject);
        }
        //if (other.gameObject.tag == "PlayerProjectile") {
        //    Destroy(other.gameObject);
        //    Instantiate(projectileExplosion,other.transform.position,other.transform.rotation);

        //}
    }
  
   
}
