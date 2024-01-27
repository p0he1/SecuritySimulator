using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    private SkillTree skillTree;
    public moneyCounter MoneyCounter;

    public TMP_Text nameText;
    public TMP_Text priceText;

    public int id;

    public int[] connectedSkills;

    private void Start()
    {
        skillTree = SkillTree.skillTree;
        skillTree.UpdateAllSkillsUI();
    }

    public void UpdateUI()
    {
        nameText.text = skillTree.skillsName[id];
        priceText.text = $"{skillTree.skillsLevel[id]}/{skillTree.skillsPrice[id]}";
        GetComponent<Image>().color = skillTree.skillsPrice[id] == skillTree.skillsLevel[id] ? Color.white :
            MoneyCounter.numberUAH >= skillTree.skillsPrice[id] ? new Color(0, 0, 0, 0.5f) : new Color(0, 0, 0, 0.5f);

        foreach (int connectedSkill in connectedSkills)
        {
            skillTree.skillList[connectedSkill].gameObject.SetActive(skillTree.skillsPrice[id] == skillTree.skillsLevel[id]);
            skillTree.connectorList[connectedSkill].gameObject.SetActive(skillTree.skillsPrice[id] == skillTree.skillsLevel[id]);
        }
        for (int i = 0; i < skillTree.skillsLevel.Length; i++)
        {
            PlayerPrefs.SetInt($"level_{i}", skillTree.skillsLevel[i]);
        };
    }

    public void Buy()
    {
        if (skillTree.skillsPrice[id] == skillTree.skillsLevel[id] || MoneyCounter.numberUAH <= skillTree.skillsPrice[id]) return;
        skillTree.skillsLevel[id] = skillTree.skillsPrice[id];
        MoneyCounter.numberUAH -= skillTree.skillsPrice[id];
        skillTree.UpdateAllSkillsUI();
    }
}
