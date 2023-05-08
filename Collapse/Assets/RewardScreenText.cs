using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardScreenText : MonoBehaviour
{
    public GameManager gm;
    public QuestManager qm;
    public Resource_Handler rh;
    
    public Image enviornment_back;
    public TextMeshProUGUI difficulty_type;
    public TextMeshProUGUI rewarded_amount_type;
    public TextMeshProUGUI gained_amount_type;
    public TextMeshProUGUI outcome;
    public Button continue_button;
    public Sprite desert;
    public Sprite tropical;
    public Sprite forest;

    public int index = 0;

    int FuelText = 0;
    int SurvivalText = 0;
    int MineralText = 0;
    int FuelTextCurrent = 0;
    int SurvivalTextCurrent = 0;
    int MineralTextCurrent = 0;

    bool text_needs_update = false;
    int currentTime;
    float scrollingTime;

    string rewarded_string;
    string gained_string;
    string difficulty_string;

    Color32[] button_colors = { new Color32(200, 135, 0, 255), new Color32(150, 0, 0, 255), new Color32(0, 150, 0, 255) }; //orange, red, green
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        qm = QuestManager.instance;


        if (gm != null)
            rh = gm.GetComponent<Resource_Handler>();
        SetScreen();

        outcome.text = gm.missionSuccess ? "Successful Mission" : "Failed Mission";
        continue_button.onClick.AddListener(ContinueHome);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyUp("c")) 
        //{
        //    index++;
        //    if (index >= 3)
        //        index = 0;

        //    SetButtonColor();
        //}
        if (text_needs_update)
        {

            if (scrollingTime > 0.001)
            {
                UpdateGainText();
                scrollingTime = 0;
            }
            else
                scrollingTime += Time.deltaTime;
        }
    }

    void SetButtonColor()
    {
        if (continue_button != null)
        {
            continue_button.image.color = button_colors[index];
        }

    }

    void SetScreen()
    {
        difficulty_type.text = fill_diff();
        index = ButtonColorChoice();
        SetBackgroundImage();
        SetButtonColor();
        GetFutureResource();
        UpdateQuestReward();
    }

    void UpdateGainText()
    {
        if (FuelText <= 0 && SurvivalText <= 0 && MineralText <= 0)
        {
            text_needs_update = false;
            return;
        }

        CalculateResourceText();
        gained_amount_type.text = SurvivalTextCurrent + " Food/Water | " + FuelTextCurrent + " BioMatter | " + MineralTextCurrent + " Minerals";
    }

    void UpdateQuestReward()
    {
        string type = "n/a";

        switch (gm.rewardType)
        {
            case "Food/Water": // tropical
                type = "Food/Water";
                break;
            case "Biomass": //forest
                type = "Biomass";
                break;
            case "Minerals": // Desert
                type = "Minerals";
                break;
        }

        rewarded_amount_type.text = gm.rewardAmount + " " + type;
    }

    void CalculateResourceText()
    {
        if (FuelText > 0)
        {
            FuelTextCurrent++;
            FuelText--;
        }

        if (MineralText > 0)
        {
            MineralTextCurrent++;
            MineralText--;
        }

        if (SurvivalText > 0)
        {
            SurvivalTextCurrent++;
            SurvivalText--;
        }

        
    }

    string fill_diff()
    {
        string text = "n/a";

        switch (qm.difficulty)
        {
            case 0:
                text = "Easy";
                break;
            case 1:
                text = "Medium";
                break;
            case 2:
                text = "Hard";
                break;

        }
        return text;
    }

    int ButtonColorChoice()
    {
        int temp = 9999;

        switch (qm.stage)
        {
            case "Battle_Tropical":
                temp = 0;
                break;
            case "Battle_Forest":
                temp = 1;
                break;
            case "Battle_Desert":
                temp = 2;
                break;

        }
        return temp;
    }

    void SetBackgroundImage()
    {
        switch (qm.stage)
        {
            case "Battle_Tropical":
                enviornment_back.sprite = tropical;
                break;
            case "Battle_Forest":
                enviornment_back.sprite = forest;
                break;
            case "Battle_Desert":
                enviornment_back.sprite = desert;
                break;

        }
    }


    void GetFutureResource()
    {
        FuelText = rh.GetFuelFuture();
        SurvivalText = rh.GetSurvivalFuture();
        MineralText = rh.GetMineralsFuture();

        FuelTextCurrent = 0;
        SurvivalTextCurrent = 0;
        MineralTextCurrent = 0;

        if (FuelText > 0 || SurvivalText > 0 || MineralText > 0)
            text_needs_update = true;

    }

    void ContinueHome() 
    {
        GameManager.instance.GetComponent<LevelControl>().LoadLevel("Hub");
    }
    
}
