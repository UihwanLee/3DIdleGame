using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Damage를 표시하기 위해 Damage Prefab을 저장한 Pool
/// </summary>
public class FloatingTextPool : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _maxCount;
    [SerializeField] private Transform _canvasParent;
    [SerializeField] private FloatingText _data;

    private string _key;
    private PoolManager _poolManager;
    private FloatingTextPoolManager _manager;

    private Coroutine _fadeCoroutine;
    private WaitForSeconds waitForSeconds;

    private void Start()
    {
        _poolManager = PoolManager.Instance;
        _manager = FloatingTextPoolManager.Instance;
        Initialize();
    }

    private void Initialize()
    {
        // Key 저장
        _key = _data.textName;

        // Damage 생성
        _poolManager.CreatePool(_key, _prefab, _maxCount, _canvasParent);

        // 코루틴 변수
        waitForSeconds = new WaitForSeconds(_data.duration);

        // Pool 전달
        _manager.Add(this);
    }

    public GameObject SpawnText(string text, Transform target, Color? color = null)
    {
        // Text를 세팅하고 넘기기
        GameObject newText = _poolManager.GetObject(_key);

        if(newText != null)
        {
            if(newText.TryGetComponent<Text>(out Text floatingText))
            {
                // 텍스트 설정
                floatingText.text = text;

                // 색상 설정
                Color newColor = (Color)((color != null) ? color : _data.color);
                newColor.a = 1.0f;
                floatingText.color = newColor;

                // 위치 설정
                Vector3 screenPosition = Camera.main.WorldToScreenPoint(target.position);
                floatingText.GetComponent<RectTransform>().position = screenPosition;
            }

            // Floating 코루틴은 몇 초 뒤에 자동으로 반환
            StartFadeCoroutine(newText);
        }

        return newText;
    }

    private void StartFadeCoroutine(GameObject obj)
    {
        if (_fadeCoroutine != null) StopCoroutine(_fadeCoroutine);
        _fadeCoroutine = StartCoroutine(FadeCoroutine(obj));
    }

    private IEnumerator FadeCoroutine(GameObject obj)
    {
        float fadeTime = _data.duration;
        float curTime = 0.0f;
        Text floatingText = obj.GetComponent<Text>();
        Color tempColor = floatingText.color;

        while (curTime < fadeTime)
        {
            curTime += Time.deltaTime;

            float t = (curTime / fadeTime);

            // alpha 값 수정
            float alpha = 1f - t;
            tempColor.a = alpha;
            floatingText.color = tempColor;

            // 위치 값 수정
            Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y + _data.floatingDist, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, t);
        }

        yield return new WaitForSeconds(_data.duration);

        // 반환
        Release(obj);
    }


    public void Release(GameObject obj)
    {
        _poolManager.ReleasePool(_key, obj);
    }

    #region 프로퍼티

    public string Key { get { return _key; } }
    public FloatingText Data { get { return _data; } }
    public TextType Type { get { return _data.type; } }

    #endregion
}
