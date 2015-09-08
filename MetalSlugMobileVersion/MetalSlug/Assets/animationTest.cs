using UnityEngine;
using System.Collections;

public class animationTest : MonoBehaviour {

    public float animSpeed = 10;    //1秒播放10帧图片
    private float animTimeInterval = 0;     //动画的时间间隔

    //是否播放一次
    public bool isOnce = false;
    //Sprite渲染器
    public SpriteRenderer mSpriteRenderer;
    //爆炸状态渲染
    public Sprite[] sSpriteArray;
    //当前播放第几帧
    private int mIndex = 0;
    //帧的总数
    private int mLength = 0;
    //定时器
    private float mTimer = 0;

    public Sprite[] spriteArray;
    private float speed = 30;
    private int sIndex = 0;
    private float sanimInterval = 0;

   
    void Start()
    {

        animTimeInterval = 1 / animSpeed;
        mLength = spriteArray.Length;
    }

    void Update()
    {
        mTimer += Time.deltaTime;
       
        //播放下一帧
        if (mTimer > animTimeInterval)
        {
            //当前计时器减去下一帧的时间间隔
            mTimer -= animTimeInterval;
            //当前帧数加1
            mIndex++;
            //判断是否达到最大帧数
            mIndex %= mLength;
        }
        mSpriteRenderer.sprite = spriteArray[mIndex];
       
    }
   
   
       
       
       
      

 }
     

