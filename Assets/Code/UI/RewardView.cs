using Code.Inventory;
using UnityEngine;
using UnityEngine.UI;

public class RewardView : MonoBehaviour
{
    [SerializeField] 
    Image _rewardIcon;

    public void Draw(InventoryItem item)
    {
        _rewardIcon.sprite = item.Icon;
    }
}
