using UnityEngine;
using static DropItem;
using static Unity.Collections.AllocatorManager;

public class BlockSpawner : MonoBehaviour
{

    public int chancePercentage;//�A�C�e�����o������m��

    public Block blockPrefab; // �u���b�N�̃v���n�u
    public int columns = 4;        // ���ɕ��ׂ�u���b�N�̐�
    public int rows = 8;           // �c�ɕ��ׂ�u���b�N�̐�
    public float blockWidth = 270f; // �u���b�N�̉���
    public float blockHeight = 100f; // �u���b�N�̏c��
    public Vector2 startPosition = new Vector2(0f, 0f); // �ŏ��̃u���b�N�̈ʒu
    public GameObject parentObj;

    void Start()
    {
        SpawnBlocks();
    }

    void SpawnBlocks()
    {
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                // �e�u���b�N�̈ʒu���v�Z
                Vector2 position = new Vector2(startPosition.x + x * blockWidth, startPosition.y - y * blockHeight);
                // �u���b�N�𐶐�
                Block block = Instantiate(blockPrefab, position, Quaternion.identity);
                block.transform.SetParent(parentObj.transform); // ���̃X�N���v�g���A�^�b�`���ꂽ�I�u�W�F�N�g�̎q�ɂ���

                int randomValue = Random.Range(0, 100);
                if (randomValue < chancePercentage)
                {
                    
                    block.dropItem = true;

                } else
                {
                    block.dropItem = false;
                }
            }
        }
    }
}
