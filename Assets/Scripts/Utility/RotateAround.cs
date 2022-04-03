// Thank you to robertbu for the code to create a simple orbit path
// around a target
//
// http://answers.unity.com/answers/463742/view.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public Transform target;
    public Vector3 axis;
    public float radius = 10;
    public float orbitSpeed = 0.5f;
    public float rotationSpeed = 1.01f;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = (transform.position - target.position).normalized * radius + target.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.position, axis, rotationSpeed * Time.deltaTime);
        Vector3 desiredPosition = (transform.position - target.position).normalized * radius + target.position;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * orbitSpeed);
    }
}
