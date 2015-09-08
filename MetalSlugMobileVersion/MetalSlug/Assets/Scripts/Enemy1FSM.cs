using UnityEngine;
using System.Collections;
public enum FSMState
{
    Walking,
    FirstMove,
    Idle,
    Die,
    Kill,
    GrenadeDie,
    Disappear
}
public class Enemy1FSM : FSM {
    //定义状态机的5种状态：行走，移动，空闲，刺杀，被手榴弹炸状态以及其余死亡状态
    public FSMState curState;
    //获得Enemy1的五种状态
    public GameObject enemy1Idle;
    public GameObject enemy1FirstMove;
    public GameObject enemy1grenadeDie;
    public GameObject enemy1Kill;
    public GameObject enemy1Walking;
    public GameObject enemy1Die;
    private GameObject player;
    private PlayerController playercontroller;
    public GameObject enemyExplosion;
    private float mDistance;

    public AudioClip enemyDie;
    //移动的速度
    public float m_speed = 0.3f;
    //设置默认的状态为Idle
    public Enemy1State state;
    private float mtime = 0;
    public GameObject prjectilePrefab;
    public GameObject grenadeExplosionPrefab;
    private int x = 1;
    private bool isPlay = false;
  
    protected override void Initialize()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playercontroller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        curState = FSMState.Idle;
        isDirection();
    }
    protected override void FSMUpdate()
    {
       
        mtime += Time.deltaTime;
       
        switch (curState)
        {
            case FSMState.Walking:
                UpdateWalkingState();
                break;
            case FSMState.FirstMove:
                UpdateFirstMoveState();
                break;
            case FSMState.Idle:
                UpdateIdleState();
                break;
            case FSMState.Die:
                if (mtime > 1.5f)
                {
                    AudioSource.PlayClipAtPoint(enemyDie, new Vector3(transform.position.x,
                        transform.position.y, transform.position.z));
                    mtime = 0;
                }
                UpdateDieState();
                break;
            case FSMState.GrenadeDie: 
                if (mtime > 1.5f)
                {
                    AudioSource.PlayClipAtPoint(enemyDie, new Vector3(transform.position.x,
                        transform.position.y, transform.position.z));
                    mtime = 0;
                }
                UpdateGrenadeDieState();
                break;
            case FSMState.Kill:
                UpdateKillState();
                break;
            case FSMState.Disappear:
                UpdatedDisappear();
                break;
            default:
                break;
        }
    }
        protected void UpdateWalkingState()
    {
        transform.position += transform.right * isDirection() * m_speed *2f * Time.deltaTime;
            enemy1Idle.SetActive(false);
            enemy1FirstMove.SetActive(false);
            enemy1grenadeDie.SetActive(false);
            enemy1Kill.SetActive(false);
            enemy1Walking.SetActive(true);
            enemy1Die.SetActive(false);
        }
        protected void UpdateFirstMoveState(){
            StartCoroutine(WaitForTime(0.5f,FSMState.Walking,true));
            enemy1Idle.SetActive(false);
            enemy1FirstMove.SetActive(true);
            enemy1grenadeDie.SetActive(false);
            enemy1Kill.SetActive(false);
            enemy1Walking.SetActive(false);
            enemy1Die.SetActive(false);
        }
        protected void UpdateIdleState(){
            enemy1Idle.SetActive(true);
            StartCoroutine(WaitForTime(0.5f,FSMState.FirstMove,true));
            enemy1FirstMove.SetActive(false);
            enemy1grenadeDie.SetActive(false);
            enemy1Kill.SetActive(false);
            enemy1Walking.SetActive(false);
            enemy1Die.SetActive(false);
        }
        protected void UpdateDieState(){
            enemy1Idle.SetActive(false);
            enemy1FirstMove.SetActive(false);
            enemy1grenadeDie.SetActive(false);
            enemy1Kill.SetActive(false);
            enemy1Walking.SetActive(false);
            enemy1Die.SetActive(true);
            StartCoroutine(isDieOut(0.6f));
        }
        protected void UpdateGrenadeDieState() {
            enemy1Idle.SetActive(false);
            enemy1FirstMove.SetActive(false);
            enemy1grenadeDie.SetActive(true);
            enemy1Kill.SetActive(false);
            enemy1Walking.SetActive(false);
        }
        protected void UpdateKillState()
        {
            transform.position += transform.right *isDirection()*m_speed * 1f * Time.deltaTime;
            enemy1Idle.SetActive(false);
            enemy1FirstMove.SetActive(false);
            enemy1grenadeDie.SetActive(false);
            enemy1Kill.SetActive(true);
            enemy1Walking.SetActive(false);
            enemy1Die.SetActive(false);
        }
        protected void UpdatedDisappear() {
            enemy1Idle.SetActive(false);
            enemy1FirstMove.SetActive(false);
            enemy1grenadeDie.SetActive(false);
            enemy1Kill.SetActive(false);
            enemy1Walking.SetActive(false);
            enemy1Die.SetActive(false);
        }
        bool isValidStateChange(FSMState newstate) {
            bool returnVal=false;
            switch(curState){
                case FSMState.Idle:
                    if(newstate==FSMState.FirstMove||newstate==FSMState.Die||newstate==FSMState.GrenadeDie){
                        returnVal=true;
                    }
                    else
                        return false;
                    break;
                case FSMState.FirstMove:
                    if (newstate == FSMState.Kill || newstate == FSMState.Die||newstate==FSMState.GrenadeDie)
                    {
                        returnVal = true;
                    }
                    else
                        returnVal = false;
                    break;
                case FSMState.Kill:
                    if (newstate == FSMState.Die || newstate == FSMState.GrenadeDie)
                    {
                        returnVal = true;
                    }
                    else
                        returnVal = false;
                    break;
                case FSMState.Die:
                    if (newstate == FSMState.Disappear)
                    {
                        returnVal = true;
                    }
                    else
                        returnVal = false;
                    break;
                case FSMState.GrenadeDie:
                    if (newstate == FSMState.Disappear)
                    {
                        returnVal = true;
                    }
                    else
                        returnVal = false;
                    break;
                default:
                    break;
            }
            return returnVal;
        }
    //敌人1的状态之间的切换
        IEnumerator WaitForTime(float time,FSMState state,bool isMove) {
            yield return new WaitForSeconds(time);
            curState = state;
            if (isMove) {
                transform.position += transform.right *isDirection()*m_speed * Time.deltaTime;
            }
        }
        IEnumerator isDieOut(float time) {
            yield return new WaitForSeconds(time);
            enemy1Die.SetActive(false);
        }
        IEnumerator isLife(float time) {
            yield return new WaitForSeconds(time);
            this.gameObject.SetActive(true);
        }
  
    protected override void FSMFixedUpdate()
    {
        Vector3 direction = (player.transform.position - this.transform.position).normalized;
        Ray ray = new Ray(this.transform.position, direction);
        RaycastHit hitinfo;
        bool isPlayer = Physics.Raycast(ray, out hitinfo, 0.5f);
        if (isPlayer)
        {
            curState = FSMState.Kill;
            if (playercontroller != null && playercontroller.Projectile && transform.localScale.x == -player.transform.localScale.x)
            {
                curState = FSMState.Die;
                Instantiate(enemyExplosion, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject,0.5f);
                }
            isPlay = false;
        }
        //Debug.DrawLine(transform.position, ray.direction, Color.red, 1);
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "PlayerProjectile" ) {
                curState = FSMState.Die;
                Instantiate(enemyExplosion,this.transform.position,Quaternion.identity);
                Destroy(other.gameObject);
                Destroy(this.gameObject,1.5f);
            }
        if (other.gameObject.tag == "Grenade") {
            curState = FSMState.GrenadeDie;
        }
    }
   
 
    protected int isDirection() {
        if (player.transform.position.x < transform.position.x)
        {
            this.transform.localScale = new Vector2(1,transform.localScale.y);
            return -x;
        }
        else
        {
            this.transform.localScale = new Vector2(-1, transform.localScale.y);
            return x ;
        }
       
    }
}
