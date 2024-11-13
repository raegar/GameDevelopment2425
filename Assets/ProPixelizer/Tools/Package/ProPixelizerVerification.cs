// Copyright Elliot Bentine, 2018-
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
#endif

namespace ProPixelizer
{
    /// <summary>
    /// Provides some simple verifications to ensure that the asset is being used correctly,
    /// and provides messages to users to help them figure out when it is not.
    /// </summary>
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public class ProPixelizerVerification
    {
        static ProPixelizerVerification()
        {
#if UNITY_EDITOR
            EditorApplication.delayCall += ReimportShaders;
#endif
        }

        /// <summary>
        /// Forces ProPixelizer shaders to be imported in the correct order.
        /// This is to fix a Unity issue with UsePass, where sometimes shaders do not correctly import passes from SG shaders if imported before them.
        /// </summary>
        public static void ReimportShaders()
        {
#if UNITY_EDITOR
            AssetDatabase.ImportAsset("Assets/ProPixelizer/ShaderGraph/ProPixelizerBase.shadergraph", ImportAssetOptions.ForceUpdate);
            AssetDatabase.ImportAsset("Assets/ProPixelizer/SRP/PixelizedWithOutline.shader", ImportAssetOptions.ForceUpdate);
#endif
        }

        public static void GenerateWarnings()
        {
#if UNITY_EDITOR
            bool generatedWarning = false;

            // In the future, I hope that Unity makes ShaderGraphPreferences public so that I can use that rather than hard-coded names here.
            int variantLimit = EditorPrefs.GetInt("UnityEditor.ShaderGraph.VariantLimit", 128);
            if (variantLimit < 256)
            {
                Debug.LogWarning(string.Format(
                    "The ShaderGraph Variant Limit is currently set to a value of {0}. " +
                    "The ProPixelizer appearance shader will not compile unless this variant " +
                    "is raised, and your shaders will appear pink. Please increase this limit, " +
                    "e.g. to 256, by changing Preferences > ShaderGraph > Shader Variant Limit. " +
                    "Afterwards, reimport the ProPixelizer folder.",
                    variantLimit));
                generatedWarning = true;
            }

            var asset = GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset;
            if (asset != null)
            {
                if (asset.msaaSampleCount > 1)
                {
                    Debug.LogWarning(string.Format("MSAA is enabled in the active render pipeline asset, this is incompatible with ProPixelizer."));
                    generatedWarning = true;
                }
            }

            if (generatedWarning)
                Debug.LogWarning("Warnings have been emitted during the ProPixelizer verification step. You can disable them by unticking 'Generate Warnings' in the render feature.");
#endif
        }
    }
}