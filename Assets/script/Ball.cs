using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject ballPrefab; // ���􎞂ɐ�������{�[���̃v���n�u
    public Transform ballTrn;
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ballTrn = GameObject.Find("ballObj").transform;        //SplitBall();
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

}
