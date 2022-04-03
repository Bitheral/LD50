using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Asteroid : MonoBehaviour
{
    private float timeAlive = 0;
    public float ttl = 120;
    new Rigidbody rigidbody;

    void Start()
    {
        float randomScale = Random.Range(0.1f, 1);
        transform.localScale = Vector3.one * randomScale;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.mass = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeAlive >= ttl) Destroy(gameObject);
        timeAlive += Time.deltaTime;

        transform.LookAt(GameManager.earthTransform);
        rigidbody.AddForce(transform.forward * rigidbody.mass);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward);
        Gizmos.DrawRay(transform.position, rigidbody.velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
