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

    private readonly List<RewardView> _rewardViews = new List<RewardView>();

    private float _rewardWidth;

    protected override void Awake()
    {
        base.Awake();
        _closeButton.onClick.AddListener(Hide);
        _rewardWidth = _rewardViewPrefab.GetComponent<RectTransform>().sizeDelta.x;
    }

    protected override void OnShow(object[] args)
    {
        _titleText.text = (string)args[0];
        var items = (List<InventoryItem>)args[1];

        if (items == null) 
            return;

        CreateRewardViews(items);
        AdjustScrollContent();
    }

    protected override void OnHide()
    {
        foreach (var rewardView in _rewardViews)
            DestroyImmediate(rewardView.gameObject);
        _rewardViews.Clear();

        _scrollContent.anchoredPosition = Vector2.zero;
    }

    private void CreateRewardViews(List<InventoryItem> items)
    {
        foreach (var item in items)
        {
            var prefab = Instantiate(_rewardViewPrefab, _scrollContent);
            prefab.DrawIcon(item);
            prefab.LongTapStarted += () => OnLongTapStarted(item);
            prefab.LongTapEnded += OnLongTapEnded;
            _rewardViews.Add(prefab);
        }
    }

    private void OnLongTapStarted(InventoryItem item)
    {
        var tooltip = Get<TooltipWindow>();
        tooltip.DrawToolTip(item);
        tooltip.Show();
    }

    private void OnLongTapEnded()
    {
        Get<TooltipWindow>().Hide();
    }

    private void AdjustScrollContent()
    {
        var scrollWidth = _scrollRect.viewport.rect.width;
        var contentWidth = _rewardViews.Count * _rewardWidth;
        
        // Включаем/выключаем скролл, выставляем анчор для контента в скролле (left, center)
        _scrollRect.enabled = contentWidth > scrollWidth;
        _scrollContent.anchorMin = _scrollRect.enabled ? new Vector2(0, 0.5f) : new Vector2(0.5f, 0.5f);
        _scrollContent.anchorMax = _scrollRect.enabled ? new Vector2(0, 0.5f) : new Vector2(0.5f, 0.5f);
        _scrollContent.pivot = _scrollRect.enabled ? new Vector2(0, 0.5f) : new Vector2(0.5f, 0.5f);
    }
}