using System.Collections.Generic;
using Code.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardsWindow : BaseWindow
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private RewardView _rewardViewPrefab;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private RectTransform _scrollContent;
    
    protected override void Awake()
    {
        base.Awake();
        _closeButton.onClick.AddListener(Hide);
    }

    protected override void OnShow(object[] args)
    {
        _titleText.text = (string)args[0];
        var items = (List<InventoryItem>)args[1];

        foreach (var item in items)
        {
            var prefab = Instantiate(_rewardViewPrefab, _scrollContent);
            prefab.DrawIcon(item);
            prefab.LongTapStarted += () =>
            {
                var tooltip = Get<TooltipWindow>();
                tooltip.Show();
                tooltip.DrawToolTip(item);
            };
            prefab.LongTapEnded += () => Get<TooltipWindow>().Hide();
        }

        AdjustLayoutGroup();
    }

    protected override void OnHide()
    {
        foreach (RectTransform child in _scrollContent.transform)
            Destroy(child.gameObject);

        _scrollContent.anchoredPosition = Vector2.zero;
    }

    void AdjustLayoutGroup()
    {
        var scrollWidth = _scrollRect.viewport.rect.width;
        var childCount = _scrollContent.childCount;
        var contentWidth = 0f;

        for (var i = 0; i < childCount; i++)
        {
            var child = _scrollContent.GetChild(i) as RectTransform;
            contentWidth += child!.sizeDelta.x;
        }
        
        _scrollRect.enabled = contentWidth > scrollWidth;
        
        if (_scrollRect.enabled)
        {
            _scrollContent.anchorMin = new Vector2(0, 0.5f);
            _scrollContent.anchorMax = new Vector2(0, 0.5f);
            _scrollContent.pivot = new Vector2(0, 0.5f);
        }
        else
        {
            _scrollContent.anchorMin = new Vector2(0.5f, 0.5f);
            _scrollContent.anchorMax = new Vector2(0.5f, 0.5f);
            _scrollContent.pivot = new Vector2(0.5f, 0.5f);
        }
    }
}