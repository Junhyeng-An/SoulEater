using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skill_Select : MonoBehaviour
{
    private List<Skill_Card> skillCards = new List<Skill_Card>();  // List of skill cards
    List<Skill_Card> selectedCards = new List<Skill_Card>(); // List of selected skill cards

    public List<Text> Card;
    public List<Button> Button;
    bool isShow = false;

    class Skill_Card
    {
        public string name; // Skill name
        public string skillContent; // Skill description
        public float upgradeCount; // Number of upgrades available

        public Skill_Card(string N, string SC, float UC)
        {
            name = N;
            skillContent = SC;
            upgradeCount = UC;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        skillCards.Add(new Skill_Card("Heal", "Restores HP when used", 10));
        skillCards.Add(new Skill_Card("Charge Attack", "Charges and performs a powerful attack", 12));
        skillCards.Add(new Skill_Card("Elemental Affinity", "Enhances weapon with elemental effects", 15));
        CardShuffle();
        CardShow();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CardShuffle()
    {
        List<int> selectedIndices = new List<int>(); // List of selected indices
        for (int i = 0; i < 3; i++)
        {
            int randomIndex;

            // Ensure that the same index is not selected again
            do
            {
                randomIndex = Random.Range(0, skillCards.Count);
            } while (selectedIndices.Contains(randomIndex));

            // Add the selected index to the list and add the corresponding card to the selectedCards list
            selectedIndices.Add(randomIndex);
            selectedCards.Add(skillCards[randomIndex]);
        }
    }

    void CardShow()
    {
        for (int i = 0; i < selectedCards.Count; i++)
        {
            Card[i].text = $"Card {i + 1}: {selectedCards[i].name}\n\n{selectedCards[i].skillContent}\n\nUpgrade Count: {selectedCards[i].upgradeCount}";
        }
    }

    void CardEffect(int selectedIndex)
    {
        Skill_Card selectedSkill = selectedCards[selectedIndex];

        // Apply the effects based on the selected skill
        switch (selectedSkill.name)
        {
            case "Heal":
                DataManager.Instance._Player_Skill.HP_Drain += selectedSkill.upgradeCount;
                break;
            case "Charge Attack":
                DataManager.Instance._Player_Skill.sword_Reach += selectedSkill.upgradeCount;
                break;
            case "Elemental Affinity":
                DataManager.Instance._Player_Skill.fire_dote += selectedSkill.upgradeCount;
                break;
                // Add cases for other skills as needed
        }

        // Save the data after applying the effects
        DataManager.Instance.SaveData();
    }

    public void ButtonClick(int buttonIndex) // Button event, calls CardEffect(int selectedIndex)
    {
        CardEffect(buttonIndex);
        gameObject.SetActive(false);
        isShow = false;
    }
}
