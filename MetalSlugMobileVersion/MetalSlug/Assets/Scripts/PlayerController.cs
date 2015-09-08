using UnityEngine;
using System.Collections;
public enum PlayerState { 
    PlayerGround,
    PlayerDown,
    PlayerJump
}

public class PlayerController : MonoBehaviour
{
    private bool walkshotupstate = false;
    //移动的速度
    public float m_speed = 2.0f;
    //跳跃的速度
    public float jumpSpeed = 1.2f;
    //设置默认的状态为跳跃
    public PlayerState state = PlayerState.PlayerGround;
    private int groundLayerMask;
    private int slopeLayerMask;
    private bool iSlope = false;
    //判断是否为地面状态
    private bool isGround = false;
    //判断是否按下Down状态对应的按键
    private bool isBottomButtonKeyClick = false;
    public PlayerShoot playerShoot;
    public PlayerThrowGrenade playerThrowGrenade;

    //获取三个脚本状态
    public PlayerGround playerGround;
    public PlayerDown playerDown;
    public PlayerJump playerJump;

    public GameObject tankFireExplosion;
    public GameObject projectileprefab;
    public GameObject bombExplosion;

    public Enemy1FSM enemy1T;
    public AudioClip playerDie;
    //主角所有的碰撞检测
    public AudioClip giftSound;
    //判断player是否死亡
    bool killed = false;
    //播放一次死亡声音
    bool oneTimeSound = false;
    float mtime = 0;
    //射线的长度
    public float rayLength = 0.84f;
    public float raySpeedRate = 10f;
    public GameObject winPos;

    //上下左右状态
    public bool Top = false;
    public bool Down = false;
    public bool Right = false;
    public bool Left = false;

    public bool UpToWalk = false;
    public bool DownToWalk = false;
    public bool UpToShot = false;
    public bool DownToShot = false;

    //判断方向
    //左边
    public bool LA = false;
    //右边
    public bool RA = false;

    public bool Jump = false;
    public bool Projectile = false;
    public bool Grenade = false;

    //射击方向
    public bool ShootUp = false;
    public bool ShootLeft = false;
    public bool ShootRight = false;
    int ForWard = 1;
    private GameObject player;
    private bool RLshoot = false;

    private float timer = 0;

    private float rayNumbers = 10f;

    private float jumptime = 0;
    public GameObject raycastPos;
    private bool isEnemy = false;
    void Start()
    {
        //取得地面的层
        groundLayerMask = LayerMask.GetMask("Ground");
        player = GameObject.FindGameObjectWithTag("Player");
        if (Application.loadedLevelName == "play5" && player.transform.FindChild("PlayerGround")!=null)
        {
            player.transform.FindChild("PlayerGround").transform.localScale = new Vector3(-1, 1, 1);
        }
       
    }

    void Update()
    {
       
        float xFactor = Screen.width / 720f;
        float yFactor = Screen.height / 1280f;
        Camera.main.rect = new Rect(0, 0, 1, xFactor / yFactor);
        //按下S键
        if (Input.GetKeyDown(KeyCode.S))
        {
            isBottomButtonKeyClick = true;
        }
        //松开按键
        if (Input.GetKeyUp(KeyCode.S))
        {
            isBottomButtonKeyClick = false;
        }

        ////控制主角的朝向
       
        //取得主角在水平方向坐标轴
        float h = Input.GetAxis("Horizontal");
        //获取当前的速度
        Vector3 v = rigidbody.velocity;

        //保存主角当前的速度
        v = rigidbody.velocity;
        //判断主角是否与地面发生碰撞
       
      
        //控制主角的跳跃
        if (isGround &&( Input.GetKeyDown(KeyCode.K) || Jump))
        {
            state=PlayerState.PlayerJump;
            PlayerJump();

        }
       
      
       // 控制主角的跳跃
        if ((Input.GetKeyDown(KeyCode.K) || Jump))
        {
            state = PlayerState.PlayerJump;
            PlayerJump();

        }
        //判断主角当前状态：跳起，蹲下，正常
        if (isGround == false)
        {
            state = PlayerState.PlayerJump;
        }
        else
        {

            if (Down)
            {
                //Down状态中Idle状态
               state = PlayerState.PlayerDown;
               playerDown.status = AnimStatus.Idle;
               //转为跳跃状态
               if (Input.GetKeyDown(KeyCode.K) || Jump)
               {
                   state = PlayerState.PlayerJump;
                   PlayerJump();
                  
               }
                //射击状态
                if (Projectile || Input.GetKey(KeyCode.J))
                {
                    playerDown.status = AnimStatus.DownShot;
                    playerShoot.isToShoot = true;
                    playerShoot.isProjectile = true;
                }
                //down状态下向左行走
                if (LA&&DownToWalk)
                {
                    playerDown.status = AnimStatus.Walk;
                    rigidbody.velocity = new Vector3(-0.7f*m_speed, v.y, v.z);
                    //转为跳跃状态
                    if (Input.GetKeyDown(KeyCode.K) || Jump)
                    {
                        state = PlayerState.PlayerJump;
                        PlayerJump();

                    }
                        if (Projectile || Input.GetKey(KeyCode.J))
                        {
                            playerDown.status = AnimStatus.DownShot;
                            playerShoot.isToShoot = true;
                            playerShoot.isProjectile = true;
                        }
                        if (RA) {
                            rigidbody.velocity = new Vector3(0.7f*m_speed, v.y, v.z);
                            RA = false;
                        }
                        if (Left) {
                            Down = false;
                            playerGround.status = AnimStatus.Walk;
                            Left = false;
                        }
                        if (Right) {
                            Down = false;
                            playerGround.status = AnimStatus.Walk;
                            Right = false;
                        }
                        DownToWalk = false;

                 }
                //down状态下向右行走
                  else if (RA&&DownToWalk)
                    {
                        playerDown.status = AnimStatus.Walk;
                        rigidbody.velocity = new Vector3(0.7f*m_speed, v.y, v.z);
                        //转为跳跃状态
                        if (Input.GetKeyDown(KeyCode.K) || Jump)
                        {
                            state = PlayerState.PlayerJump;
                            PlayerJump();

                        }
                        if (Projectile || Input.GetKey(KeyCode.J))
                        {
                            playerDown.status = AnimStatus.DownShot;
                            playerShoot.isToShoot = true;
                            playerShoot.isProjectile = true;
                        }
                        if (LA)
                        {
                            rigidbody.velocity = new Vector3(-0.7f*m_speed, v.y, v.z);
                            LA = false;
                        }
                        if (Left)
                        {
                            playerGround.status = AnimStatus.Walk;
                            Left = false;
                        }
                        if (Right)
                        {

                            playerGround.status = AnimStatus.Walk;
                            Right = false;
                        }
                        DownToWalk = false;
                    }

                //进入Left状态
                if (Left) {
                    Down = false;
                    Left = false;
                }
                //进入Right状态
                if (Right)
                {
                    Down = false;
                    Right = false;
                } 
                }
            //处于地面状态
            else if (!Down)
            {
                state = PlayerState.PlayerGround;

                playerGround.status = AnimStatus.Idle;
                //转为跳跃状态
               if ( Input.GetKeyDown(KeyCode.K) || Jump)
                {
                    state=PlayerState.PlayerJump;
                    PlayerJump();
              
                }
                //Idle状态下射击
                if ((Projectile || Input.GetKey(KeyCode.J))&&!Left&&!Right)
                {
                    playerGround.status = AnimStatus.WalkShot;
                    playerShoot.isToShoot = true;
                    playerShoot.isProjectile = true;
                }
                //向左行走
                if (Left) {
                    Right = false;
                    playerGround.status = AnimStatus.Walk;
                    rigidbody.velocity = new Vector3(-m_speed, v.y, v.z);

                    //转为跳跃状态
                    if (Input.GetKeyDown(KeyCode.K) || Jump)
                    {
                        state = PlayerState.PlayerJump;
                        PlayerJump();
                        
                    }
                    //向左行走射击
                    if ((Projectile || Input.GetKey(KeyCode.J)))
                    {
                        playerGround.status = AnimStatus.WalkingShot;
                        playerShoot.isToShoot = true;
                        playerShoot.isProjectile = true;
                    }
                    //转向upwalk状态
                    if (RA && UpToWalk)
                    {
                        Left = false;
                        playerGround.status = AnimStatus.UpWalk;
                        UpToWalk = false ;
                    }
                  if (LA && UpToWalk)
                    {
                        Left = false;
                        playerGround.status = AnimStatus.UpWalk;
                        UpToWalk=false;
                    }
                    //转向Up状态
                    if (Top) {
                        Left = false;
                        playerGround.status = AnimStatus.Up; 
                        Top = false;
                    }
                    //进入下蹲状态
                    if (LA && DownToWalk) {
                        Left = false;
                        Down = true;
                        DownToWalk = false;
                    }
                    if (RA && DownToWalk) {
                        Left = false;
                        Down = true;
                        DownToWalk = false;
                    }
                    if (Right)
                    {
                        Left = false;
                        rigidbody.velocity = new Vector3(m_speed, v.y, v.z);
                    }
                }
                //向右行走
                else if (Right) {
                    playerGround.status = AnimStatus.Walk;
                    rigidbody.velocity = new Vector3(m_speed, v.y, v.z);

                    //转为跳跃状态
                    if (Input.GetKeyDown(KeyCode.K) || Jump)
                    {
                        state = PlayerState.PlayerJump;
                        PlayerJump();
                      
                    }
                    //向右行走射击
                    if ((Projectile || Input.GetKey(KeyCode.J)))
                    {
                        playerGround.status = AnimStatus.WalkingShot;
                        playerShoot.isToShoot = true;
                        playerShoot.isProjectile = true;
                    }
                    //转向upwalk状态
                    if (RA && UpToWalk) {
                        Right = false;
                        playerGround.status = AnimStatus.UpWalk;
                        if (Projectile || Input.GetKey(KeyCode.J)) {
                            UpToWalk = false;
                            playerGround.status = AnimStatus.WalkShotUp;
                        }
                        RA = false;
                    }
                    if (LA && UpToWalk) {
                        Right = false;
                        playerGround.status = AnimStatus.UpWalk;
                        if (Projectile || Input.GetKey(KeyCode.J))
                        {
                            UpToWalk = false;
                            playerGround.status = AnimStatus.WalkShotUp;
                        }
                        if (Left) {
                            Right = false;
                            rigidbody.velocity = new Vector3(-m_speed, v.y, v.z);
                        }
                        LA = false;
                    }

                    //转向Up状态
                    if (Top)
                    {
                        Right = false;
                        playerGround.status = AnimStatus.Up;
                        Top = false;
                    }

                    //进入Down状态
                    if (RA&& DownToWalk)
                    {
                        Right = false;
                        Down = true;
                        DownToWalk = false;
                    }
                    if (LA && DownToWalk)
                    {
                        Right = false;
                        Down = true;
                        DownToWalk = false;
                    }
                    
                }
                    //右上行走
               else if (RA && UpToWalk)
                {
                    playerGround.status = AnimStatus.UpWalk;
                    rigidbody.velocity = new Vector3(m_speed, v.y, v.z);
                    //转为跳跃状态
                    if (Input.GetKeyDown(KeyCode.K) || Jump)
                    {
                        state = PlayerState.PlayerJump;
                        PlayerJump();

                    }
                   //右上行走射击
                    if (Projectile || Input.GetKey(KeyCode.J))
                    {
                        playerGround.status = AnimStatus.WalkShotUp;
                        playerShoot.isToShoot = true;
                        playerShoot.isProjectile = true;
                        //射击的时候转向左边
                        if (LA) {
                            rigidbody.velocity = new Vector3(-m_speed, v.y, v.z);
                            LA = false;
                        }
                    }
                   //转向左边
                    if (LA)
                    {
                        rigidbody.velocity = new Vector3(-m_speed, v.y, v.z);
                        LA = false;
                    }
                    //转向右边
                    if (Right)
                    {
                        playerGround.status = AnimStatus.Walk;
                        Right = false;
                    }
                    //转向左边边
                    if (Left)
                    {
                        playerGround.status = AnimStatus.Walk; 
                        Left = false;
                    }
                    UpToWalk = false;

                }
                //左上行走
               else  if (LA && UpToWalk)
                {
                    playerGround.status = AnimStatus.UpWalk;
                    rigidbody.velocity = new Vector3(-m_speed, v.y, v.z);

                    //转为跳跃状态
                    if (Input.GetKeyDown(KeyCode.K) || Jump)
                    {
                        state = PlayerState.PlayerJump;
                        rigidbody.velocity = new Vector3(0.8f * v.x, 1.1f * jumpSpeed, v.z);
                        if (Projectile || Input.GetKey(KeyCode.J))
                        {
                            playerShoot.isToShoot = true;
                            playerShoot.isProjectile = true;
                            playerShoot.isShoot = true;
                        }
                    }
                   //左上行走射击
                    if (Projectile || Input.GetKey(KeyCode.J))
                    {
                        playerGround.status = AnimStatus.WalkShotUp;
                        playerShoot.isToShoot = true;
                        playerShoot.isProjectile = true;
                        //射击的时候转向右边
                        if (RA)
                        {
                            rigidbody.velocity = new Vector3(m_speed, v.y, v.z);
                            RA = false;
                        }
                    }
                   //转向右边
                    if (RA) {
                        rigidbody.velocity = new Vector3(m_speed, v.y, v.z);
                        RA = false;
                    }
                   //转向walk
                    if (Left) {
                        playerGround.status = AnimStatus.Walk;
                        Left = false;
                    }
                    //转向右边
                    if (Right)
                    {
                        playerGround.status = AnimStatus.Walk;
                        Right = false;
                    }
                    UpToWalk = false;
                }
                else if (Top)
                {
                    playerGround.status = AnimStatus.Up;
                    //转为跳跃状态
                    if (Input.GetKeyDown(KeyCode.K) || Jump)
                    {
                        state = PlayerState.PlayerJump;
                        PlayerJump();

                    }
                    //向上射击
                     if (Projectile||Input.GetKey(KeyCode.J)) {
                        playerGround.status = AnimStatus.ShootUp;
                        playerShoot.isToShoot = true;
                        playerShoot.isProjectile = true;
                    }
                    
                }
              }
                //Idle状态
                else
                {
                    playerGround.status = AnimStatus.Idle;
                    playerJump.status = AnimStatus.Idle;
                    playerDown.status = AnimStatus.Idle;

                }
            }

        float x = 1;
        if (rigidbody.velocity.x > 0.05f)
        {
            x = -1;
        }
        else if (rigidbody.velocity.x < -0.05f)
        {
            x = 1;
        }
        else
        {
            x = 0;
        }
        if (x != 0)
        {
            playerGround.transform.localScale = new Vector3(x, 1, 1);
            playerJump.transform.localScale = new Vector3(x, 1, 1);
            playerDown.transform.localScale = new Vector3(x, 1, 1);
        }
        //退出游戏
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //根据状态来判定启用哪一个游戏状态
        switch (state)
        {
            case PlayerState.PlayerDown:
                playerDown.gameObject.SetActive(true);
                playerGround.gameObject.SetActive(false);
                playerJump.gameObject.SetActive(false);
                break;
            case PlayerState.PlayerGround:
                playerDown.gameObject.SetActive(false);
                playerGround.gameObject.SetActive(true);
                playerJump.gameObject.SetActive(false);
                break;
            case PlayerState.PlayerJump:
                playerDown.gameObject.SetActive(false);
                playerGround.gameObject.SetActive(false);
                playerJump.gameObject.SetActive(true);             
                break;

        }
        //RaycastHit hitEnemy;
        //isEnemy = Physics.Raycast(transform.position -new Vector3(3,0,0), Vector3.up, out hitEnemy, 1.2f, default);
        //Debug.DrawLine(transform.position, hitEnemy.point, Color.red, 1);
        //if (isEnemy)
        //{
        //    print("enemy");
        //}
        //else
        //{
        //    isEnemy = false;
        //}
    }
    void FixedUpdate() {
            NormalizeSlope();
            //射线检测与地面的碰撞
            //Vector3 rayOrigin = GetComponent<Collider>().bounds.center;
            float rayDistance = GetComponent<Collider>().bounds.extents.y + 0.5f;
            RaycastHit hitInfo;
            ////isGround = (Physics.Raycast(transform.position + Vector3.down * raySpeedRate+new Vector3(rayx,0,0),
            ////   Vector3.down, out hitInfo, rayLength, groundLayerMask));
            //isGround = (Physics.Raycast(rayOrigin, Vector3.down, out hitInfo, rayDistance, groundLayerMask));
            for (int i = 0; i < rayNumbers; i++)
            {
                float lerpmount = (float)i / (float)0.1 - 1;
                Vector3 origin = Vector3.Lerp(playerJump.transform.position - new Vector3(0.3f, 0, 0),
                    playerJump.transform.position + new Vector3(0.12f, 0, 0), lerpmount);
                Ray ray = new Ray(origin, Vector3.down);
                isGround = Physics.Raycast(ray, out hitInfo, rayDistance, groundLayerMask);
                //Debug.DrawLine(transform.position, hitInfo.point, Color.red, 1);
                if (isGround)
                {
                    break;
                }
            }

           
    }
    void OnTriggerEnter(Collider other)
    {
        //捡到人质的炸弹包，手榴弹数目加1
        if (other.gameObject.tag == "Gift1")
        {
            AudioSource.PlayClipAtPoint(giftSound, new Vector3(other.transform.position.x,
                other.transform.position.y, other.transform.position.z));
            if (DigitDisplay.grenade < 15)
                DigitDisplay.grenade++;
            Destroy(other.gameObject);
        }
        //捡到人质的药包，生命值加1
        if (other.gameObject.tag == "Gift2")
        {
            AudioSource.PlayClipAtPoint(giftSound, new Vector3(other.transform.position.x,
                other.transform.position.y, other.transform.position.z));
            DigitDisplay.life++;
            Destroy(other.gameObject);
        }
        //捡到药丸，生命值加1
        if (other.gameObject.tag == "Pills")
        {
            AudioSource.PlayClipAtPoint(giftSound, new Vector3(other.transform.position.x,
                other.transform.position.y, other.transform.position.z));
            DigitDisplay.life++;
            Destroy(other.gameObject);
        }
        //碰到敌人且敌人1的不处于刺杀状态，敌人消失，分数加100；如果碰到敌人1的刺杀状态，生命值减1
        if (other.gameObject.tag == "Enemy2" || other.gameObject.tag == "Enemy3"
            || other.gameObject.tag == "Enemy1")
        {
           
            if (enemy1T.curState == FSMState.Kill)
            {
                AudioSource.PlayClipAtPoint(playerDie, new Vector3(transform.position.x,
                transform.position.y, transform.position.z));
                if (DigitDisplay.life > 0)
                {
                    DigitDisplay.life--;
                }
            }
            else
            {
                DigitDisplay.score += 100;
            }
          
            //other.gameObject.SetActive(false);
        }
        //碰到坦克的炮弹，生命值减1
        if (other.gameObject.tag == "TankFire")
        {
            AudioSource.PlayClipAtPoint(playerDie, new Vector3(transform.position.x,
             transform.position.y, transform.position.z));
            Destroy(other.gameObject);
            if (DigitDisplay.life > 0)
            {
                DigitDisplay.life--;
            }
            Vector3 v = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);
            Instantiate(tankFireExplosion, v, Quaternion.Euler(new Vector3(0, 0, 0)));

        }
        //碰到敌人2,3的炮弹，飞机以及飞机扔的炸弹，生命值减1
        if (other.gameObject.tag == "Jet" || other.gameObject.tag == "JetBomb")
        {
            AudioSource.PlayClipAtPoint(playerDie, new Vector3(transform.position.x,
              transform.position.y, transform.position.z));
            if (DigitDisplay.life > 0)
            {
                DigitDisplay.life--;
            }
        }

        if (other.gameObject.tag == "Bomb" || other.gameObject.tag == "Enemy3CannoBall")
        {
            Destroy(other.gameObject);
            AudioSource.PlayClipAtPoint(playerDie, new Vector3(transform.position.x,
             transform.position.y, transform.position.z));
            Instantiate(bombExplosion, (transform.position - new Vector3(
                -0.1f, 0.2f, 0)), transform.rotation);
            if (DigitDisplay.life > 0)
            {
                DigitDisplay.life--;
            }
        }
      
      
    }
 
    //void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag=="Ground")
    //    isGround = true;
    //}
 
    //void OnTriggerExit(Collider other) {
    //    if (other.gameObject.tag == "Ground") {
    //        isGround = false;
    //    }
    //}
  
    //IEnumerator KilledForTime()
    //{
    //    yield return new WaitForSeconds(1.5f);
    //    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    //    if (oneTimeSound)
    //    {
    //        //   AudioSource.PlayClipAtPoint(playerDiesound,new Vector3(transform.position.x,0,0));
    //        oneTimeSound = false;
    //    }
    //    killed = false;
    //}


    void NormalizeSlope()
    {
        
        if (isGround)
        {
            RaycastHit hit;
                Physics.Raycast(player.transform.position + Vector3.down * raySpeedRate,
            Vector3.down, out hit, rayLength, groundLayerMask);
                // 垂直正交化
            if (hit.collider != null && Mathf.Abs(hit.normal.x) > 0.02f)
            {
                Rigidbody body = GetComponent<Rigidbody>();
                //通过改变下滑的速度来使人物静止
                body.velocity = new Vector2(body.velocity.x - (hit.normal.x * 0.15f), body.velocity.y);
                //人物向上/向下行走
                Vector3 pos = transform.position;
                pos.y += -hit.normal.x * Mathf.Abs(body.velocity.x) * Time.deltaTime * (
                    body.velocity.x - hit.normal.x > 0 ? 1 : -1);
                transform.position = pos;
            }
           
        }
       
        
    }
    void PlayerJump() {
        float direction = 0.0f;
        if (isGround) {
            if (Left || LA)
            {
                direction = -1.0f;
            }
            else if (Right || RA)
            {
                direction = 1.0f;
            }
            else
            {
                direction = 0.0f;
            }
            if (Application.loadedLevelName == "play2")
            {
                rigidbody.AddForce(new Vector2(direction * 20f, 250f));
            }
            else if (Application.loadedLevelName == "play5") {
                rigidbody.AddForce(new Vector2(direction * 30f, 320f));
            }
            if (Projectile || Input.GetKey(KeyCode.J))
            {
                playerShoot.isToShoot = true;
                playerShoot.isProjectile = true;
                playerShoot.isShoot = true;
            }
            Jump = false;
        }
        isGround = false;
    }
    //摇杆控制
    void OnEnable()
    {
        EasyJoystick.On_JoystickMove += On_JoystickMove;
        EasyJoystick.On_JoystickMoveEnd += On_JoystickMoveEnd;
        EasyJoystick.On_JoystickMoveStart += On_JoysticMoveStart;
        EasyJoystick.On_JoystickTouchUp += On_JoystickTouchUp;
        EasyJoystick.On_JoystickTouchStart += On_JoystickTouchStart;
    }
    void OnDisable()
    {
        EasyJoystick.On_JoystickMove -= On_JoystickMove;
        EasyJoystick.On_JoystickMoveEnd -= On_JoystickMoveEnd;
        EasyJoystick.On_JoystickMoveStart -= On_JoysticMoveStart;
        EasyJoystick.On_JoystickTouchUp -= On_JoystickTouchUp;
        EasyJoystick.On_JoystickTouchStart -= On_JoystickTouchStart;
    }
    void OnDestroy()
    {
        EasyJoystick.On_JoystickMove -= On_JoystickMove;
        EasyJoystick.On_JoystickMoveEnd -= On_JoystickMoveEnd;
        EasyJoystick.On_JoystickMoveStart -= On_JoysticMoveStart;
        EasyJoystick.On_JoystickTouchUp -= On_JoystickTouchUp;
        EasyJoystick.On_JoystickTouchStart -= On_JoystickTouchStart;

    }
    void On_JoystickTouchStart(MovingJoystick move) {
        move.joystick.touchSize = 55;
        
    }
    void On_JoystickTouchUp(MovingJoystick move)
    {
      
        move.joystick.touchSize = 40;
    }
    void On_JoysticMoveStart(MovingJoystick move)
    {
     
    }
   
    void On_JoystickMove(MovingJoystick move)
    {
        
        //判断触摸的方向键
        float currentAngle = CalculaAngle(move.joystickAxis.x, move.joystickAxis.y);
        if (currentAngle <= 32.5f && currentAngle >= 0f || currentAngle <= 360f && currentAngle >= 327.5f) {
           // print("A");
            Left = true;
            
        }
        else if (currentAngle <= 67.5f && currentAngle >= 32.5f) {
           // print("WA");
            LA = true;
            UpToWalk = true;

        }
        else if (currentAngle <= 112.5 && currentAngle >= 67.5f) {
           // print("W");
           Top = true;
           
        }
        else if (currentAngle <= 147.5f && currentAngle >= 112.5f)
        {
            // print("WD");
            RA = true;
            UpToWalk = true;
        }
       
        else if (currentAngle <= 212.5f && currentAngle >= 147.5f)
        {
           // print("D");
            Right = true;
        }
        else if (currentAngle <= 247.5f && currentAngle >= 202.5f)
        {
            // print("SD");
            RA = true;
            DownToWalk = true;
        }
        else if (currentAngle <= 292.5f && currentAngle >= 247.5f)
        {
           // print("S");
            Down = true;
        }
        else if (currentAngle <= 337.5f && currentAngle >= 292.5f)
        {
           // print("SA");
            LA = true;
            DownToWalk = true;
        }
      


    }
    private float CalculaAngle(float _joyPositionX, float _joyPositionY)
    {
        float currentAngleX = _joyPositionX * 90f + 90f;
        float currentAngleY = _joyPositionY * 90f + 90f;
        if (currentAngleY < 90f)
        {
            if (currentAngleX < 90)
            {
                return 270f + currentAngleY;
            }
            else if (currentAngleX > 90)
            {
                return 180f + (90f - currentAngleY);
            }
        }

        return currentAngleX;
    }
    void On_TouchUp()
    {
    }

    void On_JoystickMoveEnd(MovingJoystick move)
    {

        Down = false;
        Top = false;
        Left = false;
        Right = false;
        UpToShot = false;
        UpToWalk = false;
        DownToWalk = false;
        DownToShot = false;
        walkshotupstate = false;
        LA = false;
        RA = false;
    }

    void ThrowGrenadeStart()
    {
        Grenade = true;
    }
    void ThrowGrenadeEnd() {
        Grenade = false;
    }
    void ShootProjectile()
    {
        Projectile = true;
    }
    void ShootProjectileUp()
    {
        Projectile = false;
    }
    void JumpDown()
    {
        Jump = true;
    }
    void JumpUp() {
        Jump = false;
    }
   
}



