using UnityEngine;
using System.Collections;
public enum AnimStatus { 
        Idle,       //静止状态
        Walk,       //行走状态

        Up,         //向上状态
        UpWalk,     //向上行走状态
        ShootUp,    //向上射击状态
        ThrewUp,      //扔手榴弹状态
        ThrewDown,      //蹲下扔手榴弹
        WalkShot,   //行走时射击状态
        WalkingShot,    //行走射击
        WalkShotUp, //向上行走射击
        DownShot,   //蹲下射击

};
public class PlayerGround : MonoBehaviour {
    private float timer = 0;
    public float m_x = 0;
    public float m_y = 0;
    public float animSpeed = 10;//1秒播放10帧图片
    private float animTimeInterval = 0;

    public float walkSpeed = 30;
    private float walkAnimTimeInterval = 0;

    //walk状态下的动画速度
    private float walkAnimSpeed = 25f;
    public AnimStatus status = AnimStatus.Idle;//表示主角当前的状态
    //Sprite渲染器
    public SpriteRenderer upRenderer;
    public SpriteRenderer downRenderer;

    //Idle状态上半身渲染
    public Sprite[] idleUpSpriteArray;
    private int idleUpIndex = 0;
    private int idleUpLength = 0;
    private float idleUpTimer = 0;
    //下半身
    public Sprite idleDownSprite;
    
    //在walk状态下,上半身动作
    public Sprite[] walkUpSpriteArray;
    private int walkUpIndex = 0;
    private int walkUpLength = 0;
    private float walkUpTimer = 0;
    //walk下半身动作
    public Sprite[] walkDownSpriteArray;
    private int walkDownIndex = 0;
    private int walkDownLength = 0;
    private float walkDownTimer = 0;


    //在Up状态下上半身动作
    public Sprite[] upSpriteArray;
    private int upIndwx = 0;
    private int upLength = 0;
    private float upTimer = 0;
    //下半身动作
    public Sprite[] upDSpriteArray;
    private int updIndex = 0;
    private int updLength = 0;
    private float updTimer = 0;

    //在Up状态下，行走的下半身动作
    public Sprite[] upWalkdownSpriteArray;
    private int upWalkdownIndex = 0;
    private int upWalkdownLength = 0;
    private float upWalkdownTimer = 0;

    //在向上射击状态下,上半身动作
    private float shotUpAnimSpeed = 35;
    private float shotUpAnimTimeInterval = 0;
    public Sprite[] shotUpSpriteArray;
    private int shotUpIndex = 0;
    private int shotUpLength = 0;
    private float shotUpTimer = 0;
    //walk下半身动作
    public Sprite shotUpdownsSprite;

    //walk状态向上射击走动
    private float walkShotUpAnimSpeed = 35;
    private float walkShotUpTimeInterval = 0;
    public Sprite[] walkShotUpSpriteArray;
    private int walkShotUpIndex = 0;
    private int walkShotUpLength = 0;
    private float walkShotUpTimer = 0;
    //下半身动作
    private float walkshotdownAnimSpeed =30;
    private float walkshotdownAnimInterval = 0;

    public Sprite[] walkShotUpdownSpriteArray;
    private int walkShotUpdownIndex = 0;
    private int walkShotUpdownLength = 0;
    private float walkShotUpdownTimer = 0;

    //walking状态下射击上半身
    private float walkingshotupAnimSpeed = 20f;
    private float walkingshotupTimeInterval = 0;

    public Sprite[] walkingShotupSpriteArray;
    private int walkingShotupLength = 0;
    private int walkingShotupIndex=0;
    private float walkingShotupTimer = 0;
    
    //行走时射击下半身的动画速度
    private float walkingshotdownAnimSpeed = 30;
    private float walkingshotdownAnimInterval = 0;

    public Sprite[] walkingShotdownSpriteArray;
    private int walkingShotdownLength = 0;
    private int walkingShotdownIndex = 0;
    private float walkingShotdownTimer = 0;

    //扔手榴弹的动作上半身
    private float threwAnimSpeed = 10;
    private float threwAnimTimeInterval = 0;
    public Sprite[] threwUpSpriteArray;
    private int threwUpIndex = 0;
    private int threwUpLength = 0;
    private float threwUpTimer = 0;
  
    //下半身动作
    public Sprite[] threwDownSpriteArray;
    private int threwDownIndex = 0;
    private int threwDownLength = 0;
    private float threwDownTimer = 0;

    public Sprite shotUpSprite;
    public Sprite shootHorizontalSprite;

    private bool shoot = false;
    private ShootDir shootDir;

    public GameObject projectilePrefab;
    public Transform shootuppos;
    public Transform shootHorzontailpos;
    public GameObject startPrefab;

    ////扔手榴弹的时候动画渲染
    //public Sprite throwUpSprite;
    //public Sprite throwHorizontalSprite;
    //判断是否该扔
    private bool throwing = false;
    private ThrowDir throwDir;
    //手榴弹预置体
    public GameObject grenadePrefab;
    public Transform throwUpPos;
    public Transform throwHorizontalPos;
    private GameObject Grenade;
    public PlayerController playerController;

    private bool isShotup = false;


   //静止射击时的动作切换
    private float idleAnimSpeed = 28;
    private float idleAnimTimeInterval = 0;
    public Sprite[] idleshootupSpriteArray;
    private int idleshootupIndex = 0;
    private int idleshootupLength = 0;
    private float idleshootupTimer = 0;

    public Sprite[] idledownSpriteArray;
    private int idledownIndex = 0;
    private int idledownLength = 0;
    private float idledownTimer = 0;

    private float mspeed=20;
    private float intervaltimer = 0;
    private int indexs = 0;
    public Sprite throwHorizontalSprite;
    
	void Start () {
        animTimeInterval = 1 / animSpeed;//得到每一帧的时间间隔
        walkAnimTimeInterval = 1 / walkAnimSpeed;
        idleAnimTimeInterval = 1 / idleAnimSpeed;
        threwAnimTimeInterval = 1 / threwAnimSpeed;
        shotUpAnimTimeInterval = 1 / shotUpAnimSpeed;
        walkShotUpTimeInterval = 1 / walkShotUpAnimSpeed;

        idleUpLength = idleUpSpriteArray.Length;
        walkUpLength = walkUpSpriteArray.Length;
        walkDownLength = walkDownSpriteArray.Length;
        upLength = upSpriteArray.Length;
        updLength = upDSpriteArray.Length;

        shotUpLength = shotUpSpriteArray.Length;
        idleshootupLength = idleshootupSpriteArray.Length;
        walkShotUpLength = walkShotUpSpriteArray.Length;
        walkShotUpdownLength = walkShotUpdownSpriteArray.Length;
        walkshotdownAnimInterval = 1 / walkshotdownAnimSpeed;

        walkingShotupLength = walkingShotupSpriteArray.Length;
        walkingShotdownLength = walkingShotdownSpriteArray.Length;

        walkingshotupTimeInterval = 1 / walkingshotupAnimSpeed;
        walkingshotdownAnimInterval = 1 / walkingshotdownAnimSpeed;

        upWalkdownLength = upWalkdownSpriteArray.Length;
        //test
        idleshootupLength = idleshootupSpriteArray.Length;
        idledownLength = idledownSpriteArray.Length;
        //扔手榴弹
        threwUpLength = threwUpSpriteArray.Length;
        threwDownLength = threwDownSpriteArray.Length;
	}
	
	// Update is called once per frame
	void Update () {
        
        switch(status){
            case AnimStatus.Idle:
                idleUpTimer += Time.deltaTime;
                ////播放下一帧
                if (idleUpTimer > animTimeInterval)
                {
                    //当前计时器减去下一帧的时间间隔
                    idleUpTimer -= animTimeInterval;
                    //当前帧数加1
                    idleUpIndex++;
                    //判断是否达到最大帧数
                    idleUpIndex %= idleUpLength;
                    upRenderer.sprite = idleUpSpriteArray[idleUpIndex];
                }
               downRenderer.sprite = idleDownSprite;
                break;
            case AnimStatus.Walk:
                walkUpTimer += Time.deltaTime;
                walkDownTimer += Time.deltaTime;
                //播放下一帧
                if (walkUpTimer > walkAnimTimeInterval) { 
                    //当前计时器减去下一帧的时间间隔
                    walkUpTimer -= walkAnimTimeInterval;
                    walkUpIndex++;
                    walkUpIndex %= walkUpLength;
                    upRenderer.sprite=walkUpSpriteArray[walkUpIndex];
                }
                if (walkDownTimer > animTimeInterval)
                {
                    //当前计时器减去下一帧的时间间隔
                    walkDownTimer -= walkAnimTimeInterval;
                    walkDownIndex++;
                    walkDownIndex %= walkDownLength;
                    downRenderer.sprite = walkDownSpriteArray[walkDownIndex];
                }
                break;
            case AnimStatus.Up:
                upTimer += Time.deltaTime;
                updTimer += Time.deltaTime;
                if (upTimer > animTimeInterval) {
                    upTimer -= animTimeInterval;
                    upIndwx++;
                    upIndwx %= upLength;
                    upRenderer.sprite=upSpriteArray[upIndwx];
                }
               
                if (updTimer > animTimeInterval) {
                    updIndex++;
                    updIndex %= updLength;
                    downRenderer.sprite=upDSpriteArray[updIndex];
                }
                break;
            case AnimStatus.UpWalk:
                upTimer += Time.deltaTime;
                upWalkdownTimer += Time.deltaTime;
                if (upTimer > animTimeInterval) {
                    upTimer -= animTimeInterval;
                    upIndwx++;
                    upIndwx %= upLength;
                    upRenderer.sprite=upSpriteArray[upIndwx];
                }
                if (upWalkdownTimer > animTimeInterval) {
                    upWalkdownTimer -= animTimeInterval;
                    upWalkdownIndex++;
                    upWalkdownIndex %= upWalkdownLength;
                    downRenderer.sprite=upWalkdownSpriteArray[upWalkdownIndex];
                }
                break;
            case AnimStatus.ShootUp:
                 shotUpTimer += Time.deltaTime;
                //播放下一帧
                if (shotUpTimer > shotUpAnimTimeInterval)
                {
                    //当前计时器减去下一帧的时间间隔
                    shotUpTimer -= shotUpAnimTimeInterval;
                    shotUpIndex++;
                    shotUpIndex %= shotUpLength;
                    upRenderer.sprite = shotUpSpriteArray[shotUpIndex];
                }
                downRenderer.sprite = shotUpdownsSprite;
                break;
            case AnimStatus.WalkShotUp:
                walkShotUpTimer += Time.deltaTime;
                walkShotUpdownTimer += Time.deltaTime;
                if (walkShotUpTimer > walkShotUpTimeInterval)
                {
                    walkShotUpTimer -= walkShotUpTimeInterval;
                    walkShotUpIndex++;
                    walkShotUpIndex %= walkShotUpLength;
                    upRenderer.sprite = walkShotUpSpriteArray[walkShotUpIndex];
                }
                if (walkShotUpdownTimer > walkshotdownAnimInterval)
                {
                    walkShotUpdownTimer -= walkshotdownAnimInterval;
                    walkShotUpdownIndex++;
                    walkShotUpdownIndex %= walkShotUpdownLength;
                   downRenderer.sprite=walkShotUpdownSpriteArray[walkShotUpdownIndex];
                }
                break;
            case AnimStatus.WalkingShot:
                walkingShotupTimer += Time.deltaTime;
                walkingShotdownTimer += Time.deltaTime;
                if (walkingShotupTimer > walkingshotupTimeInterval)
                {
                    walkingShotupTimer -= walkingshotupTimeInterval;
                    walkingShotupIndex++;
                    walkingShotupIndex %= walkingShotupLength;
                    upRenderer.sprite=walkingShotupSpriteArray[walkingShotupIndex];
                }
                if (walkingShotdownTimer > walkingshotdownAnimInterval)
                {
                    walkingShotdownTimer -= walkingshotdownAnimInterval;
                    walkingShotdownIndex++;
                    walkingShotdownIndex %= walkingShotdownLength;
                    downRenderer.sprite=walkingShotdownSpriteArray[walkingShotdownIndex];
                }
                break;
            case AnimStatus.ThrewUp:
                threwUpTimer += Time.deltaTime;
                 threwDownTimer += Time.deltaTime;
                 if (threwUpTimer > threwAnimTimeInterval) {
                     threwUpTimer -= threwAnimTimeInterval;
                     threwUpIndex++;
                     threwUpIndex %= threwUpLength;
                     upRenderer.sprite = threwUpSpriteArray[threwUpIndex];
                 }
               
                //播放下一帧
                 if (threwDownTimer > threwAnimTimeInterval)
                {
                    //当前计时器减去下一帧的时间间隔
                    threwDownTimer -= animTimeInterval;
                    threwDownIndex++;
                    threwDownIndex %= threwDownLength;
                    downRenderer.sprite = threwDownSpriteArray[threwDownIndex];
                }
                
                break;
            case AnimStatus.WalkShot:
                    idleshootupTimer += Time.deltaTime;
                    idledownTimer += Time.deltaTime;
                    if (idleshootupTimer > idleAnimTimeInterval)
                    {
                        idleshootupTimer -= idleAnimTimeInterval;
                        idleshootupIndex++;
                        idleshootupIndex %= idleshootupLength;
                        upRenderer.sprite=idleshootupSpriteArray[idleshootupIndex];
                    }
                    if (idledownTimer > idleAnimTimeInterval)
                    {
                        idledownTimer -= idleAnimTimeInterval;
                        idledownIndex++;
                        idledownIndex %= idledownLength;
                        downRenderer.sprite=idledownSpriteArray[idledownIndex];
                    }
                break;

        }
      
	   
	}
    
    void LateUpdate()
    {
        timer += Time.deltaTime;
        if (shoot)
        {
            shoot = false;
            //进行射击
            Vector3 pos = Vector3.zero;
            if (shootDir == ShootDir.Top)
            {
                pos = shootuppos.position;
            }else if (shootDir == ShootDir.Left || shootDir == ShootDir.Right)
            {
                pos = shootHorzontailpos.position;
                if (playerController.Right) {
                    Vector3 v= new Vector3(shootHorzontailpos.position.x+0.1f,shootHorzontailpos.position.y,shootHorzontailpos.position.z);
                    pos =v;
                }else if(playerController.Left){
                    Vector3 v = new Vector3(shootHorzontailpos.position.x - 0.08f, shootHorzontailpos.position.y-0.1f, shootHorzontailpos.position.z);
                    pos = v;
                }
            }
            //判断子弹的射击的方向
            int z_rotation = 0;
            switch (shootDir)
            {
                case ShootDir.Left:
                    z_rotation = 180;
                    break;
                case ShootDir.Right:
                    z_rotation = 0;
                    break;
                case ShootDir.Top:
                    z_rotation = 90;
                    break;
                case ShootDir.Down:
                    z_rotation = 270;
                    break;
                default:
                    break;
            }
            if (timer > 0.2f) {
                Instantiate(projectilePrefab, pos, Quaternion.Euler(0, 0, z_rotation));
                Instantiate(startPrefab, pos, Quaternion.Euler(0, 0, z_rotation));
                timer = 0;
            }
              
        
           

        }
        if (throwing)
        {
            throwing = false;
            //扔手榴弹
            Vector3 pos = Vector3.zero;
            if (throwDir == ThrowDir.Top)
            {
                pos = throwUpPos.position;
            }
            else if (throwDir == ThrowDir.Left || throwDir == ThrowDir.Right)
            {
                pos = throwHorizontalPos.position;
            }
            //判断扔手榴弹的方向
            int t_rotation = 0;
            switch (throwDir)
            {
                case ThrowDir.Left:
                   
                    upRenderer.sprite = throwHorizontalSprite;
                    if (DigitDisplay.grenade >0)
                    {
                        Grenade = Instantiate(grenadePrefab, pos, Quaternion.Euler(0, 0, t_rotation)) as GameObject;
                        DigitDisplay.grenade--;
                        Grenade.rigidbody.velocity = new Vector3(m_x, m_y, 0.0f);
                    }
                    t_rotation = 180;
                    if (Grenade != null)
                    {
                        Grenade.transform.localEulerAngles = new Vector3(0.0f, 0.0f, Random.Range(10, 80));
                    }
                    break;
                case ThrowDir.Right:
                    upRenderer.sprite = throwHorizontalSprite;
                    if (DigitDisplay.grenade > 0) {
                        Grenade = Instantiate(grenadePrefab, pos, Quaternion.Euler(0, 0, t_rotation)) as GameObject;
                        DigitDisplay.grenade--;
                        Grenade.rigidbody.velocity = new Vector3(-m_x, m_y, 0.0f);
                    }
                   
                   
                    t_rotation = 0;
                    if (Grenade != null)
                    {
                        Grenade.transform.localEulerAngles = new Vector3(0.0f, 0.0f, Random.Range(10, 80));
                    }
                    break;
                case ThrowDir.Top:
                    upRenderer.sprite = throwHorizontalSprite;
                    if (DigitDisplay.grenade > 0)
                    {
                        Grenade = Instantiate(grenadePrefab, pos, Quaternion.Euler(0, 0, t_rotation)) as GameObject;
                        DigitDisplay.grenade--;
                        if (transform.localScale.x == 1)
                            Grenade.rigidbody.velocity = new Vector3(m_x, m_y, 0.0f);
                        else {
                            Grenade.rigidbody.velocity = new Vector3(-m_x, m_y, 0.0f);
                        }
                    }
                    t_rotation = 90;
                    if (Grenade != null)
                    {
                        Grenade.transform.localEulerAngles = new Vector3(0.0f, 0.0f, Random.Range(10, 80));
                    }
                    break;
                case ThrowDir.Down:
                  //  upRenderer.sprite = throwUpSprite;
                    t_rotation = 270;
                    break;
                default:
                    break;
                   
            }

        }
    }
    public void Shoot(float v_h, bool isTopKeyDown, bool isBottomKeyDown)
    {
        shoot = true;
        //得到射击的方向
        //地面状态射击
        if (isTopKeyDown == false && isBottomKeyDown == false) {
            if (playerController.Left)
            {
                shootDir = ShootDir.Left;

            }
            else if (playerController.Right)
            {
                shootDir = ShootDir.Right;
            }
            else if((playerController.LA && playerController.UpToWalk) || (playerController.RA && playerController.UpToWalk)){
                 shootDir = ShootDir.Top;
            }
            else{
                 if (this.transform.localScale.x == 1)
                {
                    shootDir = ShootDir.Left;
                }
                 else if (this.transform.localScale.x == -1)
                {
                    shootDir = ShootDir.Right;
                }
            }
        }
        if (isTopKeyDown)
        {
            shootDir = ShootDir.Top;

        }
        if(isBottomKeyDown){
         if (this.transform.localScale.x == 1)
            {
                shootDir = ShootDir.Left;
            }
            else if (this.transform.localScale.x == -1)
            {
                shootDir = ShootDir.Right;
            }
        }
    }
    public void Throw(float v_h, bool isTopKeyDown, bool isBottomKeyDown)
    {
        throwing = true;
        //获得扔手榴弹的方向
        if (isTopKeyDown == false && isBottomKeyDown == false)
        {
            if (transform.localScale.x == 1)
            {
                throwDir = ThrowDir.Left;
            }
            else if (transform.localScale.x == -1)
            {
                throwDir = ThrowDir.Right;
            }
        }
        else
        {
            if (isTopKeyDown )
            {
                throwDir = ThrowDir.Top;
            }
            else if (isBottomKeyDown)
            {
                throwDir = ThrowDir.Down;
            }
        }
    }
}
