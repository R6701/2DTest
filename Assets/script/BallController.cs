using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed;  // ボールの初期速度
    private Rigidbody2D rb;
    public GameManager gameManager;
    private string gameManagerTag = "GameManager";

    public string ballTag = "Ball";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindGameObjectWithTag(gameManagerTag).GetComponent<GameManager>();
        StartCoroutine(LaunchBall());
    }

    IEnumerator LaunchBall()
    {

        yield return new WaitForSeconds(2f);
        // ボールをランダムな方向に発射
        float xDirection = Random.Range(-1f, 1f); // 左右ランダム
        Vector2 direction = new Vector2(xDirection, 1).normalized; // 上方向へ
        rb.velocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // パドルやブロックに衝突したときに速度を維持
        rb.velocity = rb.velocity.normalized * speed;

        if (collision.gameObject.CompareTag("Ground"))
        {

            GameObject[] balls = GameObject.FindGameObjectsWithTag(ballTag);

            if (balls.Length <= 1)
            {
                Debug.Log("not found");
                gameManager.GameOver();
            } else
            {
                Debug.Log("found");
            }
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Paddle"))
        {
            // ハンドル（パドル）の幅を取得
            float paddleWidth = collision.collider.bounds.size.x;

            // ハンドルの中央を基準に、ボールが当たった位置を計算 (-0.5 ~ 0.5 の範囲)
            float hitPoint = (transform.position.x - collision.transform.position.x) / paddleWidth;

            // ボールの現在の速度を取得
            Vector2 currentVelocity = rb.velocity;

            // 新しい反射方向を計算
            Vector2 direction = new Vector2(hitPoint, 1).normalized;

            // 反射ベクトルを設定
            rb.velocity = direction * speed;
        }
    }
}
