using UnityEngine;

public class Skin : MonoBehaviour
{
    [SerializeField] private SkinType _skinType;

    [SerializeField] private GameObject _fracturedSkin;
    [SerializeField] private GameObject _commonSkin;

    [SerializeField] private AudioSource _fractureSound;

    private GameObject _fracturedInstance;

    public SkinType GetSkinType()
    {
        return _skinType;
    }

    public void CreateFracturedSkin()
    {
        _commonSkin.SetActive(false);
        _fracturedInstance = Instantiate(_fracturedSkin, transform);
        _fractureSound.Play();
    }

    public GameObject GetCommonSkin()
    {
        return _commonSkin;
    }

    public void Reset()
    {
        if (_fracturedInstance != null)
        {
            Destroy(_fracturedInstance);
        }
        _commonSkin.SetActive(true);
    }
}
