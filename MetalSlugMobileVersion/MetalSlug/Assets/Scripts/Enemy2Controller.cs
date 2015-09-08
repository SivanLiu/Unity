using UnityEngine;
using System.Collections;
public enum Enemy2State { 
    Idle,
    Die,
    GrenadeDie,
    Throw,
}
public class Enemy2Controller : MonoBehaviour {
    //定时器
    private float mtime=0;
    public Enemy2State state;
    public GameObject Enemy2Idle;
    public GameObject Enemy2Die;
    public GameObject Enemy2GrenadeDie;
    public GameObject Enemy2Throw;

    public GameObject prjectileExplosionPrefab;
    public GameObject grenadeExplosionPrefab;
    public GameObject bombPrefab;
    //是否投掷手榴弹
    public bool isBomb=false;
    private GameObject bomb;
    private float timer;
    public AudioClip enemyDie;
    private GameObject player;
    private PlayerController playerController;
    public GameObject enemyExplosion;
    // Use this for initialization
    void Start()
    {
       
        state = Enemy2State.Idle;
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.x > player.transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);

        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);

        }
        mtime += Time.deltaTime;
        switch (state)
        {
             
            case Enemy2State.Idle:
                if (mtime > 0.8f)
                {
                    if (Mathf.Abs(transform.position.x - transform.position.x) < 3.5f) {
                        state = Enemy2State.Throw;
                    }
                    
                }
                Enemy2Idle.SetActive(true);
                Enemy2Die.SetActive(false);
                Enemy2GrenadeDie.SetActive(false);
                Enemy2Throw.SetActive(false);
                break;
            case Enemy2State.Die: 
                if (mtime > 1.5f)
                {
                    AudioSource.PlayClipAtPoint(enemyDie, new Vector3(transform.position.x,
                  transform.position.y, transform.position.z));
                    mtime = 0;
                }
                Enemy2Idle.SetActive(false);
                Enemy2Die.SetActive(true);
                Enemy2GrenadeDie.SetActive(false);
                Enemy2Throw.SetActive(false);
                if (Enemy2Die != null) {
                    Destroy(this.gameObject,1f);
                }
                break;
            case Enemy2State.GrenadeDie:
                if (mtime > 1.5f) {
                    AudioSource.PlayClipAtPoint(enemyDie, new Vector3(transform.position.x,
                  transform.position.y, transform.position.z));
                    mtime = 0;
                }
               
                Enemy2Idle.SetActive(false);
                Enemy2Die.SetActive(false);
                Enemy2GrenadeDie.SetActive(true);
                Enemy2Throw.SetActive(false);
                if (Enemy2Throw != null)
                {
                    Destroy(this.gameObject, 1f);
                }
                break;
            case Enemy2State.Throw:
                Enemy2Idle.SetActive(false);
                Enemy2Die.SetActive(false);
                Enemy2GrenadeDie.SetActive(false);
                Enemy2Throw.SetActive(true);
                timer -= Time.deltaTime;
                if (timer < 0 && Mathf.Abs(transform.position.x -player.transform.position.x) < 3.5f)
                {
                    bomb = Instantiate(bombPrefab, this.transform.position, this.transform.rotation) as GameObject;
                    if ((!float.IsNaN(BallisticVel(player.transform, bomb.transform).x)) &&
                    (!float.IsNaN(BallisticVel(player.transform, bomb.transform).y)) &&
                    (!float.IsNaN(BallisticVel(player.transform, bomb.transform).z)))
                    bomb.rigidbody.velocity =1.1f*BallisticVel(player.transform,bomb.transform);
                    timer = 0.5f;
                    Destroy(bomb, 5);
                }
                break;
            default:
                break;
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerProjectile")
        {
            Instantiate(enemyExplosion, transform.position, transform.rotation);
            Destroy(other.gameObject);
            state = Enemy2State.Die;
            Destroy(this.gameObject, 0.8f);
        }
        if (other.gameObject.tag == "Grenade")
        {
            Instantiate(grenadeExplosionPrefab, transform.position, transform.rotation);
            this.transform.position += transform.up * 6.5f * Time.deltaTime;
            state = Enemy2State.GrenadeDie;
            Destroy(this.gameObject, 1.2f);
        }
      
    }
    void isBombE() {
        isBomb = true;    
    }
    void FixedUpdate() {
        Vector3 direction = (player.transform.position - this.transform.position).normalized;
        Ray ray = new Ray(this.transform.position, direction);
        RaycastHit hitinfo;
        bool isPlayer = Physics.Raycast(ray, out hitinfo, 0.5f);
        if (isPlayer)
        {
            state = Enemy2State.Throw;
            if (playerController != null && playerController.Projectile && transform.localScale.x == -player.transform.localScale.x)
            {
                state = Enemy2State.Die;
                Instantiate(enemyExplosion, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject, 0.5f);
            }
            isPlayer = false;
        }
        //Debug.DrawLine(transform.position, ray.direction, Color.red, 1);
    }
    //炮弹跟随目标
    Vector3 BallisticVel(Transform target,Transform player) {
        //获得目标的方向
        Vector3 dir = target.position - transform.position;
        //保存高度
        float h = dir.y;
        //清除方向的高度
        dir.y = 0;
        //获取水平距离
        float dist = dir.magnitude;
        dir.y = dist;
        dist += h;
        //计算炮弹的速度
        float vel = Mathf.Sqrt(dist*Physics.gravity.magnitude);
        return vel * dir.normalized;

    }
}
