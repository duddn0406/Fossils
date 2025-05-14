using UnityEngine;

public class HardRockModel : MonoBehaviour
{
    [SerializeField] private GameObject _dirtParticlePrefab;

    private int _hitCount;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Test"))
        {
            _hitCount++;
            if (_hitCount == 5)
            {
                CreateSoils();
                Destroy(this.gameObject);
            }
        }
    }

    //못 정?으로 닿았을 때 이거 호출
    public void GetDamage()
    {
        _hitCount++;
        if (_hitCount == 5)
        {
            CreateSoils();
            Destroy(this.gameObject);
        }
    }

    private void CreateSoils()
    {
        GameObject clone = Instantiate(_dirtParticlePrefab);
        clone.transform.position = this.transform.position;

        int randomSoilCount = Random.Range(5, 20);
        for (int i = 0; i < randomSoilCount; i++)
        {
            float randomScale = Random.Range(0.003f, 0.008f);
            Vector3 randomPos = new Vector3(Random.Range(0.001f, 0.003f), Random.Range(0.001f, 0.003f), Random.Range(0.001f, 0.003f));

            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            sphere.transform.position = this.transform.position;
            sphere.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            var rend = sphere.GetComponent<Renderer>();
            rend.material = new Material(rend.material);
            rend.material.color = new Color(153f / 255f, 102f / 255f, 51f / 255f);

            sphere.AddComponent<Rigidbody>();
        }
    }
}
