using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Black_Smith : MonoBehaviour
{
    public GameObject weapon_Upgrade;
    
    private bool isInTrigger = false;
 
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어가 트리거에 들어갔을 때 호출되는 함수
        if (other.CompareTag("Player"))
        {
            // 트리거 영역에 들어갔음을 표시
            isInTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // 플레이어가 트리거에서 나갔을 때 호출되는 함수
        if (other.CompareTag("Player"))
        {
            // 트리거 영역에서 나갔음을 표시
            isInTrigger = false;
        }
    }

    void Update()
    {
        if (isInTrigger && Input.GetKeyDown(KeyCode.V))
        {
            // 화면을 활성화
            if (weapon_Upgrade != null)
            {
                weapon_Upgrade.SetActive(true);
            }
        }
    }





    public void Player_attack_damage_upgrade()
    {
        if (DataManager.Instance._PlayerData.coin >= DataManager.Instance._SwordData.Upgrade_attack_Cost - (DataManager.Instance._SwordData.Upgrade_attack_Cost * DataManager.Instance._Player_Skill.Discount_Cost/100))
        {
            DataManager.Instance._PlayerData.coin -= DataManager.Instance._SwordData.Upgrade_attack_Cost - Mathf.RoundToInt(DataManager.Instance._SwordData.Upgrade_attack_Cost * DataManager.Instance._Player_Skill.Discount_Cost / 100);
            DataManager.Instance._SwordData.player_damage_attack += 2;
            DataManager.Instance._SwordData.player_attack_level += 1;
            DataManager.Instance._SwordData.Upgrade_attack_Cost += 1;
            SoundManager.Instance.Playsfx(SoundManager.SFX.upgrade);
        }
    }

    public void Player_parrying_damage_upgrade()
    {
        if (DataManager.Instance._PlayerData.coin >= DataManager.Instance._SwordData.Upgrade_parrying_Cost - (DataManager.Instance._SwordData.Upgrade_parrying_Cost * DataManager.Instance._Player_Skill.Discount_Cost / 100))
        {
            DataManager.Instance._PlayerData.coin -= DataManager.Instance._SwordData.Upgrade_parrying_Cost - Mathf.RoundToInt(DataManager.Instance._SwordData.Upgrade_parrying_Cost * DataManager.Instance._Player_Skill.Discount_Cost / 100);
            DataManager.Instance._SwordData.player_parrying_attack += 1;
            DataManager.Instance._SwordData.player_parrying_level += 1;
            DataManager.Instance._SwordData.Upgrade_parrying_Cost++;
            SoundManager.Instance.Playsfx(SoundManager.SFX.upgrade);
        }
    }

    public void Player_sword_reach_upgrade()
    {
        if (DataManager.Instance._PlayerData.coin >= DataManager.Instance._SwordData.Upgrade_reach_Cost - (DataManager.Instance._SwordData.Upgrade_reach_Cost * DataManager.Instance._Player_Skill.Discount_Cost / 100))
        {
            DataManager.Instance._PlayerData.coin -= DataManager.Instance._SwordData.Upgrade_reach_Cost - Mathf.RoundToInt(DataManager.Instance._SwordData.Upgrade_reach_Cost * DataManager.Instance._Player_Skill.Discount_Cost / 100);
            DataManager.Instance._SwordData.player_sword_reach += 0.1f;
            DataManager.Instance._SwordData.player_sword_level += 1;
            DataManager.Instance._SwordData.Upgrade_reach_Cost++;
            SoundManager.Instance.Playsfx(SoundManager.SFX.upgrade);
        }
    }



}

