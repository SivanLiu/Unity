using UnityEngine;
using System.Collections;
public enum ThrowDir
{
    Left,
    Right,
    Top,
    Down
}
public class PlayerThrowGrenade : MonoBehaviour {

    public float throwRate = 5f;//代表每秒可以射击的次数
    //射击的时间间隔
    private float throwTimeInterval = 0;
    private float timer = 0;
    private bool isThrow = true;

    public PlayerGround playerGround;
    public PlayerDown playerDown;
    public PlayerJump playerJump;

    private PlayerController playerMove;
    private bool isTopKeyDown = false;
    private bool isBottomDown = false;

    public bool isGrenade = false;

    public PlayerController playerController;
   
	void Start () {
        throwTimeInterval = 1 / throwRate;
        playerMove = this.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isThrow == false)
        {
            timer += Time.deltaTime;
            if (timer >= throwTimeInterval)
            {
                isThrow = true;
                timer -=throwTimeInterval;
            }
        }
        if (Input.GetKeyDown(KeyCode.W) || playerController.Top)
        {
                isTopKeyDown=true;
         }
        if (Input.GetKeyUp(KeyCode.W) || !playerController.Top)
        {
            isTopKeyDown=false;
        }
        if (Input.GetKeyDown(KeyCode.S) || playerController.Down)
        {
                isBottomDown=true;
         }
        if (Input.GetKeyUp(KeyCode.S) || !playerController.Down)
        {
            isBottomDown = false;
        }
        if (isThrow &&( Input.GetKeyDown(KeyCode.L) || playerController.Grenade))
        { 
            //进行射击的操作
          //  audio.Play();
            switch (playerMove.state)
            {
                case PlayerState.PlayerGround:
                    playerGround.Throw(rigidbody.velocity.x,playerController.Top,playerController.Down);
                    break;
                case PlayerState.PlayerDown:
                   playerDown.Throw(rigidbody.velocity.x, isTopKeyDown, isBottomDown);
                    break;
                case PlayerState.PlayerJump:
                    playerJump.Throw(rigidbody.velocity.x, isTopKeyDown, isBottomDown);
                    break;
                default:
                    break;
            }
            playerController.Grenade = false;
          }
	}
}


