using UnityEditor;
using UnityEngine;

namespace HighlightPlus {

    [CustomEditor(typeof(HighlightProfile))]
    [CanEditMultipleObjects]
    public class HighlightProfileEditor : UnityEditor.Editor {

        SerializedProperty effectGroup, effectGroupLayer, effectNameFilter, effectNameUseRegEx, combineMeshes, alphaCutOff, cullBackFaces, padding;
        SerializedProperty overlay, overlayMode, overlayColor, overlayAnimationSpeed, overlayMinIntensity, overlayTexture, overlayTextureScale, overlayTextureScrolling, overlayTextureUVSpace, overlayBlending, overlayVisibility;
        SerializedProperty fadeInDuration, fadeOutDuration, constantWidth, normalsOption, minimumWidth;
        SerializedProperty outline, outlineColor, outlineColorStyle, outlineGradient, outlineGradientInLocalSpace, outlineWidth, outlineBlurPasses, outlineQuality, outlineEdgeMode, outlineEdgeThreshold, outlineSharpness;
        SerializedProperty outlineDownsampling, outlineVisibility, outlineIndependent, outlineContourStyle, outlineMaskMode;
        SerializedProperty glow, glowWidth, glowQuality, glowBlurMethod, glowDownsampling, glowHQColor, glowDithering, glowDitheringStyle, glowMagicNumber1, glowMagicNumber2, glowAnimationSpeed;
        SerializedProperty glowBlendPasses, glowVisibility, glowBlendMode, glowPasses, glowMaskMode;
        SerializedProperty innerGlow, innerGlowWidth, innerGlowColor, innerGlowBlendMode, innerGlowVisibility;
        SerializedProperty targetFX, targetFXTexture, targetFXColor, targetFXRotationSpeed, targetFXInitialScale, targetFXEndScale, targetFXScaleToRenderBounds, targetFXUseEnclosingBounds, targetFXOffset;
        SerializedProperty targetFXAlignToGround, targetFXFadePower, targetFXGroundMaxDistance, targetFXGroundLayerMask, targetFXTransitionDuration, targetFXStayDuration, targetFXVisibility;
        SerializedProperty iconFX, iconFXMesh, iconFXLightColor, iconFXDarkColor, iconFXRotationSpeed, iconFXAnimationOption, iconFXAnimationAmount, iconFXAnimationSpeed, iconFXScale, iconFXScaleToRenderBounds, iconFXOffset, iconFXTransitionDuration, iconFXStayDuration;
        SerializedProperty seeThrough, seeThroughOccluderMask, seeThroughOccluderMaskAccurate, seeThroughOccluderThreshold, seeThroughOccluderCheckInterval, seeThroughOccluderCheckIndividualObjects, seeThroughDepthOffset, seeThroughMaxDepth;
        SerializedProperty seeThroughIntensity, seeThroughTintAlpha, seeThroughTintColor, seeThroughNoise, seeThroughBorder, seeThroughBorderWidth, seeThroughBorderColor, seeThroughOrdered, seeThroughBorderOnly, seeThroughTexture, seeThroughTextureUVSpace, seeThroughTextureScale, seeThroughChildrenSortingMode;
        SerializedProperty hitFxInitialIntensity, hitFxMode, hitFxFadeOutDuration, hitFxColor, hitFxRadius;
        SerializedProperty cameraDistanceFade, cameraDistanceFadeNear, cameraDistanceFadeFar;

        void OnEnable() {
            effectGroup = serializedObject.FindProperty("effectGroup");
            effectGroupLayer = serializedObject.FindProperty("effectGroupLayer");
            effectNameFilter = serializedObject.FindProperty("effectNameFilter");
            effectNameUseRegEx = serializedObject.FindProperty("effectNameUseRegEx");
            combineMeshes = serializedObject.FindProperty("combineMeshes");
            alphaCutOff = serializedObject.FindProperty("alphaCutOff");
            cullBackFaces = serializedObject.FindProperty("cullBackFaces");
            padding = serializedObject.FindProperty("padding");
            normalsOption = serializedObject.FindProperty("normalsOption");
            fadeInDuration = serializedObject.FindProperty("fadeInDuration");
            fadeOutDuration = serializedObject.FindProperty("fadeOutDuration");
            constantWidth = serializedObject.FindProperty("constantWidth");
            minimumWidth = serializedObject.FindProperty("minimumWidth");
            overlay = serializedObject.FindProperty("overlay");
            overlayMode = serializedObject.FindProperty("overlayMode");
            overlayColor = serializedObject.FindProperty("overlayColor");
            overlayAnimationSpeed = serializedObject.FindProperty("overlayAnimationSpeed");
            overlayMinIntensity = serializedObject.FindProperty("overlayMinIntensity");
            overlayBlending = serializedObject.FindProperty("overlayBlending");
            overlayVisibility = serializedObject.FindProperty("overlayVisibility");
            overlayTexture = serializedObject.FindProperty("overlayTexture");
            overlayTextureUVSpace = serializedObject.FindProperty("overlayTextureUVSpace");
            overlayTextureScale = serializedObject.FindProperty("overlayTextureScale");
            overlayTextureScrolling = serializedObject.FindProperty("overlayTextureScrolling");
            outline = serializedObject.FindProperty("outline");
            outlineColor = serializedObject.FindProperty("outlineColor");
            outlineColorStyle = serializedObject.FindProperty("outlineColorStyle");
            outlineGradient = serializedObject.FindProperty("outlineGradient");
            outlineGradientInLocalSpace = serializedObject.FindProperty("outlineGradientInLocalSpace");
            outlineWidth = serializedObject.FindProperty("outlineWidth");
            outlineBlurPasses = serializedObject.FindProperty("outlineBlurPasses");
            outlineQuality = serializedObject.FindProperty("outlineQuality");
            outlineEdgeMode = serializedObject.FindProperty("outlineEdgeMode");
            outlineEdgeThreshold = serializedObject.FindProperty("outlineEdgeThreshold");
            outlineSharpness = serializedObject.FindProperty("outlineSharpness");
            outlineDownsampling = serializedObject.FindProperty("outlineDownsampling");
            outlineVisibility = serializedObject.FindProperty("outlineVisibility");
            outlineIndependent = serializedObject.FindProperty("outlineIndependent");
            outlineContourStyle = serializedObject.FindProperty("outlineContourStyle");
            outlineMaskMode = serializedObject.FindProperty("outlineMaskMode");
            glow = serializedObject.FindProperty("glow");
            glowWidth = serializedObject.FindProperty("glowWidth");
            glowQuality = serializedObject.FindProperty("glowQuality");
            glowBlurMethod = serializedObject.FindProperty("glowBlurMethod");
            glowDownsampling = serializedObject.FindProperty("glowDownsampling");
            glowHQColor = serializedObject.FindProperty("glowHQColor");
            glowAnimationSpeed = serializedObject.FindProperty("glowAnimationSpeed");
            glowDithering = serializedObject.FindProperty("glowDithering");
            glowDitheringStyle = serializedObject.FindProperty("glowDitheringStyle");
            glowMagicNumber1 = serializedObject.FindProperty("glowMagicNumber1");
            glowMagicNumber2 = serializedObject.FindProperty("glowMagicNumber2");
            glowAnimationSpeed = serializedObject.FindProperty("glowAnimationSpeed");
            glowBlendPasses = serializedObject.FindProperty("glowBlendPasses");
            glowVisibility = serializedObject.FindProperty("glowVisibility");
            glowBlendMode = serializedObject.FindProperty("glowBlendMode");
            glowPasses = serializedObject.FindProperty("glowPasses");
            glowMaskMode = serializedObject.FindProperty("glowMaskMode");
            innerGlow = serializedObject.FindProperty("innerGlow");
            innerGlowColor = serializedObject.FindProperty("innerGlowColor");
            innerGlowWidth = serializedObject.FindProperty("innerGlowWidth");
            innerGlowBlendMode = serializedObject.FindProperty("innerGlowBlendMode");
            innerGlowVisibility = serializedObject.FindProperty("innerGlowVisibility");
            targetFX = serializedObject.FindProperty("targetFX");
            targetFXTexture = serializedObject.FindProperty("targetFXTexture");
            targetFXRotationSpeed = serializedObject.FindProperty("targetFXRotationSpeed");
            targetFXInitialScale = serializedObject.FindProperty("targetFXInitialScale");
            targetFXEndScale = serializedObject.FindProperty("targetFXEndScale");
            targetFXScaleToRenderBounds = serializedObject.FindProperty("targetFXScaleToRenderBounds");
            targetFXUseEnclosingBounds = serializedObject.FindProperty("targetFXUseEnclosingBounds");
            targetFXOffset = serializedObject.FindProperty("targetFXOffset");
            targetFXAlignToGround = serializedObject.FindProperty("targetFXAlignToGround");
            targetFXGroundMaxDistance = serializedObject.FindProperty("targetFXGroundMaxDistance");
            targetFXGroundLayerMask = serializedObject.FindProperty("targetFXGroundLayerMask");
            targetFXFadePower = serializedObject.FindProperty("targetFXFadePower");
            targetFXColor = serializedObject.FindProperty("targetFXColor");
            targetFXTransitionDuration = serializedObject.FindProperty("targetFXTransitionDuration");
            targetFXStayDuration = serializedObject.FindProperty("targetFXStayDuration");
            targetFXVisibility = serializedObject.FindProperty("targetFXVisibility");

            iconFX = serializedObject.FindProperty("iconFX");
            iconFXMesh = serializedObject.FindProperty("iconFXMesh");
            iconFXLightColor = serializedObject.FindProperty("iconFXLightColor");
            iconFXDarkColor = serializedObject.FindProperty("iconFXDarkColor");
            iconFXRotationSpeed = serializedObject.FindProperty("iconFXRotationSpeed");
            iconFXAnimationOption = serializedObject.FindProperty("iconFXAnimationOption");
            iconFXAnimationAmount = serializedObject.FindProperty("iconFXAnimationAmount");
            iconFXAnimationSpeed = serializedObject.FindProperty("iconFXAnimationSpeed");
            iconFXScale = serializedObject.FindProperty("iconFXScale");
            iconFXScaleToRenderBounds = serializedObject.FindProperty("iconFXScaleToRenderBounds");
            iconFXOffset = serializedObject.FindProperty("iconFXOffset");
            iconFXTransitionDuration = serializedObject.FindProperty("iconFXTransitionDuration");
            iconFXStayDuration = serializedObject.FindProperty("iconFXStayDuration");

            seeThrough = serializedObject.FindProperty("seeThrough");
            seeThroughOccluderMask = serializedObject.FindProperty("seeThroughOccluderMask");
            seeThroughOccluderMaskAccurate = serializedObject.FindProperty("seeThroughOccluderMaskAccurate");
            seeThroughOccluderThreshold = serializedObject.FindProperty("seeThroughOccluderThreshold");
            seeThroughOccluderCheckInterval = serializedObject.FindProperty("seeThroughOccluderCheckInterval");
            seeThroughOccluderCheckIndividualObjects = serializedObject.FindProperty("seeThroughOccluderCheckIndividualObjects");
            seeThroughDepthOffset = serializedObject.FindProperty("seeThroughDepthOffset");
            seeThroughMaxDepth = serializedObject.FindProperty("seeThroughMaxDepth");
            seeThroughIntensity = serializedObject.FindProperty("seeThroughIntensity");
            seeThroughTintAlpha = serializedObject.FindProperty("seeThroughTintAlpha");
            seeThroughTintColor = serializedObject.FindProperty("seeThroughTintColor");
            seeThroughNoise = serializedObject.FindProperty("seeThroughNoise");
            seeThroughBorder = serializedObject.FindProperty("seeThroughBorder");
            seeThroughBorderWidth = serializedObject.FindProperty("seeThroughBorderWidth");
            seeThroughBorderColor = serializedObject.FindProperty("seeThroughBorderColor");
            seeThroughBorderOnly = serializedObject.FindProperty("seeThroughBorderOnly");
            seeThroughOrdered = serializedObject.FindProperty("seeThroughOrdered");
            seeThroughTexture = serializedObject.FindProperty("seeThroughTexture");
            seeThroughTextureScale = serializedObject.FindProperty("seeThroughTextureScale");
            seeThroughTextureUVSpace = serializedObject.FindProperty("seeThroughTextureUVSpace");
            seeThroughChildrenSortingMode = serializedObject.FindProperty("seeThroughChildrenSortingMode");
            hitFxInitialIntensity = serializedObject.FindProperty("hitFxInitialIntensity");
            hitFxMode = serializedObject.FindProperty("hitFxMode");
            hitFxFadeOutDuration = serializedObject.FindProperty("hitFxFadeOutDuration");
            hitFxColor = serializedObject.FindProperty("hitFxColor");
            hitFxRadius = serializedObject.FindProperty("hitFxRadius");
            cameraDistanceFade = serializedObject.FindProperty("cameraDistanceFade");
            cameraDistanceFadeNear = serializedObject.FindProperty("cameraDistanceFadeNear");
            cameraDistanceFadeFar = serializedObject.FindProperty("cameraDistanceFadeFar");
        }

        public override void OnInspectorGUI() {

            serializedObject.Update();

            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Highlight Options", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(effectGroup, new GUIContent("Include"));
            if (effectGroup.intValue == (int)TargetOptions.LayerInScene || effectGroup.intValue == (int)TargetOptions.LayerInChildren) {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(effectGroupLayer, new GUIContent("Layer"));
                EditorGUI.indentLevel--;
            }
            if (effectGroup.intValue != (int)TargetOptions.OnlyThisObject && effectGroup.intValue != (int)TargetOptions.Scripting) {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(effectNameFilter, new GUIContent("Object Name Filter"));
                EditorGUILayout.PropertyField(effectNameUseRegEx, new GUIContent("Use Regular Expressions"));
                EditorGUILayout.PropertyField(combineMeshes);
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.PropertyField(alphaCutOff);
            EditorGUILayout.PropertyField(cullBackFaces);
            EditorGUILayout.PropertyField(padding);
            EditorGUILayout.PropertyField(normalsOption);
            EditorGUILayout.PropertyField(fadeInDuration);
            EditorGUILayout.PropertyField(fadeOutDuration);
            EditorGUILayout.PropertyField(cameraDistanceFade);
            if (cameraDistanceFade.boolValue) {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(cameraDistanceFadeNear, new GUIContent("Near Distance"));
                EditorGUILayout.PropertyField(cameraDistanceFadeFar, new GUIContent("Far Distance"));
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.PropertyField(constantWidth);
            if (!constantWidth.boolValue) {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(minimumWidth);
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.PropertyField(outlineIndependent, new GUIContent("Independent", "Do not combine outline with other highlighted objects."));

            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Effects", EditorStyles.boldLabel);

            EditorGUILayout.BeginVertical(GUI.skin.box);
            DrawSectionField(outline, "Outline", outline.floatValue > 0);
            if (outline.floatValue > 0) {
                EditorGUI.indentLevel++;
                HighlightEffectEditor.QualityPropertyField(outlineQuality);
                if (outlineQuality.intValue == (int)QualityLevel.Highest) {
                    EditorGUILayout.PropertyField(outlineEdgeMode, new GUIContent("Edges"));
                    if (outlineEdgeMode.intValue == (int)OutlineEdgeMode.Any) {
                        EditorGUILayout.PropertyField(outlineEdgeThreshold, new GUIContent("Edge Detection Threshold"));
                    }
                    EditorGUILayout.PropertyField(outlineContourStyle, new GUIContent("Contour Style"));
                    EditorGUILayout.PropertyField(outlineWidth, new GUIContent("Width"));
                    EditorGUILayout.PropertyField(outlineColor, new GUIContent("Color"));
                    EditorGUILayout.PropertyField(outlineBlurPasses, new GUIContent("Blur Passes"));
                    EditorGUILayout.PropertyField(outlineSharpness, new GUIContent("Sharpness"));
                } else {
                    EditorGUILayout.PropertyField(outlineWidth, new GUIContent("Width"));
                    EditorGUILayout.PropertyField(outlineColorStyle, new GUIContent("Color Style"));
                    switch ((ColorStyle)outlineColorStyle.intValue) {
                        case ColorStyle.SingleColor:
                            EditorGUILayout.PropertyField(outlineColor, new GUIContent("Color"));
                            break;
                        case ColorStyle.Gradient:
                            EditorGUI.indentLevel++;
                            EditorGUILayout.PropertyField(outlineGradient, new GUIContent("Gradient"));
                            EditorGUILayout.PropertyField(outlineGradientInLocalSpace, new GUIContent("In Local Space"));
                            EditorGUI.indentLevel--;
                            break;
                    }
                }
                if (outlineQuality.intValue == (int)QualityLevel.Highest && outlineEdgeMode.intValue != (int)OutlineEdgeMode.Any) {
                    EditorGUILayout.PropertyField(outlineDownsampling, new GUIContent("Downsampling"));
                }
                if (outlineQuality.intValue == (int)QualityLevel.Highest && glowQuality.intValue == (int)QualityLevel.Highest) {
                    EditorGUILayout.PropertyField(glowVisibility, new GUIContent("Visibility"));
                } else {
                    EditorGUILayout.PropertyField(outlineVisibility, new GUIContent("Visibility"));
                }
                EditorGUILayout.PropertyField(outlineMaskMode, new GUIContent("Mask Mode"));
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            DrawSectionField(glow, "Outer Glow", glow.floatValue > 0);
            if (glow.floatValue > 0) {
                EditorGUI.indentLevel++;
                HighlightEffectEditor.QualityPropertyField(glowQuality);
                if (glowQuality.intValue == (int)QualityLevel.Highest) {
                    EditorGUILayout.PropertyField(outlineContourStyle, new GUIContent("Contour Style"));
                    EditorGUILayout.PropertyField(glowWidth, new GUIContent("Width"));
                    EditorGUILayout.PropertyField(glowHQColor, new GUIContent("Color"));
                    EditorGUILayout.PropertyField(glowBlurMethod, new GUIContent("Blur Method", "Gaussian: better quality. Kawase: faster."));
                    EditorGUILayout.PropertyField(glowDownsampling, new GUIContent("Downsampling"));
                } else {
                    EditorGUILayout.PropertyField(glowWidth, new GUIContent("Width"));
                }
                EditorGUILayout.PropertyField(glowAnimationSpeed, new GUIContent("Animation Speed"));
                EditorGUILayout.PropertyField(glowVisibility, new GUIContent("Visibility"));
                EditorGUILayout.PropertyField(glowMaskMode, new GUIContent("Mask Mode"));
                EditorGUILayout.PropertyField(glowBlendMode, new GUIContent("Blend Mode"));
                if (glowQuality.intValue != (int)QualityLevel.Highest) {
                    EditorGUILayout.PropertyField(glowDithering, new GUIContent("Dithering"));
                    if (glowDithering.floatValue > 0) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.PropertyField(glowDitheringStyle, new GUIContent("Style"));
                        if (glowDitheringStyle.intValue == (int)GlowDitheringStyle.Pattern) {
                            EditorGUILayout.PropertyField(glowMagicNumber1, new GUIContent("Magic Number 1"));
                            EditorGUILayout.PropertyField(glowMagicNumber2, new GUIContent("Magic Number 2"));
                        }
                        EditorGUI.indentLevel--;
                    }
                    EditorGUILayout.PropertyField(glowBlendPasses, new GUIContent("Blend Passes"));
                    EditorGUILayout.PropertyField(glowPasses, true);
                }
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            DrawSectionField(innerGlow, "Inner Glow", innerGlow.floatValue > 0);
            if (innerGlow.floatValue > 0) {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(innerGlowColor, new GUIContent("Color"));
                EditorGUILayout.PropertyField(innerGlowWidth, new GUIContent("Width"));
                EditorGUILayout.PropertyField(innerGlowBlendMode, new GUIContent("Blend Mode"));
                EditorGUILayout.PropertyField(innerGlowVisibility, new GUIContent("Visibility"));
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            DrawSectionField(overlay, "Overlay", overlay.floatValue > 0);
            if (overlay.floatValue > 0) {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(overlayMode, new GUIContent("Mode"));
                EditorGUILayout.PropertyField(overlayColor, new GUIContent("Color"));
                EditorGUILayout.PropertyField(overlayTexture, new GUIContent("Texture"));
                if (overlayTexture.objectReferenceValue != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(overlayTextureUVSpace, new GUIContent("UV Space"));
                    EditorGUILayout.PropertyField(overlayTextureScale, new GUIContent("Texture Scale"));
                    if ((TextureUVSpace)overlayTextureUVSpace.intValue != TextureUVSpace.Triplanar) {
                        EditorGUILayout.PropertyField(overlayTextureScrolling, new GUIContent("Texture Scrolling"));
                    }
                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.PropertyField(overlayBlending, new GUIContent("Blending"));
                EditorGUILayout.PropertyField(overlayMinIntensity, new GUIContent("Min Intensity"));
                EditorGUILayout.PropertyField(overlayAnimationSpeed, new GUIContent("Animation Speed"));
                EditorGUILayout.PropertyField(overlayVisibility, new GUIContent("Visibility"));
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            DrawSectionField(targetFX, "Target", targetFX.boolValue);
            if (targetFX.boolValue) {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(targetFXTexture, new GUIContent("Texture"));
                EditorGUILayout.PropertyField(targetFXColor, new GUIContent("Color"));
                EditorGUILayout.PropertyField(targetFXUseEnclosingBounds, new GUIContent("Use Enclosing Bounds"));
                EditorGUILayout.PropertyField(targetFXOffset, new GUIContent("Offset"));
                EditorGUILayout.PropertyField(targetFXRotationSpeed, new GUIContent("Rotation Speed"));
                EditorGUILayout.PropertyField(targetFXInitialScale, new GUIContent("Initial Scale"));
                EditorGUILayout.PropertyField(targetFXEndScale, new GUIContent("End Scale"));
                EditorGUILayout.PropertyField(targetFXScaleToRenderBounds, new GUIContent("Scale To Object Bounds"));
                EditorGUILayout.PropertyField(targetFXAlignToGround, new GUIContent("Align To Ground"));
                if (targetFXAlignToGround.boolValue) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(targetFXGroundMaxDistance, new GUIContent("Ground Max Distance"));
                    EditorGUILayout.PropertyField(targetFXGroundLayerMask, new GUIContent("Ground Layer Mask"));
                    EditorGUILayout.PropertyField(targetFXFadePower, new GUIContent("Fade Power"));
                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.PropertyField(targetFXTransitionDuration, new GUIContent("Transition Duration"));
                EditorGUILayout.PropertyField(targetFXStayDuration, new GUIContent("Stay Duration"));
                EditorGUILayout.PropertyField(targetFXVisibility, new GUIContent("Visibility"));
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            DrawSectionField(iconFX, "Icon", iconFX.boolValue);
            if (iconFX.boolValue) {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(iconFXMesh, new GUIContent("Mesh"));
                EditorGUILayout.PropertyField(iconFXLightColor, new GUIContent("Light Color"));
                EditorGUILayout.PropertyField(iconFXDarkColor, new GUIContent("Dark Color"));
                EditorGUILayout.PropertyField(iconFXOffset, new GUIContent("Offset"));
                EditorGUILayout.PropertyField(iconFXRotationSpeed, new GUIContent("Rotation Speed"));
                EditorGUILayout.PropertyField(iconFXAnimationOption, new GUIContent("Animation"));
                if (iconFXAnimationOption.intValue != 0) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(iconFXAnimationAmount, new GUIContent("Amount"));
                    EditorGUILayout.PropertyField(iconFXAnimationSpeed, new GUIContent("Speed"));
                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.PropertyField(iconFXScale, new GUIContent("Scale"));
                EditorGUILayout.PropertyField(iconFXScaleToRenderBounds, new GUIContent("Scale To Object Bounds"));
                EditorGUILayout.PropertyField(iconFXTransitionDuration, new GUIContent("Transition Duration"));
                EditorGUILayout.PropertyField(iconFXStayDuration, new GUIContent("Stay Duration"));
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.PropertyField(seeThrough);
            if (seeThrough.intValue != (int)SeeThroughMode.Never) {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(seeThroughOccluderMask, new GUIContent("Occluder Layer"));
                if (seeThroughOccluderMask.intValue > 0) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(seeThroughOccluderMaskAccurate, new GUIContent("Accurate"));
                    EditorGUILayout.PropertyField(seeThroughOccluderThreshold, new GUIContent("Radius Threshold", "Multiplier to the object bounds. Making the bounds smaller prevents false occlusion tests."));
                    EditorGUILayout.PropertyField(seeThroughOccluderCheckInterval, new GUIContent("Check Interval", "Interval in seconds between occlusion tests."));
                    EditorGUILayout.PropertyField(seeThroughOccluderCheckIndividualObjects, new GUIContent("Check Individual Objects", "Interval in seconds between occlusion tests."));
                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.PropertyField(seeThroughDepthOffset, new GUIContent("Depth Offset" + ((seeThroughDepthOffset.floatValue > 0) ? " •" : "")));
                EditorGUILayout.PropertyField(seeThroughMaxDepth, new GUIContent("Max Depth" + ((seeThroughMaxDepth.floatValue > 0) ? " •" : "")));
                EditorGUILayout.PropertyField(seeThroughIntensity, new GUIContent("Intensity"));
                EditorGUILayout.PropertyField(seeThroughTintColor, new GUIContent("Color"));
                EditorGUILayout.PropertyField(seeThroughTintAlpha, new GUIContent("Color Blend"));
                EditorGUILayout.PropertyField(seeThroughNoise, new GUIContent("Noise"));
                EditorGUILayout.PropertyField(seeThroughTexture, new GUIContent("Texture"));
                if (seeThroughTexture.objectReferenceValue != null) {
                    EditorGUILayout.PropertyField(seeThroughTextureUVSpace, new GUIContent("UV Space"));
                    EditorGUILayout.PropertyField(seeThroughTextureScale, new GUIContent("Texture Scale"));
                }
                EditorGUILayout.PropertyField(seeThroughBorder, new GUIContent("Border When Hidden"));
                if (seeThroughBorder.floatValue > 0) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(seeThroughBorderWidth, new GUIContent("Width"));
                    EditorGUILayout.PropertyField(seeThroughBorderColor, new GUIContent("Color"));
                    EditorGUILayout.PropertyField(seeThroughBorderOnly, new GUIContent("Border Only"));
                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.PropertyField(seeThroughChildrenSortingMode, new GUIContent("Children Sorting Mode"));
                EditorGUILayout.PropertyField(seeThroughOrdered, new GUIContent("Ordered"));

                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            DrawSectionField(hitFxInitialIntensity, "Hit FX", hitFxInitialIntensity.floatValue > 0);
            if (hitFxInitialIntensity.floatValue > 0) {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(hitFxInitialIntensity, new GUIContent("Initial Intensity"));
                EditorGUILayout.PropertyField(hitFxMode, new GUIContent("Mode"));
                EditorGUILayout.PropertyField(hitFxFadeOutDuration, new GUIContent("Fade Out Duration"));
                EditorGUILayout.PropertyField(hitFxColor, new GUIContent("Color"));
                if ((HitFxMode)hitFxMode.intValue == HitFxMode.LocalHit) {
                    EditorGUILayout.PropertyField(hitFxRadius, new GUIContent("Radius"));
                }
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndVertical();

            if (serializedObject.ApplyModifiedProperties() || (Event.current.type == EventType.ValidateCommand &&
                Event.current.commandName == "UndoRedoPerformed")) {

                // Triggers profile reload on all Highlight Effect scripts
                HighlightEffect[] effects = Misc.FindObjectsOfType<HighlightEffect>();
                for (int t = 0; t < targets.Length; t++) {
                    HighlightProfile profile = (HighlightProfile)targets[t];
                    for (int k = 0; k < effects.Length; k++) {
                        if (effects[k] != null && effects[k].profile == profile && effects[k].profileSync) {
                            profile.Load(effects[k]);
                            effects[k].Refresh();
                        }
                    }
                }
                EditorUtility.SetDirty(target);
            }

        }

        void DrawSectionField(SerializedProperty property, string label, bool active) {
            EditorGUILayout.PropertyField(property, new GUIContent(active ? label + " •" : label));
        }

    }

}