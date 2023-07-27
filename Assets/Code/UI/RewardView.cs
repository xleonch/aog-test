using Code.Inventory;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RewardView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _rewardIcon;
    [SerializeField] private float longTapThreshold = 0.5f;

    private bool pointerDown;
    private float pointerDownTime;

    public event UnityAction LongTapStarted, LongTapEnded;

    private void Update()
    {
        if (pointerDown && Time.time - pointerDownTime >= longTapThreshold)
            LongTapStarted?.Invoke();
    }

    public void DrawIcon(InventoryItem item)
    {
        _rewardIcon.sprite = item.Icon;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
        pointerDownTime = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointerDown = false;

        if (Time.time - pointerDownTime >= longTapThreshold)
            LongTapEnded?.Invoke();
    }
}
