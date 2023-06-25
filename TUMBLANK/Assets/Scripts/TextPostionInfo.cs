using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPostionInfo : MonoBehaviour
{
    private TMP_Text m_tmp_text;
    private string m_txt;

    private void Awake()
    {
        m_tmp_text = GetComponent<TMP_Text>();
        EventCenter.AddListener<string>(EventType.ShowUserCoordinates, ShowUserCoordinates);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<string>(EventType.ShowUserCoordinates, ShowUserCoordinates);
    }


    // Start is called before the first frame update
    void Start()
    {
        m_txt = "Waiting for GPS connection.";

        m_tmp_text.text = m_txt;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShowUserCoordinates(string txt)
    {
        //m_tmp_text.text += "\n";
        m_tmp_text.text = txt;
    }

}
