using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

// https://forum.unity.com/threads/setting-maximum-width-of-layout-element-with-content-size-fitter.335959/
public class CustomContentSizeFitter : ContentSizeFitter {
 
    // Define the min and max width and height
    public float minWidth = 0f;
    public float maxWidth = 500f;
    public float minHeight = 0f;
    public float maxHeight = 500f;
 
 
    public override void SetLayoutHorizontal() { // Override for width
        base.SetLayoutHorizontal();
        // get the rectTransform
        var rectTransform = transform as RectTransform;
        // set the anchors to avoid problems.
        rectTransform.anchorMin = rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        var sizeDelta = rectTransform.sizeDelta; // get the size delta
        // Clamp the x value based on the min and max width
        sizeDelta.x = Mathf.Clamp(sizeDelta.x, minWidth, maxWidth);
        rectTransform.sizeDelta = sizeDelta; // set the new sizeDelta
    }
 
   
    public override void SetLayoutVertical() { // Override for height
        base.SetLayoutVertical();
        // get the rectTransform
        var rectTransform = transform as RectTransform;
        // set the anchors to avoid problems.
        rectTransform.anchorMin = rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        var sizeDelta = rectTransform.sizeDelta; // get the size delta
        // Clamp the y value based on the min and max height
        sizeDelta.y = Mathf.Clamp(sizeDelta.y, minHeight, maxHeight);
        rectTransform.sizeDelta = sizeDelta; // set the new sizeDelta
    }
}
 
 
[CustomEditor(typeof(CustomContentSizeFitter))]
public class CustomContentSizeFitterEditor : Editor {
    // override the editor to be able to show the public variables on the inspector.
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        //var contentSizeFitter = (CustomContentSizeFitter)target;
    }
}