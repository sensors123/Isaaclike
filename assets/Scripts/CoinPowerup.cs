using UnityEngine;

// 把这个脚本挂到角色上
public class CoinPowerup : MonoBehaviour
{
    [Header("Stats (set base/original values here)")]
    public float tears_f = 1f;       // 例如射速相关浮点
    public float shootSpeed_i = 1f;  // 射速/射击间隔相关
    public float range_i = 1f;       // 射程

    [Header("Currency")]
    public int coins = 0;

    // 保存原始值，确保倍数是基于原始值而不是叠加
    private float baseTears;
    private float baseShootSpeed;
    private float baseRange;

    void Start()
    {
        // 每次开始时将 coin 清零，满足“每次重新开始时，coin数清零”的需求
        coins = 0;

        // 保存原始值
        baseTears = tears_f;
        baseShootSpeed = shootSpeed_i;
        baseRange = range_i;
    }

    void Update()
    {
        // 检测按下空格（按下一帧触发）
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryActivatePowerup();
        }
    }

    // 在其他地方（如吃金币时）调用这个方法来增加金币
    public void AddCoins(int amount)
    {
        coins += amount;
    }

    // 尝试使用 50 个金币激活效果
    private void TryActivatePowerup()
    {
        const int cost = 50;
        if (coins >= cost)
        {
            // 基于原始值设置新的属性，避免多次激活造成指数增长
            tears_f = baseTears * 1.5f;
            shootSpeed_i = baseShootSpeed * 1.1f;
            range_i = baseRange * 1.1f;

            coins -= cost;

            // 如果你希望效果只持续一段时间，可以在这里启动协程来恢复原始属性
            // StartCoroutine(ResetAfterSeconds(duration));
        }
        else
        {
            // 未达到 50 个金币时无效果（什么都不做）
        }
    }

    // 可选：恢复到原始属性的实现示例（如需要）
    // private IEnumerator ResetAfterSeconds(float seconds)
    // {
    //     yield return new WaitForSeconds(seconds);
    //     tears_f = baseTears;
    //     shootSpeed_i = baseShootSpeed;
    //     range_i = baseRange;
    // }

    // 外部重置（例如玩家重新开始时可以调用）
    public void ResetCoinsAndStats()
    {
        coins = 0;
        tears_f = baseTears;
        shootSpeed_i = baseShootSpeed;
        range_i = baseRange;
    }
}
