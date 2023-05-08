using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Battle_UI : MonoBehaviour
{
    public TMP_Text Mission_Name;
    public TMP_Text Mission_Disc;
    public TMP_Text Goal;
    public QuestManager questmanager;
  
    // Start is called before the first frame update
    void Awake()
    {
        SetMissionNameDesc();
        questmanager = QuestManager.instance;
    }

    private void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.inHub)
        {
            SetMissionNameDesc();
            updateGoal();
        }
    }

    public void SetMissionNameDesc()
    {
        if (questmanager != null)
        {
            Mission_Name.text = questmanager.mission_type.ToString();
            Mission_Disc.text = questmanager.mission_description.ToString();
        }
    }

    void updateGoal() 
    {
        Goal.text = questmanager.GetComponent<QuestManager>().goal;
    }

    private void OnEnable()
    {
       questmanager = FindObjectOfType<QuestManager>();
       SetMissionNameDesc();
    }

    private void OnDisable()
    {
      
    }
}
