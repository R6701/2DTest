using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public GameObject ballPrefab; // 分裂時に生成するボールのプレハブ
    public Transform ballTrn;
    public Rigidbody2D rb;
    private RectTransform rectTransform;
    public CircleCollider2D circleCollider;
    public PlayerPower playerPower;
    public bool isCritical;
    public Image image;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        image = GetComponent<Image>();
        ballTrn = GameObject.Find("ballObj").transform;        //SplitBall();
        rectTransform = GetComponent<RectTransform>();
        playerPower = GameObject.Find("PlayerDeta").GetComponent<PlayerPower>();
        SetBallSize(1 + playerPower.ballSize);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            // ダメージ計算処理
            ApplyDamage(playerPower.criticalChance);
        }
    }

    void ApplyDamage(float critChance)
    {
        if (Random.value < critChance) // 0.0から1.0のランダム値がcritChance以下の場合
        {
             // ダメージを倍にする
            Debug.Log($"Critical hit! Damage dealt");
            gameObject.GetComponent<Image>().color = UnityEngine.Color.red;
            isCritical = true;
        } else
        {
            Debug.Log($"Normal hit. Damage dealt");
            isCritical = false;
        }
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


    public void SetBallSize(float size)
    {
        // ボールの表示サイズと当たり判定のサイズを設定
        transform.localScale = new Vector3(size, size, 1);  // 表示サイズの調整
        //circleCollider.radius = size / 2;  // Colliderの半径を設定
    }
}
