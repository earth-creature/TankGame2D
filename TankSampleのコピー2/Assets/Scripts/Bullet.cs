using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 1f;
    [SerializeField] int bounceNumMax = 1;

    Rigidbody2D body;
    Tank myTank;

    int bounceNumRemain;    //あと何回跳ね返るか
    bool isExist;

    void Awake()
    {
        bounceNumRemain = bounceNumMax;
        isExist = true;

        myTank = GameObject.FindWithTag("MyTank").GetComponent<Tank>();
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.velocity = GetStartVector() * bulletSpeed;
    }

    void Update()
    {

    }

    void OnTriggerExit2D( Collider2D col)
    {
        if (col.gameObject.tag == "Bullet"){
            DestroyBullet();
        }
        if (col.gameObject.tag == "MyTank"){
            //DestroyBullet();
            //Destroy( col.gameObject);
        }
        if (col.gameObject.tag == "Wall"){
            
            if ( bounceNumRemain == 0){
                DestroyBullet();
            }
            bounceNumRemain -= 1;
        }

        UpdateAnimation();
    }

    //弾の向きを移動方向に合わせる
    void UpdateAnimation()
    {
        float vx = body.velocity.x;
        float vy = body.velocity.y;
        float rad = Mathf.Atan2(vy, vx);
        transform.rotation = Quaternion.Euler ( 0f, 0f, rad * 180f / Mathf.PI);
    }

    //弾の破壊
    void DestroyBullet()
    {
        if ( isExist){
            isExist = false;
            myTank.bulletNumCurrent -= 1;
            Destroy ( this.gameObject);
        }
    }

    //発射時の向きから進む速度を単位ベクトルで求める
    Vector2 GetStartVector()
    {
        float angleRad = transform.eulerAngles.z * Mathf.PI / 180f;
        Vector2 vec = new Vector2 ( Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        return vec;
    }
}
