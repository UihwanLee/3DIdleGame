using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSlotManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private SkillSO[] skillArray;
    [SerializeField] private Transform slotParent;

    // Resources 폴더 내부 경로
    private const string SkillSlotPrefabResourcePath = "Skill/Prefab";
    private const string SkillSOResourcePath = "Skill/SO";

    private void Start()
    {
        Initialize();
        GenerateSkillSlot();
    }

    private void Initialize()
    {
        // 슬롯 parent 초기화
        slotParent = transform.FindChild<Transform>("SkillSlots");

        // skillList 초기화 -> Resources 폴더 내 /SO/Skill 내 있는 모든 SkillSO 가져오기
        skillArray = Resources.LoadAll<SkillSO>(SkillSOResourcePath);

        Debug.Log($"{slotPrefab.name} 로드 완료");
        Debug.Log($"{skillArray.Length}개의 SkillSO 로드 완료");
    }

    private void GenerateSkillSlot()
    {
        if (slotPrefab == null) return;

        ISkillCaster caster = GameManager.Instance.Player.SkillController.GetComponent<ISkillCaster>();

        for (int i=0;  i<skillArray.Length; i++)
        {
            SkillSlot slot = Instantiate(slotPrefab, slotParent).GetComponent<SkillSlot>();
            slot.SetSkill(skillArray[i]);
            slot.SetSkillCaster(caster);
            slot.Initialize();
        }
    }
}
