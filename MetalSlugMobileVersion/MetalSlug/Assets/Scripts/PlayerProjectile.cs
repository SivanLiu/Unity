using UnityEngine;
using System.Collections;

public class PlayerProjectile : MonoBehaviour {
    //子弹的速度
    public float m_speed = 5;
    public AudioClip hit;
    public GameObject projectileExplosionPrefab;
    public GameObject enemyexplosion;
	void Start () {
        Destroy(this.gameObject,2.0f);
	}
	
	void Update () {
        transform.Translate(Vector3.right*m_speed*Time.deltaTime);
        if (IsOutOfScreen(this.gameObject, camera)) {
            Destroy(this.gameObject);
        }
	}
   
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Enemy1" || other.gameObject.tag == "Enemy2" || other.gameObject.tag == "Enemy3") 
        {
            AudioSource.PlayClipAtPoint(hit, new Vector3(transform.position.x,
                    transform.position.y, transform.position.z));
            DigitDisplay.score += 100;
            Destroy(this.gameObject);
            //Instantiate(projectileExplosionPrefab,this.transform.position,transform.rotation);
          
                //(this.transform.localScale.x ==-1 ? new Vector3(1f, 0, 0) : new Vector3(-0.8f, 0, 0))
            Instantiate(enemyexplosion, this.transform.position, transform.rotation);
        }
        if (other.gameObject.tag == "Jet")
        {
            DigitDisplay.score += 100;
            Destroy(this.gameObject);
            Instantiate(projectileExplosionPrefab, this.transform.position, transform.rotation);
        }
        if (other.gameObject.tag == "Ground1" || other.gameObject.tag == "GroundStone")
        {
            Destroy(this.gameObject);
            Instantiate(projectileExplosionPrefab, this.gameObject.transform.position , Quaternion.identity);
        }
    }
   
    public bool IsOutOfScreen(GameObject o, Camera cam = null)
    {
        bool result = false;
        Renderer ren = o.GetComponent<Renderer>();
        if (ren)
        {
            if (cam == null) cam = Camera.main;
            Vector2 sdim = SpriteScreenSize(o, cam);
            Vector2 pos = cam.WorldToScreenPoint(o.transform.position);
            Vector2 min = pos - sdim;
            Vector2 max = pos + sdim;
            if (min.x > Screen.width || max.x < 0f ||
                min.y > Screen.height || max.y < 0f)
            {
                result = true;
            }
        }
        return result;
    }

    public Vector2 SpriteScreenSize(GameObject o, Camera cam = null)
    {
        if (cam == null) cam = Camera.main;
        Vector2 sdim = new Vector2();
        Renderer ren = o.GetComponent<Renderer>() as Renderer;
        if (ren)
        {
            sdim = cam.WorldToScreenPoint(ren.bounds.max) -
                cam.WorldToScreenPoint(ren.bounds.min);
        }
        return sdim;
    }
}
