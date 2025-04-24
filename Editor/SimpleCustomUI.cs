using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
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
            var objectField = new ObjectField("Pokemon bg Texture");
            objectField.objectType = typeof(Texture2D);
            objectField.bindingPath = "pokemonBg";
            objectField.RegisterCallback<ChangeEvent<Texture2D>>(OnBackgroundTextureChanged);
            root.Add(objectField);
            themeOption.RegisterValueChangedCallback(OnThemeOptionChanged);
            root.Add(themeOption);

            return root;
        }
        public void OnBackgroundTextureChanged(ChangeEvent<Texture2D> evt)
        {
            SimpleRuntimeUI comp = target as SimpleRuntimeUI;
            comp.pokemonBg = evt.newValue;
        }
        public void OnThemeOptionChanged(ChangeEvent<System.Enum> evt)
        {
            SimpleRuntimeUI comp = target as SimpleRuntimeUI;
            comp.SetTheme((UITheme)evt.newValue);
            //comp._theme = (UITheme)evt.newValue; 
        }
    }
}