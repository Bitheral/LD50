// Thank you to Veli V for creating the foundation of this script
// This script has been modified from the original found at:
// http://web.archive.org/web/20180203070327/http://wiki.unity3d.com/index.php/MouseOrbitImproved#Code_C.23


using UnityEngine;
using System.Collections;

public class OrbitCamera : MonoBehaviour
{

    public Transform target;
    public float distance = 5.0f;
    public float mouseSensitivity = 10;

    public float clampedY = 90;

    public float distanceSpeed = 1000;
    public float distanceMin = .5f;
    public float distanceMax = 15f;

    float x = 0.0f;
    float y = 0.0f;

    public MouseButton mouseMoveButton;

    // Use this for initialization
    void Start()
    {
        if (target) GameManager.earthTransform = target;

        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void LateUpdate()
    {
        if (target)
        {
            if (Input.GetMouseButton((int)mouseMoveButton))
            {
                x += Input.GetAxis("Mouse X") * mouseSensitivity * distance * 0.02f;
                y -= Input.GetAxis("Mouse Y") * mouseSensitivity * 0.02f;
            }


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit constructionHit, 10000) && Input.GetMouseButtonDown((int)MouseButton.RIGHT))
            {
                Construction.createConstruction(ConstructionType.COMMUNICATIONS, 1, constructionHit);
            }

            y = ClampAngle(y, -clampedY, clampedY);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * distanceSpeed, distanceMin, distanceMax);

            RaycastHit hit;
            if (Physics.Linecast(target.position, transform.position, out hit))
            {
                if(hit.transform.tag == "Planet") distance -= hit.distance;
            }
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}

public enum MouseButton
{
    LEFT = 0,
    RIGHT = 1,
    MIDDLE = 2
}