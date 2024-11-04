using UnityEngine;

namespace HighlightPlus {

    public static class ShaderParams {

        // general uniforms
        public static int Cull = Shader.PropertyToID("_Cull");
        public static int BlurScale = Shader.PropertyToID("_BlurScale");
        public static int BlurScaleFirstHoriz = Shader.PropertyToID("_BlurScaleFirstHoriz");
        public static int Speed = Shader.PropertyToID("_Speed");
        public static int ConstantWidth = Shader.PropertyToID("_ConstantWidth");
        public static int MinimumWidth = Shader.PropertyToID("_MinimumWidth");
        public static int CutOff = Shader.PropertyToID("_CutOff");
        public static int ZTest = Shader.PropertyToID("_ZTest");
        public static int Flip = Shader.PropertyToID("_Flip");
        public static int Debug = Shader.PropertyToID("_Debug");
        public static int Color = Shader.PropertyToID("_Color");
        public static int MainTex = Shader.PropertyToID("_MainTex");
        public static int BaseMap = Shader.PropertyToID("_BaseMap");
        public static int BaseMapST = Shader.PropertyToID("_BaseMap_ST");
        public static int BlendSrc = Shader.PropertyToID("_BlendSrc");
        public static int BlendDst = Shader.PropertyToID("_BlendDst");
        public static int FadeFactor = Shader.PropertyToID("_HP_Fade");
        public static int Padding = Shader.PropertyToID("_Padding");
        public static int ResampleScale = Shader.PropertyToID("_ResampleScale");
        public static int NoiseTex = Shader.PropertyToID("_NoiseTex");

        // outline uniforms
        public static int OutlineWidth = Shader.PropertyToID("_OutlineWidth");
        public static int OutlineZTest = Shader.PropertyToID("_OutlineZTest");
        public static int OutlineDirection = Shader.PropertyToID("_OutlineDirection");
        public static int OutlineColor = Shader.PropertyToID("_OutlineColor");
        public static int OutlineVertexWidth = Shader.PropertyToID("_OutlineVertexWidth");
        public static int OutlineGradientTex = Shader.PropertyToID("_OutlineGradientTex");
        public static int OutlineVertexData = Shader.PropertyToID("_OutlineVertexData");
        public static int OutlineStencilComp = Shader.PropertyToID("_OutlineStencilComp");
        public static int OutlineEdgeThreshold = Shader.PropertyToID("_EdgeThreshold");
        public static int OutlineSharpness = Shader.PropertyToID("_OutlineSharpness");

        // glow uniforms
        public static int GlowZTest = Shader.PropertyToID("_GlowZTest");
        public static int GlowStencilComp = Shader.PropertyToID("_GlowStencilComp");
        public static int GlowStencilOp = Shader.PropertyToID("_GlowStencilOp");
        public static int GlowDirection = Shader.PropertyToID("_GlowDirection");
        public static int Glow = Shader.PropertyToID("_Glow");
        public static int GlowColor = Shader.PropertyToID("_GlowColor");
        public static int Glow2 = Shader.PropertyToID("_Glow2");

        // see-through uniforms
        public static int SeeThrough = Shader.PropertyToID("_SeeThrough");
        public static int SeeThroughNoise = Shader.PropertyToID("_SeeThroughNoise");
        public static int SeeThroughBorderWidth = Shader.PropertyToID("_SeeThroughBorderWidth");
        public static int SeeThroughBorderConstantWidth = Shader.PropertyToID("_SeeThroughBorderConstantWidth");
        public static int SeeThroughTintColor = Shader.PropertyToID("_SeeThroughTintColor");
        public static int SeeThroughBorderColor = Shader.PropertyToID("_SeeThroughBorderColor");
        public static int SeeThroughStencilRef = Shader.PropertyToID("_SeeThroughStencilRef");
        public static int SeeThroughStencilComp = Shader.PropertyToID("_SeeThroughStencilComp");
        public static int SeeThroughStencilPassOp = Shader.PropertyToID("_SeeThroughStencilPassOp");
        public static int SeeThroughDepthOffset = Shader.PropertyToID("_SeeThroughDepthOffset");
        public static int SeeThroughMaxDepth = Shader.PropertyToID("_SeeThroughMaxDepth");
        public static int SeeThroughTexture = Shader.PropertyToID("_SeeThroughTexture");
        public static int SeeThroughTextureScale = Shader.PropertyToID("_SeeThroughTextureScale");

        // inner glow uniforms
        public static int InnerGlowWidth = Shader.PropertyToID("_InnerGlowWidth");
        public static int InnerGlowZTest = Shader.PropertyToID("_InnerGlowZTest");
        public static int InnerGlowColor = Shader.PropertyToID("_InnerGlowColor");
        public static int InnerGlowBlendMode = Shader.PropertyToID("_InnerGlowBlendMode");

        // overlay uniforms
        public static int OverlayData = Shader.PropertyToID("_OverlayData");
        public static int OverlayBackColor = Shader.PropertyToID("_OverlayBackColor");
        public static int OverlayColor = Shader.PropertyToID("_OverlayColor");
        public static int OverlayHitPosData = Shader.PropertyToID("_OverlayHitPosData");
        public static int OverlayHitStartTime = Shader.PropertyToID("_OverlayHitStartTime");
        public static int OverlayTexture = Shader.PropertyToID("_OverlayTexture");
        public static int OverlayTextureScrolling = Shader.PropertyToID("_OverlayTextureScrolling");
        public static int OverlayZTest = Shader.PropertyToID("_OverlayZTest");

        // target uniforms
        public static int TargetFXRenderData = Shader.PropertyToID("_TargetFXRenderData");
        public static int GlowRT = Shader.PropertyToID("_HPComposeGlowFinal");
        public static int OutlineRT = Shader.PropertyToID("_HPComposeOutlineFinal");

        // icon uniforms
        public static int IconFXDarkColor = Shader.PropertyToID("_DarkColor");

        // keywords
        public const string SKW_ALPHACLIP = "HP_ALPHACLIP";
        public const string SKW_OUTLINE_GRADIENT_WS = "HP_OUTLINE_GRADIENT_WS";
        public const string SKW_OUTLINE_GRADIENT_LS = "HP_OUTLINE_GRADIENT_LS";
        public const string SKW_ALL_EDGES = "HP_ALL_EDGES";
        public const string SKW_DEPTHCLIP = "HP_DEPTHCLIP";
        public const string SKW_DEPTHCLIP_INV = "HP_DEPTHCLIP_INV";
        public const string SKW_DEPTH_OFFSET = "HP_DEPTH_OFFSET";
        public const string SKW_TEXTURE_TRIPLANAR = "HP_TEXTURE_TRIPLANAR";
        public const string SKW_TEXTURE_SCREENSPACE = "HP_TEXTURE_SCREENSPACE";
        public const string SKW_TEXTURE_OBJECTSPACE = "HP_TEXTURE_OBJECTSPACE";
        public const string SKW_SEETHROUGH_ONLY_BORDER = "HP_SEETHROUGH_ONLY_BORDER";
        public const string SKW_MASK_CUTOUT = "HP_MASK_CUTOUT";
        public const string SKW_DITHER_BLUENOISE = "HP_DITHER_BLUENOISE";
    }
}

