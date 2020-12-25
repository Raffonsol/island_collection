using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    #region "Variables"
    Quaternion originalRotation;
    float minimumX = -360F;
    float maximumX = 360F;
    float minimumY = -60F;
    float maximumY = 60F;
    float rotationX = 0F;
    float rotationY = 0F;
    RotationAxes axes = RotationAxes.MouseXAndY;
    Vector3 jump;
    bool isGrounded;

    float waterHeight = 0f;
    bool isUnderwater = false;
    #endregion
    #region "Constants"
    public Rigidbody Rigid;
    public float MoveSpeed;

    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float jumpForce = 2.0f;

    public Color normalColor = new Color (0.5f, 0.5f, 0.5f, 0.5f);
    public Color underwaterColor = new Color (0.22f, 0.65f, 0.77f, 0.5f);

    #endregion

    void Update()
    {
        if (axes == RotationAxes.MouseXAndY)
        {
            // Read the mouse input axis
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            rotationY = ClampAngle(rotationY, minimumY, maximumY);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
            transform.localRotation = originalRotation * xQuaternion * yQuaternion;
        }
        else if (axes == RotationAxes.MouseX)
        {
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation = originalRotation * xQuaternion;
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = ClampAngle(rotationY, minimumY, maximumY);
            Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
            transform.localRotation = originalRotation * yQuaternion;
        }

        float goingSpeed = MoveSpeed;
        // prevent double speed by pressing two keys: 
        // if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)) && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)))
            // goingSpeed /= 2;

         RaycastHit hit;
          if (Physics.Raycast(transform.position, (transform.forward * Input.GetAxis("Vertical") * goingSpeed) + (transform.right * Input.GetAxis("Horizontal") * goingSpeed), out hit))
        //     // print("Found an object - distance: " + hit.distance);
            if (hit.distance <= 0.7f)
            goingSpeed /= 2;

                Rigid.MovePosition(transform.position + (transform.forward * Input.GetAxis("Vertical") * goingSpeed) + (transform.right * Input.GetAxis("Horizontal") * goingSpeed));
        //     else 
                // Rigid.MovePosition(transform.position + (transform.forward * Input.GetAxis("Vertical") * -goingSpeed) + (transform.right * Input.GetAxis("Horizontal") * goingSpeed));
            

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {

            Rigid.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if ((transform.position.y < waterHeight) != isUnderwater) {
            isUnderwater = transform.position.y < waterHeight;
            if (isUnderwater) SetUnderwater ();
            if (!isUnderwater) SetNormal ();
        }
    }
   
    // Start is called before the first frame update
    void Start()
    {
        Rigid = GetComponent<Rigidbody>();
        // Make the rigid body not change rotation
        if (Rigid)
            Rigid.freezeRotation = true;
        originalRotation = transform.localRotation;

        jump = new Vector3(0.0f, 2.0f, 0.0f);

        waterHeight = GameObject.FindGameObjectsWithTag("Water")[0].transform.position.y;
        waterHeight += waterHeight*0.035f;
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void SetNormal () {
        RenderSettings.fogColor = normalColor;
        RenderSettings.fogDensity = 0.001f;
 
    }
 
    void SetUnderwater () {
        RenderSettings.fogColor = underwaterColor;
        RenderSettings.fogDensity = 0.1f;
 
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle <= -360F)
         angle += 360F;
        if (angle >= 360F)
         angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

}
