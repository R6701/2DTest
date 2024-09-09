using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform ground; // �n�ʂ̈ʒu
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
    public Vector3 spawnPosition = Vector3.zero; // �{�[���̐����ʒu



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
            // �{�[���̃C���X�^���X���쐬
            GameObject newball = Instantiate(ballPrefab, new Vector3(0,-700,0), Quaternion.identity);
            newball.transform.SetParent(ballTrn, false); // canvasTran �� 
            // ���[���h���W��ێ����邽�߂ɁAposition ���ēx�ݒ�
            //newball.transform.position = transform.position;
        }

        Transform parent = ballObj.transform;
        float xDirection = Random.Range(-1f, 1f); // ���E�����_��
        for (int i = 0;i < parent.childCount;i++)
        {
            Transform child = parent.GetChild(i);
            BallController ball = child.gameObject.GetComponent<BallController>();
            Vector2 direction = new Vector2(xDirection, 1).normalized; // �������
            ball.GetComponent<Rigidbody2D>().velocity = direction * ball. speed;
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
