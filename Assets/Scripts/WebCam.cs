 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class WebCam : MonoBehaviour
{
    public WebCamTexture webCamTexture;
    public GameObject webCameraPlane;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (Application.isMobilePlatform)
        {
            GameObject CameraParent = new GameObject("camParent");//Creating a parent to the camera using Script
            CameraParent.transform.position = this.transform.position;//Setting the position of the parent to the position of the Main Camera
            this.transform.parent = CameraParent.transform;//establishing the parent child relationship between CameraParent game obj and the Main Camera game obj
            //CameraParent.transform.Rotate(Vector3.right, 90);//since the game is gonna be in landscape, we rotate the Parent by 90 deg to rotate the scene by 90 
                                                             //when played on device but in unity engine the scene is gonna be displayed by the local transform
                                                             // of the Main Camera which isn't manipulated.
        }
        Input.gyro.enabled = true;//Now we enable the gyro of the mobile and follow the Update func

        webCamTexture = new WebCamTexture();
        webCameraPlane.GetComponent<MeshRenderer>().material.mainTexture = webCamTexture;
        webCamTexture.Play();
    }
    
    // Update is called once per frame
    void Update()
    {
        Quaternion cameraRotation = new Quaternion(Input.gyro.attitude.x, Input.gyro.attitude.y, Input.gyro.attitude.z, Input.gyro.attitude.w);//take the current rotation values of the Mobile Device
        this.transform.localRotation = cameraRotation;//assign the taken rotation values to the cameras transform
    }
    public void FireButton()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        //bullet.transform.position = Camera.main.transform.position;
        bullet.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        bullet.transform.rotation = Camera.main.transform.rotation;//new Quaternion(Camera.main.transform.rotation.x , Camera.main.transform.position.y + 90f, Camera.main.transform.position.z + 90f, Camera.main.transform.position.x);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(Camera.main.transform.forward * 1000f);
        Destroy(bullet, 5f);
        this.GetComponent<AudioSource>().Play();
    }
}
