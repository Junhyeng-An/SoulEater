using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialController : MonoBehaviour
{
    public float speed;

    SpriteRenderer sprite;
    SpriteRenderer sprite2;
    TextMeshPro text;

    GameObject children_sprite;
    GameObject children_sprite2;
    GameObject children_text;
    float alpha;

    bool isTutorial;
    bool a;
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            /*
            if(children[i].GetComponent<SpriteRenderer>() != null)
                sprite[i] = children[i].GetComponent<SpriteRenderer>();
            if (children[i].GetComponent<TextMesh>() != null)
                sprite[i] = children[i].GetComponent<TextMesh>();*/
            children_sprite = transform.GetChild(0).gameObject;
            children_text = transform.GetChild(1).gameObject;

            sprite = children_sprite.GetComponent<SpriteRenderer>();
            text = children_text.GetComponent<TextMeshPro>();

            children_sprite2 = transform.GetChild(2).gameObject;
            sprite2 = children_sprite2.GetComponent<SpriteRenderer>();
        }
    }
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        
        if(isTutorial == true)
        {
            if (alpha <= 1.0f)
                alpha += speed;
            else
                alpha = 1;
            /*for (int i = 0; i < transform.childCount; i++)
            {
                sprite[i].color = new Vector4(sprite[i].color.r, sprite[i].color.b, sprite[i].color.g, alpha);
            }*/
        }
        else
        {
            if (alpha >= 0.0f)
                alpha -= speed;
            else
                alpha = 0;
            /*for (int i = 0; i < transform.childCount; i++)
            {
                sprite[i].color = new Vector4(sprite[i].color.r, sprite[i].color.b, sprite[i].color.g, alpha);
            }*/
        }
        sprite.color = new Vector4(sprite.color.r, sprite.color.b, sprite.color.g, alpha);
        text.color = new Vector4(sprite.color.r, sprite.color.b, sprite.color.g, alpha);
        sprite2.color = new Vector4(sprite.color.r, sprite.color.b, sprite.color.g, alpha);
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "Player")
            isTutorial = true;
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
            isTutorial = false;
    }
}
