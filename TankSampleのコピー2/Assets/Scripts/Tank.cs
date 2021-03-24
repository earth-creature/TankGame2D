using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField] float tankSpeed = 1f;
    [SerializeField] int bulletNumMax = 5;
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] GameObject tankTurret;

    float aimAngle;
    public int bulletNumCurrent;

    private Rigidbody2D body;

    void Awake()
    {
        bulletNumCurrent = 0;
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        UpdateAim();
        Act();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        body.velocity = MakeUnitVector2() * tankSpeed;
    }

    void UpdateAim()
    {
        aimAngle = GetAimAngle( Camera.main.WorldToScreenPoint( transform.position), Input.mousePosition);
        transform.rotation = Quaternion.Euler( 0f, 0f, aimAngle);
    }

    void Act()
    {
        if (Input.GetKeyDown ( KeyCode.Mouse0))
        {
            if( bulletNumCurrent < bulletNumMax)
            {
                Instantiate( bulletPrefab, tankTurret.transform.position, Quaternion.Euler( 0f, 0f, aimAngle));
                bulletNumCurrent += 1;
            }
        }
    }

    Vector2 MakeUnitVector2()
    {
        Vector2 vec = Vector2.zero;

        if ( Input.GetKey ( KeyCode.UpArrow)){
            vec += Vector2.up;
        }
        if ( Input.GetKey ( KeyCode.DownArrow)){
            vec += Vector2.down;
        }
        if ( Input.GetKey ( KeyCode.LeftArrow)){
            vec += Vector2.left;
        }
        if ( Input.GetKey ( KeyCode.RightArrow)){
            vec += Vector2.right;
        }

        if ( vec != Vector2.zero){
            vec.Normalize();
        }

        return vec;
    }

    float GetAimAngle( Vector2 from, Vector2 to)
    {
        float dx = to.x - from.x;
        float dy = to.y - from.y;
        float rad = Mathf.Atan2(dy, dx);
        return rad * Mathf.Rad2Deg;
    }
}
