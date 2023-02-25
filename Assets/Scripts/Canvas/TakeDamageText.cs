using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageText : MonoBehaviour
{
    [SerializeField]
    CanvasGroup _cg;
    [SerializeField]
    TMPro.TMP_Text _text;
    [SerializeField]
    RectTransform _rt;
    // Start is called before the first frame update
    void Start()
    {
        if (_cg == null)
        {
            _cg = GetComponent<CanvasGroup>();
        }
        if (_rt == null)
        {
            _rt = GetComponent<RectTransform>();
        }
        if (_text == null)
        {
            _text = GetComponent<TMPro.TMP_Text>();
        }
        StartCoroutine("FadeUp");
    }

    public void SetLocation(Vector2 newLoc)
    {
        if (_rt == null)
        {
            _rt = GetComponent<RectTransform>();
        }
        _rt.localPosition = newLoc;
    }

    public void SetText(string s)
    {
        if (_text == null)
        {
            _text = GetComponent<TMPro.TMP_Text>();
        }
        _text.text = s;
    }
    IEnumerator FadeUp()
    {
        for (int i = 0; i < 10; i++)
        {
            _rt.localPosition = new Vector2(_rt.localPosition.x, _rt.localPosition.y + 5);
            _cg.alpha -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.2f);
        GameObject.Destroy(this.gameObject);
    }

}