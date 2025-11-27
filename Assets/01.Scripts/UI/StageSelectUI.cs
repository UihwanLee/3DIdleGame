using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectUI : MonoBehaviour
{
    [SerializeField] private GameObject window;
    [SerializeField] private List<StageSlot> stages = new List<StageSlot>();
    [SerializeField] private Button stageSelectBtn;

    private StageSlot currentStageSlot;

    private void Awake()
    {
        window = GameObject.Find("StageSelectWindow");

        stageSelectBtn = transform.FindChild<Button>("StageSelectBtn");
        if (stageSelectBtn != null) stageSelectBtn.onClick.AddListener(() => LoadStage());

        for(int i=0; i<stages.Count; i++)
        {
            StageSlot stage = stages[i];
            stages[i].SetIndex(i);
            stages[i].GetComponent<Button>().onClick.AddListener(() => SelectStage(stage));
        }
    }

    private void Start()
    {
        window.SetActive(false);
    }

    public void OpenSelectMap()
    {
        window.SetActive(true);
    }

    public void SelectStage(StageSlot stage)
    {
        if(currentStageSlot != null)
        {
            currentStageSlot.SlotReset();
        }

        currentStageSlot = stage;
        currentStageSlot.Select();
    }

    public void LoadStage()
    {
        if(currentStageSlot != null)
        {
            this.window.SetActive(false);
            SceneManager.LoadScene(currentStageSlot.index);
        }
    }
}
