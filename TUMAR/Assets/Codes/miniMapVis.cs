using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class miniMapVis : MonoBehaviour
{
    private Dictionary<int, VirtualLandmark> landmarksIdDict = new Dictionary<int, VirtualLandmark>();
    private Dictionary<string, ChunkLandmark> chunks = new Dictionary<string, ChunkLandmark>();
    private RawImage miniMap;
    private Color dotColor = Color.red;
    private float dotRadius = 5.0f;
    private Texture2D miniMapTexture;
    private RectTransform miniMapRect;

    void Awake()
    {
        // Vector3 objScreenPos = Camera.main.WorldToScreenPoint(objTransform.position);
        miniMap = GameObject.Find("MiniMap").GetComponent<RawImage>();
        miniMapRect = miniMap.rectTransform;
        miniMapTexture = new Texture2D((int)miniMapRect.rect.width, (int)miniMapRect.rect.height);
        miniMapTexture.filterMode = FilterMode.Point;
        miniMap.texture = miniMapTexture;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in testGettingObj.landmarksIdDict) {
            landmarksIdDict.Add(item.Key, item.Value);
            /*
            GameObject obj = item.Value.GetGameObject();
            Vector3 objScreenPos = Camera.main.WorldToScreenPoint(obj.transform.position);
            int x = (int)(objScreenPos.x * (int)miniMapRect.rect.width / Screen.width);
            int y = (int)(objScreenPos.y * (int)miniMapRect.rect.height / Screen.height);
            for (int i = -Mathf.RoundToInt(dotRadius); i <= Mathf.RoundToInt(dotRadius); i++) {
                for (int j = -Mathf.RoundToInt(dotRadius); j <= Mathf.RoundToInt(dotRadius); j++)
                {
                    if (i * i + j * j <= dotRadius * dotRadius)
                    {
                        miniMapTexture.SetPixel(x + i, y + j, dotColor);
                    }
                }
            }

            miniMapTexture.Apply();
            */
        }

        foreach (var item in testGettingObj.chunks) {
            chunks.Add(item.GetChunkName(), item);
        }
    }

    void reDrawMap(string chunkName) {
        
        
        // x - right, z - up, no y
        ChunkLandmark chunk = chunks[chunkName];
        GameObject currentChunk = GameObject.Find(chunkName);
        List<GameObject> objs = chunk.GetChunkObjs();
        float width = currentChunk.GetComponent<Collider>().bounds.size.x;
        float depth = currentChunk.GetComponent<Collider>().bounds.size.z;
        
        float orgW = miniMapRect.rect.width;
        float orgH = miniMapRect.rect.height;
        miniMap.rectTransform.sizeDelta = new Vector2(orgW, orgH*depth/width);
        miniMapRect = miniMap.rectTransform;
        miniMapTexture = new Texture2D((int)miniMapRect.rect.width, (int)miniMapRect.rect.height);
        miniMapTexture.filterMode = FilterMode.Point;
        miniMap.texture = miniMapTexture;

        Debug.Log("chunk size: " + width + " " + depth);
        Debug.Log("map size: " + miniMapRect.rect.width + " " + miniMapRect.rect.height);
        foreach(var obj in objs) {
            // Vector3 objScreenPos = Camera.main.WorldToScreenPoint(obj.transform.position);
            int x = (int)((obj.transform.position.x - currentChunk.transform.position.x + width/2) * (int)miniMapRect.rect.width / width);
            int y = (int)((obj.transform.position.z - currentChunk.transform.position.z + depth/2) * (int)miniMapRect.rect.height / depth);
            Debug.Log("obj pos: " + x + " " + y);

            for (int i = -Mathf.RoundToInt(dotRadius); i <= Mathf.RoundToInt(dotRadius); i++) {
                for (int j = -Mathf.RoundToInt(dotRadius); j <= Mathf.RoundToInt(dotRadius); j++)
                {
                    if (i * i + j * j <= dotRadius * dotRadius)
                    {
                        miniMapTexture.SetPixel(x + i, y + j, dotColor);
                    }
                }
            }
        }
        miniMapTexture.Apply();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
