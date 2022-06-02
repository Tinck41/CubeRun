using UnityEngine;

public class Skin : MonoBehaviour
{
    [SerializeField] private SkinType _skinType;

    public SkinType GetSkinType()
    {
        return _skinType;
    }
}
