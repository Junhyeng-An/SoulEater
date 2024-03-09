using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Singleton : MonoBehaviour
{
    private static GameManager_Singleton instance = null;
    public static GameManager_Singleton Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
     
            return instance;
        }
    }
     
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        // "controlled" �±װ� ������ GameObject ã��
        GameObject controlledObject = GameObject.FindGameObjectWithTag("Controlled");

        if (controlledObject != null)
        {
            // GameManager GameObject ã��


            if (gameObject != null)
            {
                // controlledObject�� GameManager�� �ڽ����� �����
                controlledObject.transform.parent = gameObject.transform;
            }
            else
            {
                Debug.LogWarning("GameManager object not found.");
            }
        }
        else
        {
            Debug.LogWarning("No GameObject with tag 'controlled' found.");
        }
        
        foreach (Transform child in gameObject.transform)
        {
            if (child.gameObject.tag != "Controlled")
            {
                Destroy(child.gameObject);
            }
        }
        
        
        
        
        
        
        
        
        
    }


 
    
  
    
}


