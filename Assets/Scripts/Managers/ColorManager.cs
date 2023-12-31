using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.core.Singleton;
public class ColorManager : Singleton<ColorManager>
{
    public List<Material> materials;
    public List<ColorSetup> colorSetups;

    public void ChangeColorByType(ArtManager.ArtType artType)
    {
        var setup = colorSetups.Find(i => i.artType == artType);

        if(setup != null)
        {
            int colorIndex = 0;

            for(int i = 0; i < materials.Count; i++)
            {
                materials[i].SetColor("_Color", setup.colors[colorIndex]);   
                if(colorIndex + 1 < setup.colors.Count)
                {
                    colorIndex ++;
                }
            }
        }

    }
}

[System.Serializable]
public class ColorSetup
{
    public ArtManager.ArtType artType;
    public List<Color> colors;
}
