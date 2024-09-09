using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public GameObject ballPrefab; // ���􎞂ɐ�������{�[���̃v���n�u
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
            // �_���[�W�v�Z����
            ApplyDamage(playerPower.criticalChance);
        }
    }

    void ApplyDamage(float critChance)
    {
        if (Random.value < critChance) // 0.0����1.0�̃����_���l��critChance�ȉ��̏ꍇ
        {
             // �_���[�W��{�ɂ���
            Debug.Log($"Critical hit! Damage dealt");
            gameObject.GetComponent<Image>().color = UnityEngine.Color.red;
            isCritical = true;
        } else
        {
            Debug.Log($"Normal hit. Damage dealt");
            isCritical = false;
        }
    }

    // ���􂷂鏈��
    public void SplitBall()
    {

        if(rb == null)
        {
            Debug.Log("rb null");
        }
        // ���݂̐i�s�������擾
        Vector2 currentDirection = rb.velocity.normalized;

        // 30�x�E�ɌX�����i�s����
        Vector2 directionRight = Quaternion.Euler(0, 0, 30) * currentDirection;
        // 30�x���ɌX�����i�s����
        Vector2 directionLeft = Quaternion.Euler(0, 0, -30) * currentDirection;

        // ���􂵂��{�[���𐶐�
        CreateSplitBall(directionRight);
        CreateSplitBall(directionLeft);

        Destroy(gameObject);
    }

    void CreateSplitBall(Vector2 direction)
    {

        Debug.Log("CreateSplitBall" + direction);
        // �V�����{�[���𐶐�
        GameObject newBall = Instantiate(ballPrefab, transform.position, transform.rotation);

        // �V�����{�[����Rigidbody2D���擾
        Rigidbody2D newRb = newBall.GetComponent<Rigidbody2D>();
        // �{�[����Canvas�̎q�ɐݒ肵�A���[���h���W���ێ�����
        newBall.transform.SetParent(ballTrn, false); // canvasTran �� Canvas �� Transform �ւ̎Q��

        // ���[���h���W��ێ����邽�߂ɁAposition ���ēx�ݒ�
        newBall.transform.position = transform.position;
        // �V�����i�s������ݒ�
        newRb.velocity = direction * rb.velocity.magnitude; // ���x�͌��̃{�[���Ɠ���
    }


    public void SetBallSize(float size)
    {
        // �{�[���̕\���T�C�Y�Ɠ����蔻��̃T�C�Y��ݒ�
        transform.localScale = new Vector3(size, size, 1);  // �\���T�C�Y�̒���
        //circleCollider.radius = size / 2;  // Collider�̔��a��ݒ�
    }
}
