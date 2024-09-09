using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed;
    public float xMin; // �p�h���̍ŏ�X�ʒu
    public float xMax;  // �p�h���̍ő�X�ʒu
    private Rigidbody2D rb;
    public float paddleWidth = 300f; // �f�t�H���g�̉���
    public PlayerPower playerPower;
    private RectTransform  rectTransform;
    private BoxCollider2D boxCollider;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // RectTransform��BoxCollider2D�R���|�[�l���g���擾
        rectTransform = GetComponent<RectTransform>();
        boxCollider = GetComponent<BoxCollider2D>();
        // �p�h���̉�]���Œ�iZ���̉�]�𖳌����j
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        SetPaddleWidth(300 + playerPower.paddleSize);
    }

    void Update()
    {
        // �v���C���[�̓��͂��擾�i���E�L�[�܂���A�AD�L�[�j
        float input = Input.GetAxis("Horizontal");

        // ���݈ʒu���擾
        Vector2 position = rb.position;

        // ���͂Ɋ�Â��ăp�h�����ړ�
        position.x += input * speed * Time.deltaTime;
        //position.x = Mathf.Clamp(position.x, xMin, xMax);
        // �p�h����V�����ʒu�Ɉړ�
        rb.MovePosition(position);
    }

    public void SetPaddleWidth(float width)
    {
        // sizeDelta�ŉ�����ύX
        rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
        // BoxCollider2D�̃T�C�Y��ύX (�����I�ȓ����蔻��̃T�C�Y��ύX)
        boxCollider.size = new Vector2(width, boxCollider.size.y);
    }
}
