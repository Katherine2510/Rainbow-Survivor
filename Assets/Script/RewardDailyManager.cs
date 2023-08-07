using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RewardDailyManager : MonoBehaviour
{
    public int dailyRewardAmount = 100; // Số tiền reward hàng ngày
    public float dailyRewardInterval = 86400f; // Khoảng thời gian giữa các lần nhận reward (24 giờ)
    
    private const string lastRewardTimeKey = "LastRewardTime";
    private float timeSinceLastReward;

    private void Start()
    {
        // Lấy thông tin thời gian nhận reward gần nhất từ PlayerPrefs
        
    }
    public void RecieveReward() {
        if (PlayerPrefs.HasKey(lastRewardTimeKey))
        {
            float lastRewardTime = PlayerPrefs.GetFloat(lastRewardTimeKey);
            timeSinceLastReward = Time.time - lastRewardTime;
        }
        else
        {
            timeSinceLastReward = Mathf.Infinity;
        }

        // Kiểm tra nếu đã đủ thời gian để nhận reward mới
        if (timeSinceLastReward >= dailyRewardInterval)
        {
            // Hiển thị thông tin reward (nếu có)
            Debug.Log("Bạn đã nhận được reward hàng ngày!");

            // Gọi hàm nhận reward
            ReceiveDailyReward();
        }
        else
        {
            // Hiển thị thông tin thời gian đếm ngược đến lần nhận reward tiếp theo (nếu có)
            //float timeRemaining = dailyRewardInterval - timeSinceLastReward;
            //TimeSpan timeSpan = TimeSpan.FromSeconds(timeRemaining);
            //Debug.Log("Bạn có thể nhận reward sau: " + timeSpan.Hours + " giờ " + timeSpan.Minutes + " phút " + timeSpan.Seconds + " giây");
        }
    }


    private void ReceiveDailyReward()
    {
        // Thực hiện xử lý nhận reward
        // Ví dụ: Tăng tiền trong game cho người chơi
        // CurrencyManager.IncreaseCurrency(dailyRewardAmount);

        // Cập nhật lại thời gian nhận reward gần nhất
        PlayerPrefs.SetFloat(lastRewardTimeKey, Time.time);
        PlayerPrefs.Save();
    }

}
