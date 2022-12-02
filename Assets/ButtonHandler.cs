using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public struct RenderFeatureToggle
{
    public ScriptableRendererFeature feature;
    public bool isEnabled;
}

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private List<RenderFeatureToggle> renderFeatures = new List<RenderFeatureToggle>();
    [SerializeField] private UniversalRenderPipelineAsset pipelineAsset;
    public GameObject Parent;
    private string origtext;
    // Start is called before the first frame update
    void Start()
    {
        origtext = Parent.GetComponent<TextMeshProUGUI>().text;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void Pressed(){
        foreach (RenderFeatureToggle toggleObj in renderFeatures)
        {
            toggleObj.feature.SetActive(toggleObj.isEnabled);
        }
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }
}
