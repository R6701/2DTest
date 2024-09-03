using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropItem : MonoBehaviour
{

    public float fallSpeed = 5f; // ���ɓ������x
    public ItemType dropItemtype;
    public Color dropColor;
    public Image image;

    public enum ItemType{
        �ђ�,
        ����
    }

    
    // Start is called before the first frame update
    void Start()
    {
        // Update���\�b�h�ŉ��ɓ�����
        StartCoroutine(MoveDown());
    }

    IEnumerator MoveDown()
    {
        while (true)
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
            yield return null; // ���̃t���[���܂őҋ@
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        // �Փ˂����I�u�W�F�N�g��Paddle�̏ꍇ
        if (collision.gameObject.name == "Paddle")
        {
            switch (dropItemtype)
            {
                case ItemType.�ђ�:
                    GameObject[] blockobjs = GameObject.FindGameObjectsWithTag("Block");
                    // ���ׂĂ�Collider��isTrigger��true�ɐݒ�
                    foreach (GameObject block in blockobjs)
                    {
                        block.GetComponent<BoxCollider2D>().isTrigger = true;
                    }
                    break;
                case ItemType.����:

                    // �{�[�����폜����
                    Destroy(gameObject);

                    GameObject[] ballObjs = GameObject.FindGameObjectsWithTag("Ball");
                    // ���ׂĂ�Collider��isTrigger��true�ɐݒ�
                    foreach (GameObject ball in ballObjs)
                    {
                        ball.GetComponent<Ball>().SplitBall();
                    }
                    break; // �������^�[���œ�d������h��
            }
        }

        // �{�[�����폜����
        Destroy(gameObject);
/*        if (collision.gameObject.name == "underground")
        {
            Destroy(gameObject);
        }*/
    }
}
