using UnityEngine;
using System.Collections;
public enum ShootDir { 
    Left,
    Right,
    Top,
    Down
}
public class PlayerShoot : MonoBehaviour {

    public float shootRate = 5.0f;//代表每秒可以射击的次数
    //射击的时间间隔
    private float shootTimeInterval = 0;
    private float timer = 0;
    public  bool isShoot = true;

    public PlayerGround playerGround;
    public PlayerDown playerDown;
    public PlayerJump playerJump;

  
    private bool isTopKeyDown = false;
    private bool isBottomDown = false;
   
    public  PlayerController playerController;

    public bool isProjectile = false;
    public bool isToShoot = false;
	void Start () {
        shootTimeInterval = 1 / shootRate;
	}
   
	// Update is called once per frame
	void Update () {
        if (isShoot == false)
        {
            timer += Time.deltaTime;
            if (timer >= shootTimeInterval) {
                isShoot = true;
                timer -= shootTimeInterval;
            }
        }
        if(Input.GetKeyDown(KeyCode.W)||playerController.Top){
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
       
        if (playerController.Projectile) {
            playerJump.Shoot(rigidbody.velocity.x, isTopKeyDown, isBottomDown);
        }

        if (isShoot && isToShoot&&(isProjectile||Input.GetKeyDown(KeyCode.J)))
        {

            //进行射击的操作
            audio.Play();
            switch (playerController.state)
            {
                //case PlayerState.PlayerJump:
                //  playerJump.Shoot(rigidbody.velocity.x, isTopKeyDown, isBottomDown);
                //    break;
                case PlayerState.PlayerGround:
                    playerGround.Shoot(rigidbody.velocity.x,playerController.Top,playerController.Down);
                    break;
                case PlayerState.PlayerDown:
                   
                    playerDown.Shoot(rigidbody.velocity.x,playerController.Top,playerController.Down);
                    break;
               
                default:
                    break;
            }
            isProjectile = false;
            isToShoot = false;
          }
       
	}

}
