using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;


namespace POC
{

    [CustomEditor(typeof(SimpleThemeUI))]
    public class SimpleThemeUIInspector : Editor
    {       
        public override VisualElement CreateInspectorGUI()
        {
            SimpleThemeUI comp = target as SimpleThemeUI;
            // Each editor window contains a root VisualElement object
            VisualElement root = new VisualElement();
            Label label = new Label("Simple Inspector editor created");
            root.Add(label);
            CreateThemeTextureField(root);
            CreateThemeField(root);
            CreateInfoField(root);
            return root;
        }
        private void CreateInfoField(VisualElement root)
        {
            var infoField = new TextField("Info Text: ");
            infoField.bindingPath = "infoText";
            infoField.RegisterCallback<ChangeEvent<string>>(OnInfoChange);
            root.Add(infoField);
        }
        private void CreateThemeTextureField(VisualElement root)
        {
            var objectField = new ObjectField("Pokemon bg Texture");
            objectField.objectType = typeof(Texture2D);
            objectField.bindingPath = "pokemonBg";
            objectField.RegisterCallback<ChangeEvent<Texture2D>>(OnBackgroundTextureChanged);
            root.Add(objectField);
        }
        private void CreateThemeField(VisualElement root)
        {
            EnumField themeOption = new EnumField("UI Theme: ");
            themeOption.bindingPath = "_theme";
            root.Add(themeOption);
            themeOption.RegisterValueChangedCallback(OnThemeOptionChanged);
        }
        private void OnBackgroundTextureChanged(ChangeEvent<Texture2D> evt)
        {
            SimpleThemeUI comp = target as SimpleThemeUI;
            comp.pokemonBg = evt.newValue;
        }
        private void OnThemeOptionChanged(ChangeEvent<System.Enum> evt)
        {
            SimpleThemeUI comp = target as SimpleThemeUI;
            comp.SetTheme((UITheme)evt.newValue);
        }
        private void OnInfoChange(ChangeEvent<string> evt)
        {
            SimpleThemeUI comp = target as SimpleThemeUI;
            comp.SetInfoText(evt.newValue);
        }

    }
}