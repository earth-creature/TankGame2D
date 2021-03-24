using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 1f;
    [SerializeField] int bounceRemain = 2;

    void Update()
    {
        transform.Translate ( Vector3.right * bulletSpeed, Space.Self);
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2d called");

        float rot = transform.localEulerAngles.z;

        if (collision.gameObject.tag == "HorizontalCollider")
        {
            BounceCheck();
            transform.rotation = Quaternion.Euler ( 0f, 0f, 360f - rot);
            Debug.Log("1 called");
        }
        else if (collision.gameObject.tag == "VerticalCollider")
        {
            BounceCheck();
            transform.rotation = Quaternion.Euler ( 0f, 0f, 180f - rot);
            Debug.Log("2 called");
        }
    }

    void BounceCheck()
    {
        if ( bounceRemain == 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            bounceRemain -= 1;
        }
    }
}
