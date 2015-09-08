using UnityEngine;
using System.Collections;

public class TankController : MonoBehaviour {

    public GameObject TankFire;
    //坦克发射炮弹的位置
    public GameObject FirePos;
    public GameObject TankFireExplosion;
    public GameObject ProjectileExplosion;

    public AudioClip tankShotSound;
    public AudioClip tankExplosion;
    public AudioClip projectileSound;
    //坦克被击毁是的爆炸效果
    public GameObject Explosion;
    Transform mTop;
    GameObject mFire;
    private float mTime = 0;
    //记录坦克被子弹击中的次数
    int hitTimes = 0;
    //添加bool变量，以免炮塔初始时会向前移动
    private bool isMove=false;
    private GameObject player;
    public AudioClip tankRun;
    public Camera camera;
	// Use this for initialization
	void Start () {
        mTop = transform.FindChild("top");
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        mTime += Time.deltaTime;
        if (this.transform.position.x - player.transform.position.x > 6&&this.transform.position.x - player.transform.position.x <8) {
           
            rigidbody.MovePosition(Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * 0.15f));
        }

        //if (transform.position.x - player.transform.position.x >4f) {
        //    rigidbody.MovePosition(Vector3.MoveTowards(transform.position,player.transform.position,Time.deltaTime*0.15f));
           
        //}
       
        if (mTime > 2 && player.transform.position.x + 6f>=transform.position.x)
        {
            isMove = true;
            //炮塔向后移动一段距离
            mTop.Translate(new Vector3(0.10f * Time.deltaTime, 0, 0));
            if (mTime > 2.1f)
            {
                mTime = 0;
                mFire = Instantiate(TankFire, FirePos.transform.position, transform.rotation) as GameObject;
                AudioSource.PlayClipAtPoint(tankShotSound, new Vector3(transform.position.x,
                  transform.position.y, transform.position.z));
                //mFire.rigidbody.velocity = new Vector3(-3.5f, 4f, 0.0f);
                mFire.rigidbody.velocity = (player.transform.position-transform.position).normalized*8.5f;
                mFire.transform.localEulerAngles = new Vector3(0.0f, 0.0f, Random.Range(10, 30));
                Destroy(mFire, 2.0f);
            }
          
        }  //炮塔向左移动，恢复原来的位置
        else if(isMove&&mTime>0&&mTime<2) {
            isMove = false;
            mTop.position = new Vector3(-0.2f + transform.position.x, mTop.position.y, mTop.position.z);
        }
	}
    public void SetTime(float time) {
        mTime = time;
    }
    void FixedUpdate() {
        if (rigidbody.velocity.magnitude > 0&&mTime>0.86f)
        {
            AudioSource.PlayClipAtPoint(tankRun, new Vector3(transform.position.x,
                        transform.position.y, transform.position.z));
            mTime = 0;
        }
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "PlayerProjectile") {
            AudioSource.PlayClipAtPoint(projectileSound, new Vector3(transform.position.x,
                 transform.position.y, transform.position.z));
            Instantiate(ProjectileExplosion,other.transform.position+new Vector3(0,0,-1.0f),Quaternion.identity);
            Destroy(other.gameObject);
            hitTimes++;
            if (hitTimes >= 10) {
                Instantiate(Explosion,transform.position+new Vector3(0,0.8f,0),transform.rotation);
                Destroy(this.gameObject,0.5f);
                DigitDisplay.score += 2000;
            }
        }
        if (other.gameObject.tag == "Grenade") {
            AudioSource.PlayClipAtPoint(tankExplosion, new Vector3(transform.position.x,
                 transform.position.y, transform.position.z));
            Destroy(other.gameObject,1.1f);
            Instantiate(Explosion, transform.position,transform.rotation);
            Destroy(this.gameObject,1.0f);
            DigitDisplay.score += 2000;
        }
    }
}
