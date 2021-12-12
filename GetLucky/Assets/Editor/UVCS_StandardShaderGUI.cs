using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class UVCS_StandardShaderGUI : ShaderGUI
{
        public enum BlendMode
        {
            Opaque,
            Fade,   
            Transparent 
        }

    private static class Styles
        {
            public static string primaryPropertiesText = "Main Properties";
            public static string secondaryMapsText = "Secondary Maps";
            public static string forwardText = "Forward Rendering Options";
            public static string renderingMode = "Rendering Mode";
            public static string advancedText = "Advanced Options";
            public static string albedoIntensityText = "Albedo Intensity";
            public static string metallicText = "Metallic";
            public static string emissionText = "Emission";
            public static string emissionColorText = "Emission Color";
            public static string smoothnessText = "Smoothness";
            public static string alphaText = "Alpha";
            public static readonly string[] blendNames = Enum.GetNames(typeof(BlendMode));
        }

        MaterialProperty albedoIntensity = null;
        MaterialProperty metallic = null;
        MaterialProperty emission = null;
        MaterialProperty emissionColor = null;
        MaterialProperty smoothness = null;
        MaterialProperty alpha = null;
        MaterialProperty blendMode = null;
        MaterialEditor m_MaterialEditor;
        bool m_FirstTimeApply = true;


    public void FindProperties(MaterialProperty[] props)
        {
            albedoIntensity = FindProperty("_Albedo", props);
            metallic = FindProperty("_Metallic", props);
            emission = FindProperty("_Emission", props);
            emissionColor = FindProperty("_EmissionColor", props);
            smoothness = FindProperty("_Smoothness", props);
            alpha = FindProperty("_Alpha", props);
            blendMode = FindProperty("_Mode", props);
        }

    override public void OnGUI(MaterialEditor materialEditor, MaterialProperty[] props)
    {
        FindProperties(props);
        m_MaterialEditor = materialEditor;
        Material material = materialEditor.target as Material;
        if (m_FirstTimeApply)
        {
            MaterialChanged(material);
            m_FirstTimeApply = false;
        }
        ShaderPropertiesGUI(material);
    }

    public void ShaderPropertiesGUI(Material material)
        {
            EditorGUIUtility.labelWidth = 0f;
            EditorGUI.BeginChangeCheck();
            {
                BlendModePopup();
                GUILayout.Label(Styles.primaryPropertiesText, EditorStyles.boldLabel);
                m_MaterialEditor.RangeProperty(albedoIntensity,Styles.albedoIntensityText);
                if((BlendMode)material.GetFloat("_Mode") == BlendMode.Fade || (BlendMode)material.GetFloat("_Mode") == BlendMode.Transparent){
                m_MaterialEditor.RangeProperty(alpha,Styles.alphaText);
                }               
                m_MaterialEditor.RangeProperty(metallic,Styles.metallicText);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("", GUILayout.Width(25));
                m_MaterialEditor.RangeProperty(smoothness,"Smoothness");
                EditorGUILayout.EndHorizontal();                           
                m_MaterialEditor.ColorProperty(emissionColor,Styles.emissionColorText);
                m_MaterialEditor.RangeProperty(emission,Styles.emissionText);
                EditorGUILayout.Space();
            }
            if (EditorGUI.EndChangeCheck())
            {
                foreach (var obj in blendMode.targets)
                    MaterialChanged((Material)obj);
            }
            EditorGUILayout.Space();

            GUILayout.Label(Styles.advancedText, EditorStyles.boldLabel);
            m_MaterialEditor.EnableInstancingField();
            m_MaterialEditor.DoubleSidedGIField();
        }


        public override void AssignNewShaderToMaterial(Material material, Shader oldShader, Shader newShader)
        {
            if (material.HasProperty("_Emission"))
            {
                material.SetColor("_EmissionColor", material.GetColor("_Emission"));
            }

            base.AssignNewShaderToMaterial(material, oldShader, newShader);

            if (oldShader == null || !oldShader.name.Contains("Legacy Shaders/"))
            {
                SetupMaterialWithBlendMode(material, (BlendMode)material.GetFloat("_Mode"));
                return;
            }

            BlendMode blendMode = BlendMode.Opaque;
            material.SetFloat("_Mode", (float)blendMode);

            MaterialChanged(material);
        }

        void BlendModePopup()
        {
            EditorGUI.showMixedValue = blendMode.hasMixedValue;
            var mode = (BlendMode)blendMode.floatValue;

            EditorGUI.BeginChangeCheck();
            mode = (BlendMode)EditorGUILayout.Popup(Styles.renderingMode, (int)mode, Styles.blendNames);
            if (EditorGUI.EndChangeCheck())
            {
                m_MaterialEditor.RegisterPropertyChangeUndo("Rendering Mode");
                blendMode.floatValue = (float)mode;
            }

            EditorGUI.showMixedValue = false;
        }

        public static void SetupMaterialWithBlendMode(Material material, BlendMode blendMode)
        {
            switch (blendMode)
            {
                case BlendMode.Opaque:
                    material.SetOverrideTag("RenderType", "Opaque");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt("_ZWrite", 1);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = -1;
                    break;
                case BlendMode.Fade:
                    material.SetOverrideTag("RenderType", "Transparent");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 1);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.EnableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                    break;
                case BlendMode.Transparent:
                    material.SetOverrideTag("RenderType", "Transparent");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 1);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                    break;
            }
        }

    static void MaterialChanged(Material material)
    {
        SetupMaterialWithBlendMode(material,(BlendMode)material.GetFloat("_Mode"));
    }

    static void SetKeyword(Material m, string keyword, bool state)
        {
            if (state)
                m.EnableKeyword(keyword);
            else
                m.DisableKeyword(keyword);
        }
    }

