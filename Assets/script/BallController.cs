using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed;  // �{�[���̏������x
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
        // �{�[���������_���ȕ����ɔ���
        float xDirection = Random.Range(-1f, 1f); // ���E�����_��
        Vector2 direction = new Vector2(xDirection, 1).normalized; // �������
        rb.velocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // �p�h����u���b�N�ɏՓ˂����Ƃ��ɑ��x���ێ�
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
            // �n���h���i�p�h���j�̕����擾
            float paddleWidth = collision.collider.bounds.size.x;

            // �n���h���̒�������ɁA�{�[�������������ʒu���v�Z (-0.5 ~ 0.5 �͈̔�)
            float hitPoint = (transform.position.x - collision.transform.position.x) / paddleWidth;

            // �{�[���̌��݂̑��x���擾
            Vector2 currentVelocity = rb.velocity;

            // �V�������˕������v�Z
            Vector2 direction = new Vector2(hitPoint, 1).normalized;

            // ���˃x�N�g����ݒ�
            rb.velocity = direction * speed;
        }
    }
}
