using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject tankTurret;

    float aimAngle;

    void Start()
    {

    }

    void Update()
    {
        aimAngle = GetAim( Camera.main.WorldToScreenPoint( transform.position), Input.mousePosition);

        Move();
        Aim();
        if (Input.GetKeyDown ( KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate( bullet, tankTurret.transform.position, Quaternion.Euler ( 0f, 0f, aimAngle));
    }

    void Aim()
    {
        transform.rotation = Quaternion.Euler( 0f, 0f, aimAngle);
    }

    float GetAim( Vector2 from, Vector2 to)
    {
        float dx = to.x - from.x;
        float dy = to.y - from.y;
        float rad = Mathf.Atan2(dy, dx);
        return rad * Mathf.Rad2Deg;
    }

    void Move()
    {
        transform.Translate ( MakeUnitVector() * moveSpeed, Space.World);
    }

    Vector3 MakeUnitVector()
    {
        Vector3 vec = Vector3.zero;

        if ( Input.GetKey ( KeyCode.UpArrow))
        {
            vec += Vector3.up;
        }
        if ( Input.GetKey ( KeyCode.DownArrow))
        {
            vec += Vector3.down;
        }
        if ( Input.GetKey ( KeyCode.LeftArrow))
        {
            vec += Vector3.left;
        }
        if ( Input.GetKey ( KeyCode.RightArrow))
        {
            vec += Vector3.right;
        }

        if ( vec != Vector3.zero)
        {
            vec.Normalize();
        }

        return vec;
    }
}
