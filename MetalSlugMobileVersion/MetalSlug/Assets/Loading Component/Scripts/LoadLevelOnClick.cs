using UnityEngine;

public class LoadLevelOnClick : MonoBehaviour
{
    private void Update()
    {
       if(Input.GetMouseButtonDown(0))
        gameObject.SendMessage("LoadNextLevel");
    }
}