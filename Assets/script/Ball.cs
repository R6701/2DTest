using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject ballPrefab; // 分裂時に生成するボールのプレハブ
    public Transform ballTrn;
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ballTrn = GameObject.Find("ballObj").transform;        //SplitBall();
    }


    // 分裂する処理
    public void SplitBall()
    {

        if(rb == null)
        {
            Debug.Log("rb null");
        }
        // 現在の進行方向を取得
        Vector2 currentDirection = rb.velocity.normalized;

        // 30度右に傾けた進行方向
        Vector2 directionRight = Quaternion.Euler(0, 0, 30) * currentDirection;
        // 30度左に傾けた進行方向
        Vector2 directionLeft = Quaternion.Euler(0, 0, -30) * currentDirection;

        // 分裂したボールを生成
        CreateSplitBall(directionRight);
        CreateSplitBall(directionLeft);

        Destroy(gameObject);
    }

    void CreateSplitBall(Vector2 direction)
    {

        Debug.Log("CreateSplitBall" + direction);
        // 新しいボールを生成
        GameObject newBall = Instantiate(ballPrefab, transform.position, transform.rotation);

        // 新しいボールのRigidbody2Dを取得
        Rigidbody2D newRb = newBall.GetComponent<Rigidbody2D>();
        // ボールをCanvasの子に設定し、ワールド座標を維持する
        newBall.transform.SetParent(ballTrn, false); // canvasTran は Canvas の Transform への参照

        // ワールド座標を保持するために、position を再度設定
        newBall.transform.position = transform.position;
        // 新しい進行方向を設定
        newRb.velocity = direction * rb.velocity.magnitude; // 速度は元のボールと同じ
    }

}
