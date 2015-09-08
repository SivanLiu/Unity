using UnityEngine;
using System.Collections;

public class mAnimatorController : MonoBehaviour {

    public float animSpeed = 10;    //1秒播放10帧图片
    private float animTimeInterval = 0;     //动画的时间间隔
    //是否播放一次
    public bool isOnce = false;
    //Sprite渲染器
    public SpriteRenderer mSpriteRenderer;
    //爆炸状态渲染
    public Sprite[] mSpriteArray;
    //当前播放第几帧
    private int mIndex = 0;
    //帧的总数
    private int mLength = 0;
    //定时器
    private float mTimer = 0;
   
    // Use this for initialization
    void Start()
    {
      // mSpriteRenderer = renderer as SpriteRenderer;
        animTimeInterval = 1 / animSpeed;//得到每一帧的时间间隔
        mLength = mSpriteArray.Length;

    }

    void Update()
    {
        SetAnimations();
    }
    void SetAnimations()
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
        mSpriteRenderer.sprite = mSpriteArray[mIndex];
        if (mIndex == mLength - 1 && isOnce == true)
        {
            Destroy(gameObject);
        }
    }
}
