using UnityEngine;
using System.Collections;

public class PlayerDown : MonoBehaviour {

    public float m_x = 0;
    public float m_y = 0;
    public float animSpeed = 10;//1秒播放10帧图片
    private float animTimeInterval = 0;
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

    //walking状态下射击上半身
    public Sprite[] walkingShotupSpriteArray;
    private int walkingShotupLength = 0;
    private int walkingShotupIndex = 0;
    private float walkingShotupTimer = 0;

    public Sprite[] walkingShotdownSpriteArray;
    private int walkingShotdownLength = 0;
    private int walkingShotdownIndex = 0;
    private float walkingShotdownTimer = 0;

    //Down状态下上半身动作
    public Sprite[] downUpSpriteArray;
    private int downUpIndex = 0;
    private int downUpLength = 0;
    private float downUpTimer = 0;
    //下半身动作
    public Sprite downSprite;

    //下蹲射击状态上半身
    private float downIdleAnimSpeed = 10;
    private float downIdleAnimInterval = 0;
    public Sprite[] downIdleUpSpriteArray;
    private int downIdleUpIndex = 0;
    private int downIdleUpLength = 0;
    private float downIdleUpTimer = 0;
    //下蹲射击状态下半身
    public Sprite[] downIdledownSpriteArray;
    private int downIdledownIndex = 0;
    private int downIdledownLength = 0;
    private float downIdleDownTimer = 0;

   

    public Sprite shotUpSprite;
    public Sprite shootHorizontalSprite;

    private bool shoot = false;
    private ShootDir shootDir;

    public GameObject projectilePrefab;
    public Transform shootuppos;
    public Transform shootHorzontailpos;


    //扔手榴弹的时候动画渲染
    public Sprite throwUpSprite;
    public Sprite throwHorizontalSprite;
    //判断是否该扔
    private bool throwing = false;
    private ThrowDir throwDir;
    //手榴弹预置体
    public GameObject grenadePrefab;
    public Transform throwUpPos;
    public Transform throwHorizontalPos;
    private GameObject Grenade;
    public GameObject startPrefab;
    public PlayerController playerController;
    private float timer = 0;
    void Start()
    {
        animTimeInterval = 1 / animSpeed;//得到每一帧的时间间隔
        idleUpLength = idleUpSpriteArray.Length;

        walkUpLength = walkUpSpriteArray.Length;
        walkDownLength = walkDownSpriteArray.Length;
        downUpLength = downUpSpriteArray.Length;

        walkingShotupLength = walkingShotupSpriteArray.Length;
        walkingShotdownLength = walkingShotdownSpriteArray.Length;


        downIdleAnimInterval = 1 / downIdleAnimSpeed;
        downIdleUpLength = downIdleUpSpriteArray.Length;
        downIdledownLength= downIdledownSpriteArray.Length;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        switch (status)
        {
            case AnimStatus.Idle:
                idleUpTimer += Time.deltaTime;
                //播放下一帧
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
                if (walkUpTimer > animTimeInterval)
                {
                    //当前计时器减去下一帧的时间间隔
                    walkUpTimer -= animTimeInterval;
                    walkUpIndex++;
                    walkUpIndex %= walkUpLength;
                    upRenderer.sprite = walkUpSpriteArray[walkUpIndex];
                }
                if (walkDownTimer > animTimeInterval)
                {
                    //当前计时器减去下一帧的时间间隔
                    walkDownTimer -= animTimeInterval;
                    walkDownIndex++;
                    walkDownIndex %= walkDownLength;
                    downRenderer.sprite = walkDownSpriteArray[walkDownIndex];
                }
                break;
            case AnimStatus.DownShot:
                downIdleUpTimer += Time.deltaTime;
                downIdleDownTimer += Time.deltaTime;
                if (downIdleUpTimer > downIdleAnimInterval) {
                    downIdleUpTimer -= downIdleAnimInterval;
                    downIdleUpIndex++;
                    downIdleUpIndex %= downIdleUpLength;
                    upRenderer.sprite=downIdleUpSpriteArray[downIdleUpIndex];
                }
                if (downIdleDownTimer > downIdleAnimInterval) {
                    downIdleDownTimer -= downIdleAnimInterval;
                    downIdledownIndex++;
                    downIdledownIndex %= downIdledownLength;
                    downRenderer.sprite=downIdledownSpriteArray[downIdledownIndex];
                }
            
                break;
            case AnimStatus.WalkingShot:
                walkingShotupTimer += Time.deltaTime;
                walkingShotdownTimer += Time.deltaTime;
                if (walkingShotupTimer > animTimeInterval)
                {
                    walkingShotupTimer -= animTimeInterval;
                    walkingShotupIndex++;
                    walkingShotupIndex %= walkingShotupLength;
                    upRenderer.sprite = walkingShotupSpriteArray[walkingShotupIndex];
                }
                if (walkingShotdownTimer > animTimeInterval)
                {
                    walkingShotdownTimer -= animTimeInterval;
                    walkingShotdownIndex++;
                    walkingShotdownIndex %= walkingShotdownLength;
                    downRenderer.sprite = walkingShotdownSpriteArray[walkingShotdownIndex];
                }
                break;
        }

    }
    void LateUpdate()
    {
        if (shoot)
        {
            shoot = false;
            //进行射击
            Vector3 pos = Vector3.zero;
            if (shootDir == ShootDir.Top)
            {
                pos = shootuppos.position;
            }
            else if (shootDir == ShootDir.Left || shootDir == ShootDir.Right)
            {
                pos = shootHorzontailpos.position;
                 if (playerController.RA)
                {
                    Vector3 v = new Vector3(shootHorzontailpos.position.x+0.05f,shootHorzontailpos.position.y-0.05f,
                        shootHorzontailpos.position.z);
                    pos = v;
                }
                 else if (playerController.RA) {
                     Vector3 v = new Vector3(shootHorzontailpos.position.x - 0.05f, shootHorzontailpos.position.y - 0.05f,
                         shootHorzontailpos.position.z);
                     pos = v;
                 }
            }
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
                    break;
                default:
                    break;
            }
            if (timer > 0.5f) {
                GameObject.Instantiate(projectilePrefab, pos, Quaternion.Euler(0, 0, z_rotation));
                GameObject.Instantiate(startPrefab, pos, Quaternion.Euler(0, 0, z_rotation));
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
                    if (DigitDisplay.grenade > 0)
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
                    if (DigitDisplay.grenade > 0)
                    {
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
                    upRenderer.sprite = throwUpSprite;
                    t_rotation = 90;
                    break;
                case ThrowDir.Down:
                    upRenderer.sprite = throwUpSprite;
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
        if (isBottomKeyDown) {
            if (transform.localScale.x == 1)
            {
                shootDir = ShootDir.Left;
            }
            else if (transform.localScale.x == -1)
            {
                shootDir = ShootDir.Right;
            }
            if (isTopKeyDown)
            {
                shootDir = ShootDir.Top;
            }
        }
        if (isTopKeyDown == false && isBottomKeyDown == false)
        {
            if (playerController.LA||playerController.RA) {
                shootDir = ShootDir.Top;
            }
            else if (transform.localScale.x == 1)
            {
                shootDir = ShootDir.Left;
            }
            else if (transform.localScale.x == -1)
            {
                shootDir = ShootDir.Right;
            }
        }
        //else
        //{
        //    if (isTopKeyDown)
        //    {
        //        shootDir = ShootDir.Top;
        //    }
        //    else if (isBottomKeyDown)
        //    {
        //        shootDir = ShootDir.Down;
        //    }
        //}
    }
    public void Throw(float v_h, bool isTopKeyDown, bool isBottomKeyDown)
    {
        throwing = true;
        //获得扔手榴弹的方向

        //得到射击的方向
        if (isBottomKeyDown)
        {
            if (transform.localScale.x == 1)
            {
                throwDir = ThrowDir.Left;
            }
            else if (transform.localScale.x == -1)
            {
                throwDir = ThrowDir.Right;
            }
            if (isTopKeyDown)
            {
                throwDir = ThrowDir.Top;
            }
        }
        //if (isTopKeyDown == false && isBottomKeyDown == false)
        //{
        //    if (transform.localScale.x == 1)
        //    {
        //        throwDir = ThrowDir.Left;
        //    }
        //    else if (transform.localScale.x == -1)
        //    {
        //        throwDir = ThrowDir.Right;
        //    }
        //}
        //else
        //{
        //    if (isTopKeyDown)
        //    {
        //        throwDir = ThrowDir.Top;
        //    }
        //    else if (isBottomKeyDown)
        //    {
        //        throwDir = ThrowDir.Down;
        //    }
        //}
    }
    
}
