// Copyright © 2015-2021 Pico Technology Co., Ltd. All Rights Reserved.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.XR.PXR;

namespace Unity.XR.PXR.Editor
{
    [CustomEditor(typeof(PXR_Settings))]
    public class PXR_SettingsEditor : UnityEditor.Editor
    {
        private const string StereoRenderingModeAndroid = "stereoRenderingModeAndroid";
        private const string SystemDisplayFrequency = "systemDisplayFrequency";

        static GUIContent guiStereoRenderingMode = EditorGUIUtility.TrTextContent("Stereo Rendering Mode");
        static GUIContent guiDisplayFrequency = EditorGUIUtility.TrTextContent("Display Refresh Rates");


        private SerializedProperty stereoRenderingModeAndroid;
        private SerializedProperty systemDisplayFrequency;

        void OnEnable()
        {
            if (stereoRenderingModeAndroid == null) 
                stereoRenderingModeAndroid = serializedObject.FindProperty(StereoRenderingModeAndroid);
            if (systemDisplayFrequency == null)
                systemDisplayFrequency = serializedObject.FindProperty(SystemDisplayFrequency);
        }

        public override void OnInspectorGUI()
        {
            if (serializedObject == null || serializedObject.targetObject == null)
                return;

            serializedObject.Update();

            BuildTargetGroup selectedBuildTargetGroup = EditorGUILayout.BeginBuildTargetSelectionGrouping();
            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                EditorGUILayout.HelpBox("PicoXR settings cannot be changed when the editor is in play mode.", MessageType.Info);
                EditorGUILayout.Space();
            }
            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            if (selectedBuildTargetGroup == BuildTargetGroup.Android)
            {
                EditorGUILayout.PropertyField(stereoRenderingModeAndroid, guiStereoRenderingMode);
                EditorGUILayout.PropertyField(systemDisplayFrequency, guiDisplayFrequency);
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndBuildTargetSelectionGrouping();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
