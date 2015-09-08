using UnityEngine;
using System.Collections;

public class GrenadeExplosionController : MonoBehaviour {
   
    public float animSpeed = 10;//1秒播放10帧图片
    private float animTimeInterval = 0;
    public bool isGround = false;
    //Sprite渲染器
    public SpriteRenderer grenadeExplosionRenderer;
    //爆炸状态渲染
    public Sprite[] grenadeExplosionSpriteArray;
    private int grenadeExplosionIndex = 0;
    private int grenadeExplosionLength = 0;
    private float grenadeExplosionTimer = 0;
	// Use this for initialization
	void Start () {
        animTimeInterval = 1 / animSpeed;//得到每一帧的时间间隔
        grenadeExplosionLength = grenadeExplosionSpriteArray.Length;
       
       
	}
	
	// Update is called once per frame
	void Update () {
        {
            if (isGround) {
                //this.gameObject.SetActive(true);
                grenadeExplosionTimer += Time.deltaTime;
                //播放下一帧
                if (grenadeExplosionTimer > animTimeInterval)
                {
                    //当前计时器减去下一帧的时间间隔
                    grenadeExplosionTimer -= animTimeInterval;
                    //当前帧数加1
                    grenadeExplosionIndex++;
                    print(grenadeExplosionIndex);
                    //判断是否达到最大帧数
                    grenadeExplosionIndex %= grenadeExplosionLength;
                   
                }
                 
            }
            grenadeExplosionRenderer.sprite = grenadeExplosionSpriteArray[grenadeExplosionIndex];
            if (grenadeExplosionIndex == grenadeExplosionLength -1) {
                isGround = false;
                Destroy(this.gameObject);
            }

        }
      
	}
    
   
}
