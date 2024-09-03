using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform ground; // 地面の位置
    //public GameObject ground;
    public Image gameOverImage;
    public Text gameOvertext;
    public bool isGameClear;


    public GameObject ballObj;
    public Transform blockObj;


    private void Update()
    {
        if (blockObj.childCount == 0 && !isGameClear)
        {
            isGameClear = true;
            GameClear();
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
