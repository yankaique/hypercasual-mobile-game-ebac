using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.core.Singleton;

public class ArtManager : Singleton<ArtManager>
{
    public enum ArtType
    {
        TYPE_01,
        TYPE_02,
        BEACH,
        SNOW
    }

    public List<ArtSetup> artSetup;
    public ArtSetup GetSetupByType(ArtType arType)
    {
        return artSetup.Find(i => i.artType == arType);
    }

}

[System.Serializable]
public class ArtSetup
{
    public ArtManager.ArtType artType;
    public GameObject gameObject;
}
