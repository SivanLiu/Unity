using UnityEngine;
using System.Collections;
public enum Enemy3State { 
    Idle,
    Die,
    GrenadeDie,
    Shoot,
    ShootWalking
}
public class Enemy3Controller : MonoBehaviour {

    //获取敌人的五个状态的脚本
    public GameObject enemy3Idle;
    public GameObject enemy3Die;
    public GameObject enemy3grenadeDie;
    public GameObject enemy3Shoot;
    public GameObject enemy3ShootWalking;
    private GameObject player;
    //移动的速度
    public float m_speed = 0.3f;
    //设置默认的状态为Idle
    public Enemy3State state;
    private float mtime = 0;
    public GameObject prjectileExplosionPrefab;
    public GameObject grenadeExplosionPrefab;
    public GameObject cannoBallPrefab;
    private GameObject cannoball;
    public GameObject cannoballPoint;
    private float timer=0;
    public AudioClip enemyDie;
    public GameObject enemyExplosion;
    private PlayerController playerController;
    private bool isDie=false;
    void Start()
    {
        state = Enemy3State.Idle;
        transform.localScale = new Vector3(1,1,1);
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.x - player.transform.position.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);

        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);

        }
      
        //控制敌人1各种状态之间的切换
        mtime += Time.deltaTime;
        switch (state)
        {
            case Enemy3State.Idle:
                enemy3Idle.SetActive(true);
                enemy3Die.SetActive(false);
                enemy3grenadeDie.SetActive(false);
                enemy3Shoot.SetActive(false);
                enemy3ShootWalking.SetActive(false);
                break;
            case Enemy3State.Die: 
                if (mtime > 1.5f)
                {
                    AudioSource.PlayClipAtPoint(enemyDie, new Vector3(transform.position.x,
                  transform.position.y, transform.position.z));
                    mtime = 0;
                    
                }
                if (enemy3Die != null) {
                    enemy3Die.SetActive(true);
                    enemy3Idle.SetActive(false);
                    enemy3grenadeDie.SetActive(false);
                    enemy3Shoot.SetActive(false);
                    enemy3ShootWalking.SetActive(false);
                    Destroy(this.gameObject,2f);
                }
                break;
            case Enemy3State.GrenadeDie: 
                if (mtime > 1.5f)
                {
                    AudioSource.PlayClipAtPoint(enemyDie, new Vector3(transform.position.x,
                  transform.position.y, transform.position.z));
                    mtime = 0;
                   
                }
                    enemy3Idle.SetActive(false);
                    enemy3Die.SetActive(false);
                    enemy3grenadeDie.SetActive(true);
                    enemy3Shoot.SetActive(false);
                    enemy3ShootWalking.SetActive(false);
                    if (enemy3grenadeDie != null) {
                        Destroy(this.gameObject,2f);
                    }
               
                break;
            case Enemy3State.Shoot:
                if (isDie)
                {
                    state = Enemy3State.Die;
                    isDie = false;
                }
                    enemy3Idle.SetActive(false);
                    enemy3Die.SetActive(false);
                    enemy3grenadeDie.SetActive(false);
                    enemy3Shoot.SetActive(true);
                    enemy3ShootWalking.SetActive(false);
                    timer -= Time.deltaTime;
                    if (timer <= 0 && Mathf.Abs(transform.position.x - player.transform.position.x) < 3.5f)
                    {
                        ShootBall();
                        timer = 0.8f;
                    }
                  
                break;
            case Enemy3State.ShootWalking:
                transform.position=Vector2.MoveTowards(transform.position,player.transform.position,Time.deltaTime*1f);
                enemy3Idle.SetActive(false);
                enemy3Die.SetActive(false);
                enemy3grenadeDie.SetActive(false);
                enemy3Shoot.SetActive(false);
                enemy3ShootWalking.SetActive(true);
                
                break;
            default:
                break;
        }
        if (transform.position.x - player.transform.position.x < 8)
        {
            state = Enemy3State.ShootWalking;
            if (isDie) {
                state = Enemy3State.Die;
                
            }

        }
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < 3.5f)
        {
            state = Enemy3State.Shoot;
            if (isDie)
            {
                state = Enemy3State.Die;
                
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerProjectile")
        {
            isDie = true;
            Instantiate(enemyExplosion, transform.position, transform.rotation);
            Destroy(other.gameObject);
            state = Enemy3State.Die;
            Destroy(this.gameObject, 1f);
        }
        if (other.gameObject.tag == "Grenade")
        {
            Instantiate(grenadeExplosionPrefab, transform.position, transform.rotation);
            this.transform.position += transform.up * 6.5f * Time.deltaTime;
            state = Enemy3State.GrenadeDie;
            Destroy(this.gameObject, 1.2f);
        }

    }
    void FixedUpdate()
    {
        Vector3 direction = (player.transform.position - this.transform.position).normalized;
        Ray ray = new Ray(this.transform.position, direction);
        RaycastHit hitinfo;
        bool isPlayer = Physics.Raycast(ray, out hitinfo, 0.5f);
        if (isPlayer)
        {
            if (playerController!=null&&playerController.Projectile&&transform.localScale.x==-player.transform.localScale.x)
            {
                state = Enemy3State.Die;
                isDie = true;
                Instantiate(enemyExplosion, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject, 0.4f);
            }
            isPlayer = false;
        }
        //Debug.DrawLine(transform.position, ray.direction, Color.red, 1);
    }
   
    void ShootBall() {
            cannoball = Instantiate(cannoBallPrefab, cannoballPoint.transform.position,
                cannoballPoint.transform.rotation) as GameObject;
            if ((!float.IsNaN(BallisticVel(player.transform, cannoball.transform).x))&&
                (!float.IsNaN(BallisticVel(player.transform, cannoball.transform).y))&&
                (!float.IsNaN(BallisticVel(player.transform, cannoball.transform).z)))
            {
                cannoball.rigidbody.velocity = BallisticVel(player.transform, cannoball.transform);
            }
        Destroy(cannoball.gameObject, 1f);
    }
    Vector3 BallisticVel(Transform target, Transform player)
    {
        Vector3 dir = target.position - transform.position;
        float h = dir.y;
        dir.y = 0;
        float dist = dir.magnitude;
        dir.y = dist;
        dist += h;
        float vel = Mathf.Sqrt(dist * Physics.gravity.magnitude);
        return vel * dir.normalized;

    }
}
