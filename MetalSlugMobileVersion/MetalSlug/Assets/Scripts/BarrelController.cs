using UnityEngine;
using System.Collections;

public class BarrelController : MonoBehaviour {
    public GameObject barrelExplosionPrefab;
    public GameObject projectileExplosion;
    public GameObject pills;
    //被子弹击中的次数
    private int count = 0;
    public float x, y;
    public SpriteRenderer spriteRenderer;
    public Sprite[] barrelSpriteArray;
    private int index = 0;
	// Use this for initialization
	void Start () {
        spriteRenderer.sprite=barrelSpriteArray[0];
	}
	
	// Update is called once per frame
	void Update () {
	}
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "PlayerProjectile") {
            Destroy(other.gameObject);
            Instantiate(projectileExplosion, other.transform.position, other.transform.rotation);
            count++;
            if (count == 2) {
                spriteRenderer.sprite=barrelSpriteArray[1];
            }
            else if (count == 4) {
                spriteRenderer.sprite = barrelSpriteArray[2];
            }
            else if (count >= 6) {
                Instantiate(barrelExplosionPrefab, transform.position, transform.rotation);
                Destroy(this.gameObject, 0.5f);
                Instantiate(pills, transform.position, transform.rotation);
                DigitDisplay.score += 300;
            }
        }
        if (other.gameObject.tag == "Grenade") {
            Instantiate(barrelExplosionPrefab, transform.position, transform.rotation);
            Destroy(this.gameObject, 0.5f);
            DigitDisplay.score += 300;
            Instantiate(pills, transform.position, transform.rotation);
        }
        //if (other.gameObject.tag == "Player") {
        //    this.rigidbody.velocity = new Vector3(0,0,0);
        //}
    }
}
