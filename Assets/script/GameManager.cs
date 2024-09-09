using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform ground; // 地面の位置
    //public GameObject ground;
    public PlayerPower power;
    public Image gameOverImage;
    public Text gameOvertext;
    public bool isGameClear;
    public Transform ballTrn;

    public GameObject ballObj;
    public GameObject ballTran;
    public Transform blockObj;

    public GameObject ballPrefab;
    public Vector3 spawnPosition = Vector3.zero; // ボールの生成位置



    private void Start()
    {
        //StartGame();
    }

    private void Update()
    {
        if (blockObj.childCount == 0 && !isGameClear)
        {
            isGameClear = true;
            GameClear();
        }
    }

    public void StartGame()
    {
        ballTrn = GameObject.FindWithTag("ballObj").transform;
        for (int i = 0; i < power.initialBallCount; i++)
        {
            // ボールのインスタンスを作成
            GameObject newball = Instantiate(ballPrefab, new Vector3(0,-700,0), Quaternion.identity);
            newball.transform.SetParent(ballTrn, false); // canvasTran は 
            // ワールド座標を保持するために、position を再度設定
            //newball.transform.position = transform.position;
        }

        Transform parent = ballObj.transform;
        float xDirection = Random.Range(-1f, 1f); // 左右ランダム
        for (int i = 0;i < parent.childCount;i++)
        {
            Transform child = parent.GetChild(i);
            BallController ball = child.gameObject.GetComponent<BallController>();
            Vector2 direction = new Vector2(xDirection, 1).normalized; // 上方向へ
            ball.GetComponent<Rigidbody2D>().velocity = direction * ball. speed;
        }

    }
    public void GameOver()
    {
        // ゲームオーバー処理を実行
        Debug.Log("Game Over!");

        // ゲームシーンをリロードする場合
        gameOverImage.gameObject.SetActive(true);
    }



    public void GameClear()
    {
        foreach (Transform child in ballObj.transform)
        {
            // 子オブジェクトを削除
            GameObject.Destroy(child.gameObject);
        }

        // ゲームシーンをリロードする場合
        gameOvertext.text = "GameClear!";
        gameOverImage.gameObject.SetActive(true);
        // 2秒後に非アクティブにするコルーチンを開始
        StartCoroutine(DeactivateAfterDelay(2f));

    }



    public IEnumerator DeactivateAfterDelay(float delay)
    {

        yield return new WaitForSeconds(delay);
        // GameOverImageを非アクティブにする
        gameOverImage.gameObject.SetActive(false);
    }
}
