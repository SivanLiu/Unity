using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

    public AudioClip sound;
    GameObject mainCamera;
    void Start() {
        mainCamera = GameObject.Find("Main Camera");
//        AudioSource.PlayClipAtPoint(sound, new Vector3(mainCamera.transform.position.x, 0, 0));

    }
}
