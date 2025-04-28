using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBG : MonoBehaviour
{
    public float movingSpeedB = 0.01f;
    public float movingSpeedK = 0.01f;
    private Vector2 offset = new Vector2(0.0f, 0.0f);
    public GameObject playerObject = null;
    public GameObject gameOverPanel = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        if (gameOverPanel != null && !gameOverPanel.activeSelf)
        {
            if (playerObject != null)
                offset.x = offset.x + (movingSpeedK * playerObject.GetComponent<PlayerControl>().usingUnit.level + movingSpeedB);
            if (offset.x <= -1.0f)
                offset.x = 0.0f;
            GetComponent<SpriteRenderer>().material.SetTextureOffset("_MainTex", offset);
        }
    }
}
