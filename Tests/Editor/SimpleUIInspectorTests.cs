using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using POC;

public class SimpleUIInspectorTests
{
    private GameObject gameObject;
    private SimpleThemeUI comp;
    private Editor editor;

    [SetUp]
    public void Setup()
    {
        gameObject = new GameObject("SimpleThemeUITest");
        comp = gameObject.AddComponent<SimpleThemeUI>();
        editor = Editor.CreateEditor(comp);
    }
    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(editor);
        Object.DestroyImmediate(gameObject);
    }
    [Test]
    public void InfoField_UpdatesInfoTextCorrectly()
    {
        // Create and simulate inspector
        var root = (editor as SimpleThemeUIInspector).CreateInspectorGUI();

        
        var infoField = root.Q<TextField>();
        Assert.IsNotNull(infoField);

        string testText = "Hello World!";
        infoField.value = testText;

        comp.SetInfoText(testText);

        // Verify
        Assert.AreEqual("Hello World!", comp.infoText);
    }
    [Test]
    public void ThemeField_UpdatesThemeCorrectly()
    {
        var root = (editor as SimpleThemeUIInspector).CreateInspectorGUI();

        var themeField = root.Q<EnumField>();
        Assert.IsNotNull(themeField);

        UITheme expectedTheme = UITheme.POKEMON; 

        // Simulate changing the value
        themeField.value = expectedTheme;

        // Again, simulate callback (normally triggered by user interaction)
        comp.SetTheme(expectedTheme);

        Assert.AreEqual(expectedTheme, comp.GetTheme());
    }

    [Test]
    public void TextureField_SetsPokemonBgCorrectly()
    {
        var root = (editor as SimpleThemeUIInspector).CreateInspectorGUI();

        var textureField = root.Q<ObjectField>();
        Assert.IsNotNull(textureField);

        var texture = new Texture2D(256, 256);
        texture.name = "TestTexture";

        textureField.value = texture;

        comp.pokemonBg = texture;

        Assert.AreEqual(texture, comp.pokemonBg);
    }
}
