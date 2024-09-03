using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropItem : MonoBehaviour
{

    public float fallSpeed = 5f; // 下に動く速度
    public ItemType dropItemtype;
    public Color dropColor;
    public Image image;

    public enum ItemType{
        貫通,
        分裂
    }

    
    // Start is called before the first frame update
    void Start()
    {
        // Updateメソッドで下に動かす
        StartCoroutine(MoveDown());
    }

    IEnumerator MoveDown()
    {
        while (true)
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
            yield return null; // 次のフレームまで待機
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        // 衝突したオブジェクトがPaddleの場合
        if (collision.gameObject.name == "Paddle")
        {
            switch (dropItemtype)
            {
                case ItemType.貫通:
                    GameObject[] blockobjs = GameObject.FindGameObjectsWithTag("Block");
                    // すべてのColliderのisTriggerをtrueに設定
                    foreach (GameObject block in blockobjs)
                    {
                        block.GetComponent<BoxCollider2D>().isTrigger = true;
                    }
                    break;
                case ItemType.分裂:

                    // ボールを削除する
                    Destroy(gameObject);

                    GameObject[] ballObjs = GameObject.FindGameObjectsWithTag("Ball");
                    // すべてのColliderのisTriggerをtrueに設定
                    foreach (GameObject ball in ballObjs)
                    {
                        ball.GetComponent<Ball>().SplitBall();
                    }
                    break; // 早期リターンで二重処理を防ぐ
            }
        }

        // ボールを削除する
        Destroy(gameObject);
/*        if (collision.gameObject.name == "underground")
        {
            Destroy(gameObject);
        }*/
    }
}
