using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed;
    public float xMin; // パドルの最小X位置
    public float xMax;  // パドルの最大X位置
    private Rigidbody2D rb;
    public float paddleWidth = 300f; // デフォルトの横幅
    public PlayerPower playerPower;
    private RectTransform  rectTransform;
    private BoxCollider2D boxCollider;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // RectTransformとBoxCollider2Dコンポーネントを取得
        rectTransform = GetComponent<RectTransform>();
        boxCollider = GetComponent<BoxCollider2D>();
        // パドルの回転を固定（Z軸の回転を無効化）
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        SetPaddleWidth(300 + playerPower.paddleSize);
    }

    void Update()
    {
        // プレイヤーの入力を取得（左右キーまたはA、Dキー）
        float input = Input.GetAxis("Horizontal");

        // 現在位置を取得
        Vector2 position = rb.position;

        // 入力に基づいてパドルを移動
        position.x += input * speed * Time.deltaTime;
        //position.x = Mathf.Clamp(position.x, xMin, xMax);
        // パドルを新しい位置に移動
        rb.MovePosition(position);
    }

    public void SetPaddleWidth(float width)
    {
        // sizeDeltaで横幅を変更
        rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
        // BoxCollider2Dのサイズを変更 (物理的な当たり判定のサイズを変更)
        boxCollider.size = new Vector2(width, boxCollider.size.y);
    }
}
