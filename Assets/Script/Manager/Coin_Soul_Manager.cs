using System.Collections;
using System.Collections.Generic;
using Calcatz.MeshPathfinding;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Coin_Soul_Manager : MonoBehaviour
{
    
    private static Coin_Soul_Manager instance = null;
    public static Coin_Soul_Manager Instance
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
    
    
    
    
    private int Soul_Count;
    public TextMeshProUGUI Soul_textName;
    private int Coin_Count;
    public TextMeshProUGUI Coun_text_Name;

    public GameObject Start_Coin;
    public GameObject Start_Soul;
    public GameObject Minimap;
    public GameObject HP_ST;
    
    
    
    
    

    public GameObject Coin;
    public GameObject Soul;
    
    
    private void Awake()
    {
        #region Singleton

        

 
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion
        
        
        Soul_Count = DataManager.Instance._PlayerData.soul_Count;
        Coin_Count = DataManager.Instance._PlayerData.coin;
    }


    

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Start_Page" || SceneManager.GetActiveScene().name == "Loading")
        {
            Start_Coin.SetActive(false);
            Start_Soul.SetActive(false);
            HP_ST.SetActive(false);
        } 
        else
        {
            Start_Coin.SetActive(true);
            Start_Soul.SetActive(true);
            Minimap.SetActive(true);
            HP_ST.SetActive(true);
        }
        
        
        if (SceneManager.GetActiveScene().name == "Start_Page" || SceneManager.GetActiveScene().name == "Loading" || SceneManager.GetActiveScene().name == "Main" || SceneManager.GetActiveScene().name == "Dorf")
            Minimap.SetActive(false);
    
        
        Show_Count();
    }

    void Show_Count()
    {
        Soul_Count = DataManager.Instance._PlayerData.soul_Count;
        Soul_textName.text = Soul_Count.ToString();

        Coin_Count = DataManager.Instance._PlayerData.coin;
        Coun_text_Name.text = Coin_Count.ToString();
    }
    
    
    public void Drop_Coin(Transform transform)
    {
        if (Coin != null)
        {
         
            GameObject item = Instantiate(Coin, transform.position, Quaternion.identity);
            
            Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Debug.Log("Coin_Drop");
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                rb.AddForce(randomDirection * 5, ForceMode2D.Impulse);
            }
        }
        
    }

    public void Drop_Soul(Transform transform)
    {
        if (Soul != null)
        {
       
            GameObject item = Instantiate(Soul, transform.position, Quaternion.identity);
            
            Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                rb.AddForce(randomDirection * 5, ForceMode2D.Impulse);
            }
        }
    }
    
    
}