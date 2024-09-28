using UnityEngine;

public class GemFallScript : MonoBehaviour
{
    public GameObject gemPrefab; // Ngọc cộng điểm
    public GameObject bombPrefab; // Bomb die
    public GameObject badItemPrefab; // Trừ điểm
    public GameObject speedPowerUpPrefab;  // Nhân hai tốc độ
    public GameObject obstaclePrefab; // Vật cản

    public float spawnInterval = 3f;
    private float timer;

    [Range(0f, 1f)] public float bombSpawnChance = 0.2f;
    [Range(0f, 1f)] public float badItemSpawnChance = 0.15f;
    [Range(0f, 1f)] public float speedPowerUpChance = 0.1f;

    private float spawnYPosition = 6.0f;
    private float leftXPosition = -6f;
    private float centerXPosition = 0f;
    private float rightXPosition = 6f;

    private static bool obstacleExists = false;

    void Start()
    {
        if (!obstacleExists)
        {
            SpawnObstacle();
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnUniqueItems(3); // Sửa đổi để sinh các vật phẩm khác nhau
            timer = 0;
        }
    }

    // Hàm sinh 3 vật phẩm ngẫu nhiên nhưng đảm bảo có ít nhất 1 viên ngọc (gem)
    void SpawnUniqueItems(int itemCount)
    {
        GameObject[] possibleItems = { gemPrefab, bombPrefab, badItemPrefab, speedPowerUpPrefab };
        bool gemSpawned = false; // Biến kiểm tra xem gem đã xuất hiện hay chưa
        int gemCount = 0; // Đếm số gem đã sinh ra

        for (int i = 0; i < itemCount; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();

            GameObject selectedPrefab = SelectRandomItem(possibleItems, ref gemSpawned, gemCount, i == itemCount - 1);

            GameObject spawnedItem = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
            if (spawnedItem != null)
            {
                Vector3 direction = Vector3.down; // Tạo hướng rơi xuống
                SetupFallingItem(spawnedItem, direction);

                // Kiểm tra và đếm số gem đã sinh ra
                if (selectedPrefab == gemPrefab)
                {
                    gemCount++;
                }
            }
        }
    }

    // Hàm chọn một vật phẩm ngẫu nhiên đảm bảo có ít nhất 1 viên ngọc
    GameObject SelectRandomItem(GameObject[] items, ref bool gemSpawned, int gemCount, bool forceGem)
    {
        // Bắt buộc sinh gem nếu là vật phẩm cuối và chưa có gem hoặc muốn có thêm gem
        if (forceGem && gemCount == 0 || (gemCount == 1 && Random.value < 0.5f))
        {
            gemSpawned = true;
            return gemPrefab;
        }

        GameObject selectedItem;
        do
        {
            selectedItem = items[Random.Range(0, items.Length)];
        } while (selectedItem == gemPrefab && gemSpawned && gemCount >= 2); // Tránh sinh quá nhiều gem nếu đã đủ

        if (selectedItem == gemPrefab)
        {
            gemSpawned = true; // Đánh dấu là đã sinh gem
        }

        return selectedItem;
    }

    Vector3 GetRandomSpawnPosition()
    {
        int spawnDirection = Random.Range(0, 3);
        Vector3 spawnPosition = Vector3.zero;

        switch (spawnDirection)
        {
            case 0:
                spawnPosition = new Vector3(leftXPosition, spawnYPosition, 0);
                break;
            case 1:
                spawnPosition = new Vector3(centerXPosition, spawnYPosition, 0);
                break;
            case 2:
                spawnPosition = new Vector3(rightXPosition, spawnYPosition, 0);
                break;
        }

        return spawnPosition;
    }

    void SetupFallingItem(GameObject item, Vector3 direction)
    {
        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0;
            rb.velocity = direction * Random.Range(1f, 3f);
        }
    }

    void SpawnObstacle()
    {
        Vector3 obstaclePosition = new Vector3(-8f, -3.6f, 0);
        GameObject obstacle = Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity);
        ObstacleMover mover = obstacle.GetComponent<ObstacleMover>();
        if (mover != null)
        {
            mover.StartMovement(Vector2.right);
        }
        obstacleExists = true;
    }
}
