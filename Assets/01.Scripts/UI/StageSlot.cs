using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSlot : MonoBehaviour
{
    [SerializeField] private Image highlight;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI stageName;

    public int index;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        highlight = transform.FindChild<Image>("StageHighlight");
        icon = transform.FindChild<Image>("StageIcon");
        stageName = transform.FindChild<TextMeshProUGUI>("StageName");
        SlotReset();
    }

    public void SetIndex(int index)
    {
        this.index = index;
    }

    public void Select()
    {
        highlight.gameObject.SetActive(true);
    }

    public void SlotReset()
    {
        highlight.gameObject.SetActive(false);
    }
}
