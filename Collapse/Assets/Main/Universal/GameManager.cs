using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    //player transferrables
    public GameObject player;
    public GameObject returnPoint;
    public List<Weapon_Func> temp_weapons;
    public List<Weapon_Func> saved_weapons;
    bool loadoutSaved = false;
    

    public Canvas hub_ui;
    public Canvas battle_ui;

    public int difficulty = 1;

    public int spawn_amount = 0;

    public int rewardAmount = 0;
    public string rewardType = string.Empty;
    public bool missionSuccess = false;
   

    public bool inHub = false;
    public bool player_spawned = false;
    public bool gamePaused = false;
    public bool readyReturn = false;
    public float percent_done = 0.0f;
    public List<KeySpawner_Scr> keyspawner_list;
    public List<KeySpawner_Scr> playerspawns_list;
    bool freshStart = false;

    private void Awake()
    {
        freshStart = true;
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != null && instance != this)
        {
            
            Destroy(this);
        }

        if (SceneManager.GetActiveScene().name == "Hub" || SceneManager.GetActiveScene().name == "Playground")
        {
            inHub = true;
        }
        else
            inHub = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        switch (difficulty) 
        {
            case 1:
                spawn_amount = 5;
                break;
        }
        //SetGame();
        player = FindObjectOfType<Player_Controller>().gameObject;
        SetCamera();

       
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCamera()
    {
        var temp = player.transform.Find("Functions");
        Camera.main.transform.position = temp.transform.Find("CameraHolder").position;
        Camera.main.transform.rotation = temp.transform.Find("CameraHolder").rotation;
        Camera.main.GetComponent<Camera_Movement>().camTarget = player.transform;
        Camera.main.GetComponent<Camera_Movement>().SetVars();
      
    }

    public void saveLoadout() 
    {
        for (int i = 0; i < player.GetComponent<Player_Controller>().weapons.Count; i++)
        {

            DontDestroyOnLoad(player.GetComponent<Player_Controller>().weapons[i]);
            saved_weapons.Add(player.GetComponent<Player_Controller>().weapons[i]);
        }

        loadoutSaved = true;
        //saved_weapons = temp_weapons;
    }

    void restoreLoadout() 
    {
        player.GetComponent<Player_Controller>().SetWeapons();
        loadoutSaved = false; 
    }
    public void SetGame() //set up the game (Spawns Player and Objective. Later will add additional treasures and mining resource spots)
    {

        GrabSpawnPoints();

        if(playerspawns_list.Count > 0) 
        {
            int RandomS_Player = Random.Range(0, playerspawns_list.Count);

            playerspawns_list[RandomS_Player].SpawnPlayer();
        }
        //set player again.
        player = FindObjectOfType<Player_Controller>().gameObject;

        if (keyspawner_list.Count > 0) 
        {
            int RandomS_Obj = Random.Range(0, keyspawner_list.Count);

            keyspawner_list[RandomS_Obj].SpawnObj();
        }

        player = FindObjectOfType<Player_Controller>().gameObject;
        spawn_amount = 5 * (QuestManager.instance.difficulty + 1);

        SetCamera();

        var equip = Player_Equipment.instance;
        if(equip.weapons.Count > 0) 
        {
            equip.restoreLoadOut();
        }
        
        
    }

    public void cacheReward(int rewardMult) 
    {
        rewardAmount = 50 * rewardMult;
        
    }

    public void GiveReward() 
    {
        switch (rewardType)
        {
            case "Food/Water": // tropical
                GetComponent<Resource_Handler>().SetSurvivalResource(rewardAmount);
                GetComponent<Resource_Handler>().SetSurvival(rewardAmount);
                break;
            case "Biomass": //forest
                GetComponent<Resource_Handler>().SetFuelResource(rewardAmount);
                GetComponent<Resource_Handler>().SetFuel(rewardAmount);
                break;
            case "Minerals": // Desert
                GetComponent<Resource_Handler>().SetMineralsResource(rewardAmount);
                GetComponent<Resource_Handler>().SetMinerals(rewardAmount);
                break;
        }
    }
    public void SetCompleteTarget() 
    {
        if (!readyReturn) 
        {
            readyReturn = true;
       
        }
    }

    public void MissionFailed() 
    {

    }


    public void GrabSpawnPoints()
    {
        var spawner_list = FindObjectsOfType<KeySpawner_Scr>();
        //clear lists before, to avoid null spawn points on reload.
        playerspawns_list.Clear();
        keyspawner_list.Clear();
        if (spawner_list.Length > 0)
        {

            for (int i = 0; i < spawner_list.Length; i++)
            {
                if (spawner_list[i].spawn_player)
                    playerspawns_list.Add(spawner_list[i]);
                else if (spawner_list[i].spawn_obj)
                    keyspawner_list.Add(spawner_list[i]);
            }
        }

        percent_done += 0.1f;
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable called");
        
        SceneManager.sceneLoaded -= OnSceneLoaded;
       

    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {
        Debug.Log("Level Loaded");
        Debug.Log(scene.name);
        Debug.Log(mode);

        if (freshStart)
        {
            freshStart = false;
            return;
        }   
        if (SceneManager.GetActiveScene().name == "Hub" || SceneManager.GetActiveScene().name == "Playground")
        {
            inHub = true;
            var canvasgroup = FindObjectsOfType<Canvas>();

            for (int i = 0; i < canvasgroup.Length; i++) 
            {
                if (canvasgroup[i].GetComponentInChildren<Battle_UI>())
                    battle_ui = canvasgroup[i];

                if (canvasgroup[i].GetComponent<Hub_UI>())
                    hub_ui = canvasgroup[i]; 
            }

            if (hub_ui != null && battle_ui != null)
            {
                //hub_ui.enabled = true;
                hub_ui.gameObject.SetActive(true);
                //battle_ui.enabled = false;
                battle_ui.gameObject.SetActive(false);
            }

            if(player == null)
                player = FindObjectOfType<Player_Controller>().gameObject;

            returnPoint = FindObjectOfType<ToMission>().gameObject;
            player.transform.position = returnPoint.transform.position;
            SetCamera();

            var equip = Player_Equipment.instance;
            if (equip.weapons.Count > 0)
            {
                equip.restoreLoadOut();
            }

            QuestManager.instance.ResetQuests();
        }
        else if(SceneManager.GetActiveScene().name == "RewardScene") 
        {
            GetComponent<Resource_Handler>().resetFuture();
            GiveReward();
        }
        else if(SceneManager.GetActiveScene().name == "Battle_Forest" || SceneManager.GetActiveScene().name == "Battle_Desert" 
            || SceneManager.GetActiveScene().name == "Battle_Tropical")
        {
            SetGame();
            inHub = false;
            if (hub_ui != null && battle_ui != null)
            {

                
                hub_ui.gameObject.SetActive(false);
                battle_ui.gameObject.SetActive(true);
            }

            if (gamePaused)
            {
                gamePaused = false;
                Time.timeScale = 1.0f;
            }
        }

     

    }






    #region cheats

    public void motherlode() 
    {
        var rh = GetComponent<Resource_Handler>();

        rh.SetFuel(9999);
        rh.SetMinerals(9999);
        rh.SetSurvival(9999);
    }

    public void gmode() 
    {
        player.GetComponent<Player_Controller>().GodMode();
    }

    public void complete() 
    {
        QuestManager.instance.cheat_complete();
    }

    public void ammo() 
    {
        player.GetComponent<Player_Controller>().weapon.InfAmmo();
    }
    #endregion
}
