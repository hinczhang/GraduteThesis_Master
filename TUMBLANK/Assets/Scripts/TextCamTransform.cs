using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextCamTransform : MonoBehaviour
{
    private TMP_Text m_tmp_text;
    private string m_txt;

    private void Awake()
    {
        m_tmp_text = GetComponent<TMP_Text>();
        EventCenter.AddListener<string>(EventType.ShowCamTransform, ShowCamTransform);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<string>(EventType.ShowCamTransform, ShowCamTransform);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_txt = "Application started.";

        m_tmp_text.text = m_txt;
    }

    // Update is called once per frame
    void Update()
    {
        //m_txt = "No object found.";
        //m_tmp_text.text = m_txt;
    }

    private void ShowCamTransform(string txt)
    {
        m_tmp_text.text = txt;
    }
}
