using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float startY;
    public float endY;
    public float speed; // Units per second

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.y -= speed * Time.deltaTime;
        if (pos.y < endY)
        {
            pos.y = startY;
        }
        transform.position = pos;
    }
}
