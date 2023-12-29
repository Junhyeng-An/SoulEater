using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Cinemachin : MonoBehaviour
{
    public static Cinemachin Instance;
    public int attackDmg = 1;

    PlayableDirector pd;
    public TimelineAsset[] ta;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        pd = GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CutScene") 
        {
            collision.gameObject.SetActive(false);
            pd.Play(ta[0]);
            
        }
    }
}
