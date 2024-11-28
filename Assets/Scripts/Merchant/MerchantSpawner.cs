using UnityEngine;

public class MerchantSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _merchantPrefab;

    public void SpawnMerchant()
    {
        Instantiate(_merchantPrefab, transform.position, Quaternion.identity);
    }
}
