using UnityEngine;
using System.Collections;

public class Released : MonoBehaviour {

    public float animSpeed = 10;//1秒播放10帧图片
    private float animTimeInterval = 0;
    //Sprite渲染器
    public SpriteRenderer releasedRenderer;
    //released状态上半身渲染
    public Sprite[] releasedSpriteArray;
    private int releasedIndex = 0;
    private int releasedLength = 0;
    private float releasedTimer = 0;
	// Use this for initialization
	void Start () {
        animTimeInterval = 1 / animSpeed;//得到每一帧的时间间隔
        releasedLength = releasedSpriteArray.Length;
	}
	
	// Update is called once per frame
	void Update () {
        releasedTimer += Time.deltaTime;
        //播放下一帧
        if (releasedTimer > animTimeInterval)
        {
            //当前计时器减去下一帧的时间间隔
            releasedTimer -= animTimeInterval;
            //当前帧数加1
            releasedIndex++;
            //判断是否达到最大帧数
            releasedIndex %= releasedLength;
            releasedRenderer.sprite = releasedSpriteArray[releasedIndex];
        }
	}
}
