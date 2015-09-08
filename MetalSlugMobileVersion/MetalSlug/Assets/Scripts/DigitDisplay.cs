using UnityEngine;
using System.Collections;

public class DigitDisplay : MonoBehaviour {
    public Texture[] lifeNumbers;
    public Texture[] weaponNumbers;
    public float posx = 32.5f;
    public float posy = 373;
    //生命值
    public static int life = 20;
    //手榴弹数目
    public static int grenade =16;
    public Texture[] scoreNumbers;
    //计时器
    public Texture[] timeNumbers;
    public Texture lifeimg;
    public Texture grenadeimg;
    public Texture projectileimg;
    public Texture scoreimg;
    //剩余时间
    public static int leftTime = 500;
    //player所得分数
    public static int score = 0;
    private float mtime = 0;
    void OnGUI() {
        float xFactor = Screen.width / 720f;
        float yFactor = Screen.height / 1280f;
        Camera.main.rect = new Rect(0, 0, 1, xFactor / yFactor);
        //分数
        GUI.DrawTexture(new Rect(8, 5, 28*1.5f, 30*1.5f), scoreimg);
        //分数
        DrawImageNumber(65, 10, score, scoreNumbers, 16*1.5f, 24*1.5f);
        //计时器
        DrawImageNumber(670, 0, leftTime, timeNumbers, 32, 45);
        //生命值
        GUI.DrawTexture(new Rect(295+210, 670, 45,45), lifeimg);
        DrawImageNumber(549, 685, life, lifeNumbers,16,22);
        //子弹
        GUI.DrawTexture(new Rect(360 + 220, 670, 45, 45), projectileimg);
        GUI.DrawTexture(new Rect(390 + 240, 690, 24, 12), weaponNumbers[10]);
        //手榴弹
        GUI.DrawTexture(new Rect(420 + 240, 680, 45 * 1.6f, 18 * 1.6f), grenadeimg);
        DrawImageNumber(735, 690, grenade, weaponNumbers,13,15);
        if (life <= 0&&Application.loadedLevelName=="play2") {
            Application.LoadLevel("gameover4");
        }

    }
    //x为绘制数字的x轴坐标，y为y轴坐标，texes  图片资源的数组  
    void DrawImageNumber(float x, float y, int number, Object[] texes,float width,float height)
    {
        //将整形转成字符数组  
        char[] chars = number.ToString().ToCharArray();
        //获取一张图片  
        Texture2D tex = (Texture2D)texes[0];
        //遍历字符数组  
        foreach (char s in chars)
        {
            //得到每一位整形数据  
            int i = int.Parse(s.ToString());
            //绘制数字  
            GUI.DrawTexture(new Rect(x, y, width, height), (Texture2D)texes[i]);
            x += width-2;

        }

    }
    void Update() {
        mtime += Time.deltaTime;
        if (mtime > 1)
        {
            leftTime--;
            mtime = 0;
        }
    }
    
}
