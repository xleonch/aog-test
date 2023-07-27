using Code.Inventory;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class TooltipWindow : BaseWindow
{
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private Image _rewardIcon;

    public void DrawToolTip(InventoryItem item)
    {
        _titleText.text = item.Title;
        _descriptionText.text = item.Description;
        _rewardIcon.sprite = item.Icon;
    }

    protected override void OnShow(object[] args)
    {
    }

    protected override void OnHide()
    {
    }
}
