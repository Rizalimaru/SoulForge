using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Transform player;
    public float smoothCameraMove = 5f;
    public Vector3 offset =  new Vector3(0,0,-10f);

    void LateUpdate()
    {
        if(player != null)
        {   
            Vector3 targetPosition = player.position + offset;
            transform.position =  Vector3.Lerp(transform.position, targetPosition, smoothCameraMove * Time.deltaTime);
        }
    }



}
