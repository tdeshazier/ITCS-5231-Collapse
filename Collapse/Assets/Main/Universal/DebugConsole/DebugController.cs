using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DebugController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool toggleDebug;
    bool showHelp;

    string input;

    Vector2 scroll;
    public List<object> commandList;
    public static DebugCommand KILL_ALL;
    public static DebugCommand MOTHERLODE;
    public static DebugCommand GMODE;
    public static DebugCommand COMPLETE;
    public static DebugCommand AMMO;
    public static DebugCommand HELP;
    public void ToggleDebug() 
    {
        toggleDebug = !toggleDebug;
    }

    public void onReturn() 
    {
        if(toggleDebug) 
        {
            HandleInput();
            input = "";
            
        }
    }

    private void Update()
    {
        if (toggleDebug)
        {

            if (Input.GetButton("Submit"))
            {
                onReturn();
            }
        }
    }

    private void Awake()
    {
        MOTHERLODE = new DebugCommand("motherlode", "9999 to all resources", "motherlode", () =>
        {
            GameManager.instance.motherlode();
        });

        HELP = new DebugCommand("help", "Shows a list of commands", "help", () =>
        {
            showHelp = true;
        });

        GMODE = new DebugCommand("gmode", "Gives player god mode", "gmode", () =>
        {
            GameManager.instance.gmode();
        });

        COMPLETE = new DebugCommand("complete", "Completes the current mission", "complete", () =>
        {
            GameManager.instance.complete();
        });

        AMMO = new DebugCommand("ammo", "Gives player infinite ammo", "ammo", () =>
        {
            GameManager.instance.ammo();
        });

        commandList = new List<object>()
        {
            MOTHERLODE,
            GMODE,
            COMPLETE,
            AMMO,
            HELP
        };

        
    }

    void OnGUI()
    {
        if (!toggleDebug)
            return;

        float y = 0f;

        GUI.color = Color.red;
        if(showHelp) 
        {

            GUI.Box(new Rect(0, y, Screen.width, 100), "");

            Rect viewport = new Rect(0,0, Screen.width-30, 20 * commandList.Count);

            scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), scroll, viewport);

            for(int i =0; i < commandList.Count; i++) 
            {
                DebugCommandBase command = commandList[i] as DebugCommandBase;
                string label = $"{command.commandFormat} - {command.commandDescription}";

                Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);

                GUI.Label(labelRect, label);
            }
            GUI.EndScrollView();

            y += 100;
        }

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = Color.black;
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
    }

    void HandleInput() 
    {
        string[] properties = input.Split(' ');
        for(int i = 0; i <commandList.Count; i++) 
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if(input.Contains(commandBase.commandID)) 
            {
                if (commandList[i] as DebugCommand != null) 
                {
                    (commandList[i] as DebugCommand).Invoke();
                }
            }
            else if (commandList[i] as DebugCommand<int> != null) 
            {
                (commandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[1]));
            }
        }
    }
}
