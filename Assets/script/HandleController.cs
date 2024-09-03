using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed;
    public float xMin; // �p�h���̍ŏ�X�ʒu
    public float xMax;  // �p�h���̍ő�X�ʒu
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // �p�h���̉�]���Œ�iZ���̉�]�𖳌����j
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        // �v���C���[�̓��͂��擾�i���E�L�[�܂���A�AD�L�[�j
        float input = Input.GetAxis("Horizontal");

        // ���݈ʒu���擾
        Vector2 position = rb.position;

        // ���͂Ɋ�Â��ăp�h�����ړ�
        position.x += input * speed * Time.deltaTime;

        // �ړ��͈͂𐧌�
        position.x = Mathf.Clamp(position.x, xMin, xMax);

        // �p�h����V�����ʒu�Ɉړ�
        rb.MovePosition(position);
    }
}
