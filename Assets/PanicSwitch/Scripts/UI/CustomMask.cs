  

using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine;

public class CustomMask : Image
{
    public override Material materialForRendering
    {
        get
        {
            Material material = new Material(base.materialForRendering);
            material.SetInt("_StencilComp", (int)CompareFunction.NotEqual);
            return material;
        }
    }
}
