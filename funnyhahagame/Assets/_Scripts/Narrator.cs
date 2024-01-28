using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Narrator : MonoBehaviour
{
    public static Narrator Instance;

    private GameManager gm;
    private Lightswitch lightswitch;
    public  List <AudioSource> audioSource = new List <AudioSource> ();
    public Lightswitch light;
    public UnityEvent<AudioSource> PlayAudio;
   


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;


    }


}  
