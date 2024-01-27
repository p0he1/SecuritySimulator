using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public static SkillTree skillTree;
    private void Awake()
    {
        skillTree = this;      
    }

    public int[] skillsPrice;
    public int[] skillsLevel;
    public string[] skillsName;

    public GameObject skillHolder;
    public List<Skill> skillList;    
    
    public GameObject connectorHolder;
    public List<GameObject> connectorList;

    public GameObject skillPanel;

    public bool fixedUpdateOnce = true;

/*    public bool buttonClicked;
    public bool canUpdate;*/

/*    private void Start()
    {
        skillsName = new string[] {
            "Magnetic door",
            "metal detector",
            "Animals for finding illegal substances",
            "Electronic signature for transactions",
            "X-ray devices for purchases",
            "Cameras",
            "AI for tracking the movement of client",
            "Robots to detect suspicious activity",
            "More cameras",
            "Sound recording tools based on AI" ,
            "Video record for all transactions",
            "System of reg. of buyers by face",
            "Alarm system",
            "Cameras with higher quality",
            "Psycho. tests to detect deception"
        };
        skillsPrice = new int[]
        {
            0, 20, 30, 50, 200, 20, 40, 40, 50, 50, 50, 100, 50, 200, 100
        };
        skillsLevel = new int[15];

        foreach (var skill in skillHolder.GetComponentsInChildren<Skill>()) skillList.Add(skill);
        for (int i = 0; i < skillList.Count; i++) skillList[i].id = i;

        foreach (var connector in connectorHolder.GetComponentsInChildren<RectTransform>()) connectorList.Add(connector.gameObject);

        skillList[0].connectedSkills = new int[] { 1, 2, 5 };
        skillList[1].connectedSkills = new int[] { 3 };
        skillList[3].connectedSkills = new int[] { 4 };
        skillList[5].connectedSkills = new int[] { 6, 7 };
        skillList[6].connectedSkills = new int[] { 8, 9 };
        skillList[7].connectedSkills = new int[] { 10 };
        skillList[8].connectedSkills = new int[] { 11, 12 };
        skillList[9].connectedSkills = new int[] { 13, 14 };
        for (int i = 0; i < skillsLevel.Length; i++)
        {
            skillsLevel[i] = PlayerPrefs.GetInt($"level_{i}");
        };
        //UpdateAllSkillsUI();
    }*/

    private void FixedUpdate()
    {
        if(skillPanel.activeSelf && fixedUpdateOnce)
        {
            skillsName = new string[] {
            "Magnetic door",
            "metal detector",
            "Animals for finding illegal substances",
            "Electronic signature for transactions",
            "X-ray devices for purchases",
            "Cameras",
            "AI for tracking the movement of client",
            "Robots to detect suspicious activity",
            "More cameras",
            "Sound recording tools based on AI" ,
            "Video record for all transactions",
            "System of reg. of buyers by face",
            "Alarm system",
            "Cameras with higher quality",
            "Psycho. tests to detect deception"
        };
            skillsPrice = new int[]
            {
            0, 20, 30, 50, 200, 20, 40, 40, 50, 50, 50, 100, 50, 200, 100
            };
            skillsLevel = new int[15];

            foreach (var skill in skillHolder.GetComponentsInChildren<Skill>()) skillList.Add(skill);
            for (int i = 0; i < skillList.Count; i++) skillList[i].id = i;

            foreach (var connector in connectorHolder.GetComponentsInChildren<RectTransform>()) connectorList.Add(connector.gameObject);

            skillList[0].connectedSkills = new int[] { 1, 2, 5 };
            skillList[1].connectedSkills = new int[] { 3 };
            skillList[3].connectedSkills = new int[] { 4 };
            skillList[5].connectedSkills = new int[] { 6, 7 };
            skillList[6].connectedSkills = new int[] { 8, 9 };
            skillList[7].connectedSkills = new int[] { 10 };
            skillList[8].connectedSkills = new int[] { 11, 12 };
            skillList[9].connectedSkills = new int[] { 13, 14 };
            for (int i = 0; i < skillsLevel.Length; i++)
            {
                skillsLevel[i] = PlayerPrefs.GetInt($"level_{i}");
            };
            UpdateAllSkillsUI();
            fixedUpdateOnce = false;
        }
    }

    public void UpdateAllSkillsUI()
    {
        foreach (Skill skill in skillList)
        {
            skill.UpdateUI();
        };
    }
}
