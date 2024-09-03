using UnityEngine;
using static DropItem;
using static Unity.Collections.AllocatorManager;

public class BlockSpawner : MonoBehaviour
{

    public int chancePercentage;//アイテムが出現する確率

    public Block blockPrefab; // ブロックのプレハブ
    public int columns = 4;        // 横に並べるブロックの数
    public int rows = 8;           // 縦に並べるブロックの数
    public float blockWidth = 270f; // ブロックの横幅
    public float blockHeight = 100f; // ブロックの縦幅
    public Vector2 startPosition = new Vector2(0f, 0f); // 最初のブロックの位置
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
                // 各ブロックの位置を計算
                Vector2 position = new Vector2(startPosition.x + x * blockWidth, startPosition.y - y * blockHeight);
                // ブロックを生成
                Block block = Instantiate(blockPrefab, position, Quaternion.identity);
                block.transform.SetParent(parentObj.transform); // このスクリプトがアタッチされたオブジェクトの子にする

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
