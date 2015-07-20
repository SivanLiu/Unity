using UnityEngine;
using System.Collections;

public class ZombieAnimator : MonoBehaviour {

	public Sprite[] sprites;
	public float framesPerSecond;
	private SpriteRenderer spriteRenderer;

	void Start () {
		//初始化spriterRenderer
		spriteRenderer = renderer as SpriteRenderer;

	}
	
	// Update is called once per frame
	void Update () {
		//关卡载入到当前的时间秒数和每秒渲染的帧数相乘
		int index=(int)(Time.timeSinceLevelLoad*framesPerSecond);
		//循环播放
		index=index % sprites.Length;
		spriteRenderer.sprite=sprites[index];
	}
}
