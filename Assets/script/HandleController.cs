using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed;
    public float xMin; // パドルの最小X位置
    public float xMax;  // パドルの最大X位置
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // パドルの回転を固定（Z軸の回転を無効化）
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        // プレイヤーの入力を取得（左右キーまたはA、Dキー）
        float input = Input.GetAxis("Horizontal");

        // 現在位置を取得
        Vector2 position = rb.position;

        // 入力に基づいてパドルを移動
        position.x += input * speed * Time.deltaTime;

        // 移動範囲を制限
        position.x = Mathf.Clamp(position.x, xMin, xMax);

        // パドルを新しい位置に移動
        rb.MovePosition(position);
    }
}
