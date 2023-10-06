using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "ItemEft/Consumable/Health")]
public class Healing : ItemEffect
{
    public int healingPoint = 0;
    public string Desscription;
    public override bool ExcuteRole() // Item Effect must override Excute Role
    {
        Debug.Log("heal");
        return true;


    }
    public override string Script()
    {


        return Desscription;
    }
}
