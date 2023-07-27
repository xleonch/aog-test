using Code.Inventory;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RewardView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _rewardIcon;
    [SerializeField] private float _longTapThreshold = 0.5f;

    private bool _isPointerDown;
    private float _pointerDownStartTime;

    public event UnityAction LongTapStarted, LongTapEnded;

    private void Update()
    {
        if (_isPointerDown && Time.time - _pointerDownStartTime >= _longTapThreshold)
            LongTapStarted?.Invoke();
    }

    public void DrawIcon(InventoryItem item)
    {
        _rewardIcon.sprite = item.Icon;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPointerDown = true;
        _pointerDownStartTime = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPointerDown = false;

        if (Time.time - _pointerDownStartTime >= _longTapThreshold)
            LongTapEnded?.Invoke();
    }
}