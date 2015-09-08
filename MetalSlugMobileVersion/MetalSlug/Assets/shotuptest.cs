using UnityEngine;
using System.Collections;

public class shotuptest : MonoBehaviour {
    public float animSpeed = 10;//1秒播放10帧图片
    private float animTimeInterval = 0;
    public AnimStatus status = AnimStatus.Idle;//表示主角当前的状态
    //Sprite渲染器
    public SpriteRenderer upRenderer;
    public SpriteRenderer downRenderer;
    //在shot状态下,上半身动作
    public Sprite[] shotUpSpriteArray;
    private int shotUpIndex = 0;
    private int shotUpLength = 0;
    private float shotUpTimer = 0;
    //walk下半身动作
    public Sprite shotUpdownsSprite;
	// Use this for initialization
	void Start () {
        shotUpLength = shotUpSpriteArray.Length;
	}
	
	// Update is called once per frame
	void Update () {
        shotUpTimer += Time.deltaTime;
        //播放下一帧
        if (shotUpTimer > animTimeInterval)
        {
            //当前计时器减去下一帧的时间间隔
            shotUpTimer -= animTimeInterval;
            shotUpIndex++;
            shotUpIndex %= shotUpLength;
            upRenderer.sprite = shotUpSpriteArray[shotUpIndex];
        }
        downRenderer.sprite = shotUpdownsSprite;
	}
}
