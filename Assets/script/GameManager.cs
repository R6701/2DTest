using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform ground; // �n�ʂ̈ʒu
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
        // �Q�[���I�[�o�[���������s
        Debug.Log("Game Over!");

        // �Q�[���V�[���������[�h����ꍇ
        gameOverImage.gameObject.SetActive(true);
    }



    public void GameClear()
    {
        foreach (Transform child in ballObj.transform)
        {
            // �q�I�u�W�F�N�g���폜
            GameObject.Destroy(child.gameObject);
        }

        // �Q�[���V�[���������[�h����ꍇ
        gameOvertext.text = "GameClear!";
        gameOverImage.gameObject.SetActive(true);
        // 2�b��ɔ�A�N�e�B�u�ɂ���R���[�`�����J�n
        StartCoroutine(DeactivateAfterDelay(2f));

    }



    public IEnumerator DeactivateAfterDelay(float delay)
    {

        yield return new WaitForSeconds(delay);
        // GameOverImage���A�N�e�B�u�ɂ���
        gameOverImage.gameObject.SetActive(false);
    }
}
