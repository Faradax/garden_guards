using UnityEngine;
using UnityEngine.Rendering;

[ExecuteAlways]
public class OrientToCameraPlane : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        RenderPipelineManager.beginCameraRendering += Orient;
    }
    private void Orient(ScriptableRenderContext scriptableRenderContext, Camera cam)
    {
        transform.forward = cam.transform.forward;
    }

    private void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= Orient;
    }
}
