using UnityEngine;
using System.Collections;
//敌人1所有状态
public enum Enemy1State
{
    Idle,
    FirstMove,
    GrenadeDie,
    Kill,
    Walking,
    Die
}
public class Enemy1Controller : MonoBehaviour {
    //获取敌人六个状态的脚本
    public GameObject enemy1Idle;
    public GameObject enemy1FirstMove;
    public GameObject enemy1grenadeDie;
    public GameObject enemy1Kill;
    public GameObject enemy1Walking;
    public GameObject enemy1Die;
    public GameObject enemy1;
    public GameObject player;
    //移动的速度
    public float m_speed = 0.3f;
    //设置默认的状态为Idle
    public Enemy1State state;   
    private float mtime=0;
    public GameObject prjectilePrefab;
    public GameObject grenadeExplosionPrefab;
    private float mDistance;
    public AudioClip enemyDie;
	void Start () {
        state = Enemy1State.Idle;
	}
	
	// Update is called once per frame
    void Update()
    {  
        //控制敌人1各种状态之间的切换
        mtime += Time.deltaTime; 
        mDistance=Vector3.Distance(transform.position, player.transform.position);
        if (mDistance < 0.5f) {
            state = Enemy1State.Die;
        }
        //StartCoroutine(WaitForTime());
        if (enemy1 != null)
        {
            switch (state)
            {
                case Enemy1State.Idle:
                    enemy1Idle.SetActive(true);
                    enemy1FirstMove.SetActive(false);
                    enemy1grenadeDie.SetActive(false);
                    enemy1Kill.SetActive(false);
                    enemy1Walking.SetActive(false);
                    enemy1Die.SetActive(false);
                    if (mtime > 1)
                    {
                        state = Enemy1State.Walking;
                    }
                    break;
                case Enemy1State.FirstMove:
                    transform.position += transform.right * -m_speed * Time.deltaTime;
                    enemy1Idle.SetActive(false);
                    enemy1FirstMove.SetActive(true);
                    enemy1grenadeDie.SetActive(false);
                    enemy1Kill.SetActive(false);
                    enemy1Walking.SetActive(false);
                    enemy1Die.SetActive(false);
                    break;
                case Enemy1State.GrenadeDie:
                  
                    enemy1Idle.SetActive(false);
                    enemy1FirstMove.SetActive(false);
                    enemy1grenadeDie.SetActive(true);
                    enemy1Kill.SetActive(false);
                    enemy1Walking.SetActive(false);
                    enemy1Die.SetActive(false);
                    break;
                case Enemy1State.Kill:
                    transform.position += transform.right * -m_speed * Time.deltaTime;
                    if (mDistance < 0.5)
                    {
                        state = Enemy1State.Die;
                    }
                    enemy1Idle.SetActive(false);
                    enemy1FirstMove.SetActive(false);
                    enemy1grenadeDie.SetActive(false);
                    enemy1Kill.SetActive(true);
                    enemy1Walking.SetActive(false);
                    enemy1Die.SetActive(false);
                    break;
                case Enemy1State.Walking:
                    if (enemy1Walking != null)
                    {
                        transform.position += transform.right * -m_speed * Time.deltaTime;
                        if (mDistance < 1.5)
                        {
                            state = Enemy1State.Kill;

                        }
                        enemy1Idle.SetActive(false);
                        enemy1FirstMove.SetActive(false);
                        enemy1grenadeDie.SetActive(false);
                        enemy1Kill.SetActive(false);
                        enemy1Walking.SetActive(true);
                        enemy1Die.SetActive(false);
                    }
                    break;
                case Enemy1State.Die:
                    Destroy(this.gameObject, 0.7f);
                    enemy1Idle.SetActive(false);
                    enemy1FirstMove.SetActive(false);
                    enemy1grenadeDie.SetActive(false);
                    enemy1Kill.SetActive(false);
                    enemy1Walking.SetActive(false);
                    enemy1Die.SetActive(true);
                    if (mtime > 10)
                    {
                        Instantiate(enemy1, this.transform.position, transform.rotation);
                    }
                    break;
                default:
                    break;
            }
           
            }
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerProjectile")
        {
            //Instantiate(prjectilePrefab, transform.position, transform.rotation);
            state = Enemy1State.Die;
            Destroy(this.gameObject, 1f);
        }
        if (other.gameObject.tag == "Grenade")
        {
            Instantiate(grenadeExplosionPrefab, transform.position, transform.rotation);
            this.transform.position += transform.up * 4.5f * Time.deltaTime;
            state = Enemy1State.GrenadeDie;
            Destroy(this.gameObject, 1.2f);
        }

    }
    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(0.2f);
        state = Enemy1State.FirstMove;
        transform.position += transform.right * -m_speed * Time.deltaTime;
        yield return new WaitForSeconds(0.5f);
        state = Enemy1State.Walking;
        transform.position += transform.right * -m_speed * Time.deltaTime;
        if (mDistance < 1.5f)
        {
            state = Enemy1State.Kill;
            if (mDistance < 0.5f)
            {
                state = Enemy1State.Die;
            }
            Destroy(this.gameObject,2);
        }
    }
   
}

