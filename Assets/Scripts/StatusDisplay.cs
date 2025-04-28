using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusDisplay : MonoBehaviour
{
    public TMP_Text fpsText = null;
    private int frameCounter = 0;
    private int freshRate = 0;
    private float lastTime = 0;
    private float currentFps = 0;
    public GameObject playerObject = null;

    // Start is called before the first frame update
    void Start()
    {
        frameCounter = 0;
        freshRate = (int)Screen.currentResolution.refreshRateRatio.value;
        lastTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        frameCounter++;
        if(frameCounter>=freshRate)
        {
            float currentTime = Time.time;
            currentFps = frameCounter / (currentTime - lastTime);
            frameCounter = 0;
            lastTime = currentTime;
        }
        int h = 0;
        PlayerControl pc = playerObject.GetComponent<PlayerControl>();
        if (playerObject != null)
            h = pc.usingUnit.health;
        fpsText.text = string.Format("FPS: {0:F1} Health: {1} Level: {2} Score: {3}", currentFps, h, pc.usingUnit.level, GameManager.playerData.playerScore);
    }
}
