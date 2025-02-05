using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObjectAlpha : MonoBehaviour
{
    public void SetAlpha(GameObject obj, float alpha)
    {
        if (obj == null)
        {
            Debug.LogError("SetAlpha: The passed GameObject is NULL!");
            return;
        }

        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer == null)
        {
            Debug.LogError($"SetAlpha: No Renderer found on {obj.name}");
            return;
        }

        // Ensure material exists
        if (renderer.material == null)
        {
            Debug.LogError($"SetAlpha: Renderer on {obj.name} has no material assigned!");
            return;
        }

        Color color = renderer.material.color;
        color.a = alpha;  // Set the alpha value
        renderer.material.color = color;

        // Ensure that the material shader supports transparency
        renderer.material.SetFloat("_Mode", 3);  // Set shader mode to Transparent if needed
        renderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        renderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        renderer.material.SetInt("_ZWrite", 1);
        renderer.material.DisableKeyword("_ALPHATEST_ON");
        renderer.material.EnableKeyword("_ALPHABLEND_ON");
        renderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        renderer.material.renderQueue = 3000;  // Render in transparent queue
        
    }
}
