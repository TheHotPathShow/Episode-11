using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName = "Textures for CONTROLERSCHEME", menuName = "Daxode/ControllerSchemeTextureCollection", order = 0)]
public class ControllerSchemeTextureCollection : ScriptableObject
{
    [Serializable]
    struct NameTexturePair
    {
        public string Name;
        public Texture2D Texture;
    }

    [SerializeField] NameTexturePair[] Textures = Array.Empty<NameTexturePair>();
    Dictionary<string, Texture2D> m_CachedLookup;
    
    Texture2D GetFromName(string name)
    {
        if (m_CachedLookup == null)
        {
            m_CachedLookup = new Dictionary<string, Texture2D>();
            foreach (var kvp in Textures) 
                m_CachedLookup[kvp.Name] = kvp.Texture;
        }
        
        return m_CachedLookup[name];
    }

    public Texture2D GetFromActionAndDevice(InputAction action, InputDevice device)
    {
        InputControl controlForThisPlayerDevice = null;
        foreach (var controlOnAction in action.controls)
        {
            if (controlOnAction.device != device) 
                continue;
            controlForThisPlayerDevice = controlOnAction;
            break;
        }
        if (controlForThisPlayerDevice == null)
            return null;
        
        var bindingIndex = action.GetBindingIndexForControl(controlForThisPlayerDevice);
        var binding = action.bindings[bindingIndex];

        if (binding.isPartOfComposite)
        {
            var upName = binding.path.Split('/')[1];
            var downName = action.bindings[bindingIndex + 1].path.Split('/')[1];
            var leftName = action.bindings[bindingIndex + 2].path.Split('/')[1];
            var rightName = action.bindings[bindingIndex + 3].path.Split('/')[1];
            Debug.Log($"Up: {upName}, Down: {downName}, Left: {leftName}, Right: {rightName}");
            return GenerateDirectionalKeyboardTexture(
                GetFromName(upName), GetFromName(downName), 
                GetFromName(leftName), GetFromName(rightName));
        }
        
        var buttonName = binding.path.Split('/')[1];
        Debug.Log($"Button {buttonName}");
        return GetFromName(buttonName);
    }
    
    static Texture2D GenerateDirectionalKeyboardTexture(Texture2D up, Texture2D down, Texture2D left, Texture2D right)
    {
        // 3x2 grid,
        // up and down should be in the middle column,
        // left right should be in bottom row, left and right of the middle column
        var width = up.width;
        var height = up.height;
        var combined = new Texture2D(width*3, height*2);
        combined.SetPixels(width, 0, up.width, up.height, up.GetPixels());
        combined.SetPixels(width, height, down.width, down.height, down.GetPixels());
        combined.SetPixels(0, height, left.width, left.height, left.GetPixels());
        combined.SetPixels(width*2, height, right.width, right.height, right.GetPixels());
        combined.Apply();
        
        return combined;
    }
}