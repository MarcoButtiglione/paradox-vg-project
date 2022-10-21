using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Transform))]
public class CameraShakeScript : MonoBehaviour
{
    
    public Transform cameraTransform = default;
    private Vector3 _orignalPosOfCam = default;
    public float shakeFrequency = default;
    private bool shake = false;

    // Start is called before the first frame update
    void Start()
    { 
        //When the game starts make sure to assign the origianl possition of the camera, to its current
        //position, supposedly it is where you want the camera to return after shaking.
        _orignalPosOfCam = cameraTransform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        if(shake)
            CameraShake();
        else
            StopShake();
        
    }

    private void CameraShake()
    {
        //This moves the camera position to the random point chosen within the circle around the camera.
        //NB:Our Random.insideUnitSphere selects a random position every frame because of GetKey
        //which is called every frame, and that causes the shaking.
        cameraTransform.position = _orignalPosOfCam + Random.insideUnitSphere * shakeFrequency;
    }

    private void StopShake()
    {
        //Return the camera to it's original position.
        cameraTransform.position = _orignalPosOfCam;
    }
    public void changeShake(){
        shake = !shake;
    }
}
