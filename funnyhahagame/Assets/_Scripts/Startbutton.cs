using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startbutton : MonoBehaviour
{
    public int scene1,scene2,scene3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public void startbUTTON()
    {
        SceneManager.LoadScene(scene1);
        SceneManager.LoadScene(scene2, LoadSceneMode.Additive);
        SceneManager.LoadScene(scene3, LoadSceneMode.Additive);
        
    }
}
