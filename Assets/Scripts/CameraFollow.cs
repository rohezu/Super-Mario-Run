using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {


    public GameObject player;
    private float deltax;
    private float cameraY;
    private float cameraZ;
    public float deltaY;
	// Use this for initialization
	void Start () {
        deltax = Mathf.Abs(player.transform.position.x - transform.position.x);
        cameraY = transform.position.y;
        cameraZ = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayController.isAlive)
        {
            YFollow();
            setCameraPosition();
        }
        
	}
    void setCameraPosition()
    {
        transform.position = new Vector3(player.transform.position.x + deltax, cameraY, cameraZ);
    }
    void YFollow()
    {
        if(player.transform.position.y < transform.position.y - deltaY)
        {
            cameraY = player.transform.position.y + deltaY;
        }
        else if (player.transform.position.y > transform.position.y + deltaY)
        {
            cameraY = player.transform.position.y - deltaY;
        }
    }
}
