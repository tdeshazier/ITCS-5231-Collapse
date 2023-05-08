using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
    public float fadein = 0.0f;
    public float fadeout = 0.0f;
    public string type = "text";
    public int amount = 0;
    public bool fadingIn = false;
    bool fadingOut = false;
    public TextMeshProUGUI displayText;
    // Start is called before the first frame update
    void Start()
    {
        displayText.alpha = 0.0f;
    }

    void startfadeout() 
    {
        fadingIn = false;
        fadingOut = true;
    }
    // Update is called once per frame
    void Update()
    {
        
        if(fadingIn) 
        {
            displayText.text = amount.ToString() + " " + type + " gained";
            if (fadein < 1.0f)
                fadein += Time.deltaTime;
            else
                Invoke("startfadeout", 3.0f);
            displayText.alpha = fadein;
        }

        if(fadingOut) 
        {
            if (fadein > 0.0f)
                fadein -= Time.deltaTime;
            else if (fadein <= 0.0f)
                fadingOut = false;

            displayText.alpha = fadein;
        }
    }
}
