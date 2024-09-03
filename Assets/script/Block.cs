using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;
using static DropItem;

public class Block : MonoBehaviour
{

    public int hp = 3; // ����HP�ݒ�
    private Image image;
    public bool dropItem;

    public DropItem item;
    private Transform dropItemTran;

    public int itemTypeChance;

    // �F�̐ݒ�
    public Color colorHP1 = Color.white; // HP 1�̐F
    public Color colorHP2 = Color.yellow; // HP 2�̐F
    public Color colorHP3 = Color.black; // HP 3�̐F


    void Start()
    {
        image = GetComponent<Image>();
        dropItemTran = GameObject.Find("dropItemObj").GetComponent<RectTransform>();
        if (image == null)
        {
            Debug.LogError("image not found on " + gameObject.name);
        }
        UpdateColor();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            TakeDamage(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            // �V�����I�u�W�F�N�g���C���X�^���X��
            if (dropItem)
            {

                // �h���b�v�A�C�e���̎�ނ�����
                int randomItemTypeChance = Random.Range(0, 100);
                if (randomItemTypeChance < itemTypeChance) // 30%�̊m���Ŋђʂ�t�^
                {
                    item.dropItemtype = ItemType.�ђ�;
                    item.image.color = Color.magenta;

                } else // �c���70%�̊m���ŕ����t�^
                {
                    item.dropItemtype = ItemType.����;
                    item.image.color = Color.green;

                }

                DropItem itemObj = Instantiate(item, transform.position, transform.rotation);
                // �e��ݒ肷��O�̈ʒu��ۑ�
                Vector3 savedPosition = itemObj.transform.position;
                // �V�����I�u�W�F�N�g�̐e��Canvas�ɐݒ�
                itemObj.transform.SetParent(dropItemTran, false);
                // ���̈ʒu���Đݒ�
                itemObj.transform.position = savedPosition;

            }
            Destroy(gameObject); // HP��0�ȉ��ɂȂ�����u���b�N��j��
        } else
        {
            UpdateColor(); // �F���X�V
        }
    }

    void UpdateColor()
    {
        
        // HP�ɉ����ĐF��ݒ�
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
                image.color = Color.clear; // HP��0�̏ꍇ�͓����ɂ���
                break;
        }
    }
}
