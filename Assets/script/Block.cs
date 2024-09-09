using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;
using static DropItem;
using static PlayerPower;

public class Block : MonoBehaviour
{

    public int hp = 3; // 初期HP設定
    private Image image;
    public bool dropItem;
    public Text hpText;

    public PlayerPower playerPower;
    public DropItem item;
    private Transform dropItemTran;

    public int itemTypeChance;

    // 色の設定
/*    public Color colorHP1 = Color.white; // HP 1の色
    public Color colorHP2 = Color.yellow; // HP 2の色
    public Color colorHP3 = Color.black; // HP 3の色*/


    void Start()
    {
        image = GetComponent<Image>();
        playerPower = GameObject.Find("PlayerDeta").GetComponent<PlayerPower>();

        //PlayerPower コンポーネントを取得
        if (playerPower == null)
        {
            Debug.LogError("PlayerPower component is not attached to " + gameObject.name);
        }

        dropItemTran = GameObject.Find("dropItemObj").GetComponent<RectTransform>();
        if (image == null)
        {
            Debug.LogError("image not found on " + gameObject.name);
        }
        hpText.text = hp.ToString();
        //UpdateColor();
    }


    //通常時
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("playerPower.ballpower" + playerPower.ballPower);
            TakeDamage(1 + playerPower.ballPower,collision.collider);
        }
    }

    //貫通時
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            TakeDamage(1 + playerPower.ballPower, collision);
        }
    }


    
    public void TakeDamage(int damage, Collider2D collider)
    {

        //クリティカル判定
        Ball ball = collider.gameObject.GetComponent<Ball>();
        if (ball.isCritical)
        {
            hp -= damage * 2;
        } else
        {
            hp -= damage;
        }
        
        if (hp <= 0)
        {
            // 新しいオブジェクトをインスタンス化
            if (dropItem)
            {

                // ドロップアイテムの種類を決定
                int randomItemTypeChance = Random.Range(0, 100);
                if (randomItemTypeChance < itemTypeChance) // 30%の確率で貫通を付与
                {
                    item.dropItemtype = ItemType.貫通;
                    item.image.color = Color.magenta;

                } else // 残りの70%の確率で分裂を付与
                {
                    item.dropItemtype = ItemType.分裂;
                    item.image.color = Color.green;

                }

                DropItem itemObj = Instantiate(item, transform.position, transform.rotation);
                // 親を設定する前の位置を保存
                Vector3 savedPosition = itemObj.transform.position;
                // 新しいオブジェクトの親をCanvasに設定
                itemObj.transform.SetParent(dropItemTran, false);
                // 元の位置を再設定
                itemObj.transform.position = savedPosition;

            }
            playerPower.coin = ++playerPower.coin;
            PlayerPrefs.SetInt(PLAYER_DETA.Coin.ToString(), playerPower.coin);
            PlayerPrefs.Save();
            Destroy(gameObject); // HPが0以下になったらブロックを破壊
        }                                                                   
        hpText.text = hp.ToString();
        //クリティカル判定のオフ
        collider.gameObject.GetComponent<Image>().color = Color.white;
        ball.isCritical = false;
    }

/*    void UpdateColor()
    {
        
        // HPに応じて色を設定
        switch (hp)
        {
            case 1:
                image.color = colorHP1;
                break;
            case 2:
                image.color = colorHP2;
                break;
            case 3:
                image.color = colorHP3;
                break;
            default:
                image.color = Color.clear; // HPが0の場合は透明にする
                break;
        }
    }*/
}
