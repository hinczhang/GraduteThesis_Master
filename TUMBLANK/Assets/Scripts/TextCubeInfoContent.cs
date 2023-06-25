using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextCubeInfoContent : MonoBehaviour
{
    private TMP_Text m_tmp_text;
    private string m_txt;

    private void Awake()
    {
        m_tmp_text = GetComponent<TMP_Text>();
        EventCenter.AddListener<string>(EventType.ShowCubeCoordinates, ShowCubeCoordinates);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<string>(EventType.ShowCubeCoordinates, ShowCubeCoordinates);
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

    private void ShowCubeCoordinates(string txt)
    {
        m_tmp_text.text = txt;
    }
}
