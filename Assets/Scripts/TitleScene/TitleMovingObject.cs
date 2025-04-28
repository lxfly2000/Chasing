using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMovingObject : MonoBehaviour
{
    public float movingSpeed = 0.01f;
    private Vector2 offset = new Vector2(0.0f, 0.0f);

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
        offset.x = offset.x - movingSpeed;
        if (offset.x <= -1.0f)
            offset.x = 0.0f;
        GetComponent<SpriteRenderer>().material.SetTextureOffset("_MainTex", offset);
    }
}
