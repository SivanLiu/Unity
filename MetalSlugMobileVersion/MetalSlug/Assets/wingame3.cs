using UnityEngine;
using System.Collections;

public class wingame3 : MonoBehaviour {

    public AudioClip wingamesound;
    GameObject mainCamera;
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        AudioSource.PlayClipAtPoint(wingamesound, new Vector3(mainCamera.transform.position.x, 0, 0));

    }
    // Use this for initialization
    public void StartNextGame()
    {
        DigitDisplay.life = 20;
        DigitDisplay.grenade = 16;
        DigitDisplay.leftTime = 500;
        Application.LoadLevel("play5");

    }
    // Update is called once per frame
    public void exitGame()
    {

        Application.Quit();

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { Application.Quit(); }
    }
}
