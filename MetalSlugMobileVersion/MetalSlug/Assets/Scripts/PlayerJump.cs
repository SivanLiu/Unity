using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour {

    public float initialV = 20;
    public float m_x =0;
    public float m_y =0;

    public float animUpSpeed = 30;//1秒播放10帧图片
    private float animUpTimeInterval = 0;

    public float animDownSpeed = 40;
    private float animDownTimeInterval = 0;

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
    public Sprite[] idleDownSpriteArray;
    private int idleDownIndex = 0;
    private int idleDownLength = 0;
    private float idleDownTimer = 0;

    //射击时的动画渲染
    public Sprite shotUpSprite;
    public Sprite shootHorizontalSprite;
    //判断是否射击
    private bool shoot = false;
    private ShootDir shootDir;
    //子弹预置体
    public GameObject projectilePrefab;
    public Transform shootuppos;
    public Transform shootHorzontailpos;

    //扔手榴弹的时候动画渲染
    public Sprite throwUpSprite;
    public Sprite throwHorizontalSprite;
    //判断是否该扔
    private bool throwing=false;
    private ThrowDir throwDir;
    //手榴弹预置体
    public GameObject grenadePrefab;
    public Transform throwUpPos;
    public Transform throwHorizontalPos;
    private GameObject Grenade;
    public GameObject startPrefab;
    private float timer = 0;
    void Start()
    {
        animUpTimeInterval = 1 / animUpSpeed;//得到每一帧的时间间隔
        animDownTimeInterval = 1 / animDownSpeed;
        idleUpLength = idleUpSpriteArray.Length;
        idleDownLength = idleDownSpriteArray.Length;
       
    }

    // Update is called once per frame
    void Update()
    {
        switch (status)
        {
            case AnimStatus.Idle:
                idleUpTimer += Time.deltaTime;
                idleDownTimer += Time.deltaTime;
                //播放下一帧
                if (idleUpTimer > animUpTimeInterval)
                {
                    //当前计时器减去下一帧的时间间隔
                    idleUpTimer -= animUpTimeInterval;
                    //当前帧数加1
                    idleUpIndex++;
                    //判断是否达到最大帧数
                    idleUpIndex %= idleUpLength;
                    upRenderer.sprite = idleUpSpriteArray[idleUpIndex];
                }
                if (idleDownTimer > animDownTimeInterval)
                {
                    //当前计时器减去下一帧的时间间隔
                    idleDownTimer -= animDownTimeInterval;
                    //当前帧数加1
                    idleDownIndex++;
                    //判断是否达到最大帧数
                    idleDownIndex %= idleDownLength;
                    downRenderer.sprite = idleDownSpriteArray[idleDownIndex];
                   
                }
                break;
        }

    }
    void LateUpdate() {
        timer += Time.deltaTime;
        if (shoot) {
            shoot = false;
            //进行射击
            Vector3 pos = Vector3.zero;
            if (shootDir == ShootDir.Top) {
                pos = shootuppos.position;
            }else if (shootDir == ShootDir.Left || shootDir == ShootDir.Right) {
                pos = shootHorzontailpos.position;
            }
            //判断子弹的射击的方向
            int z_rotation=0;
            switch (shootDir)
	            {
                    case ShootDir.Left:
                        upRenderer.sprite = shootHorizontalSprite;
                        z_rotation=180;
                        break;
                    case ShootDir.Right:
                        upRenderer.sprite = shootHorizontalSprite;
                         z_rotation=0;
                        break;
                    case ShootDir.Top:
                        upRenderer.sprite = shotUpSprite;
                        z_rotation=90;
                        break;
                    case ShootDir.Down:
                        z_rotation=270;
                        break;
                    default:
                        break;
	            }
            if (timer > 0.4f) {
                GameObject.Instantiate(projectilePrefab, pos, Quaternion.Euler(0, 0, z_rotation));
                GameObject.Instantiate(startPrefab, pos, Quaternion.Euler(0, 0, z_rotation));
                timer = 0;
            }
           
        }
        if (throwing) {
            throwing = false;
            //扔手榴弹
            Vector3 pos = Vector3.zero;
            if (throwDir == ThrowDir.Top) {
                pos = throwUpPos.position;
            }
            else if (throwDir == ThrowDir.Left || throwDir == ThrowDir.Right) {
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
                    t_rotation =90;
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
    public void Shoot(float v_h,bool isTopKeyDown, bool isBottomKeyDown)
    {
        shoot = true;
        //得到射击的方向
        if (isTopKeyDown == false && isBottomKeyDown == false)
        {
            if (transform.localScale.x == 1)
            {
                shootDir = ShootDir.Left;
             
            }
            else if (transform.localScale.x == -1)
            {
                shootDir = ShootDir.Right;
              
            }
        }else{
            if (isTopKeyDown) {
                shootDir = ShootDir.Top;
            }
            else if (isBottomKeyDown) {
                shootDir = ShootDir.Down;
            } 
        }

    }
    public void Throw(float v_h, bool isTopKeyDown, bool isBottomKeyDown) {
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
        else {
            if (isTopKeyDown) {
                throwDir = ThrowDir.Top;
            }
            else if (isBottomKeyDown) {
                throwDir = ThrowDir.Down;
            }
        }
    }
  }

