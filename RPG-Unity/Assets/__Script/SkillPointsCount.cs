using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPointsCount : MonoBehaviour
{
    private Text skillPointsText;
    public SkillTree skillPointsCount;

    // Start is called before the first frame update
    void Start()
    {
        Player.SkillPoints = 10;
        skillPointsText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        skillPointsText.text = "Skill Points: " + Player.SkillPoints;
    }
}
