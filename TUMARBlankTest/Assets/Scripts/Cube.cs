using UnityEngine;

public class Cube : MonoBehaviour
{
    private void Awake()
    {
        EventCenter.AddListener<string>(EventType.ShowCubeActivated, ShowCubeActivated);
        EventCenter.AddListener<string>(EventType.ShowCubeDeactivated, ShowCubeDeactivated);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<string>(EventType.ShowCubeActivated, ShowCubeActivated);
        EventCenter.RemoveListener<string>(EventType.ShowCubeDeactivated, ShowCubeDeactivated);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ShowCubeActivated(string str)
    {
        GameObject obj = GameObject.Find(str);
        MeshRenderer cubeRenderer = obj.GetComponent<MeshRenderer>();
        cubeRenderer.material.color = Color.red;
        //Debug.Log(str + " Activated");
    }

    private void ShowCubeDeactivated(string str)
    {
        GameObject obj = GameObject.Find(str);
        MeshRenderer cubeRenderer = obj.GetComponent<MeshRenderer>();
        cubeRenderer.material.color = Color.grey;
        //Debug.Log(str + " Deactivated");
    }

}
