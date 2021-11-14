using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public GameObject player;

    private float x_lower = -9.5f, y_lower = -14.5f;
    private float x_upper = 26.5f, y_upper = 1.5f;

    void Start()
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        x_lower += width / 2;
        x_upper -= width / 2;

        y_lower += height / 2;
        y_upper -= height / 2;
    }
 
    void Update()
    {
        this.transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, x_lower, x_upper), Mathf.Clamp(player.transform.position.y, y_lower, y_upper), -1);
    }
}
