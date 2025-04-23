using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


namespace POC
{

    [CustomEditor(typeof(SimpleRuntimeUI))]
    public class SimpleCustomUI : Editor
    {       
        public override VisualElement CreateInspectorGUI()
        {
            SimpleRuntimeUI comp = target as SimpleRuntimeUI;
            // Each editor window contains a root VisualElement object
            VisualElement root = new VisualElement();

            // VisualElements objects can contain other VisualElement following a tree hierarchy.
            VisualElement label = new Label("Custom Inspector Created from C#");
            root.Add(label);

            EnumField themeOption = new EnumField("Theme: ", comp._theme);

            themeOption.RegisterValueChangedCallback(OnThemeOptionChanged);
            root.Add(themeOption);

            return root;
        }
        public void OnThemeOptionChanged(ChangeEvent<System.Enum> evt)
        {
            SimpleRuntimeUI comp = target as SimpleRuntimeUI;
            comp._theme = (UITheme)evt.newValue; 
        }
    }
}