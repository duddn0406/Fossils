using Unity.VisualScripting;
using UnityEngine;
using System;

public class HardRockModel : MonoBehaviour
{
    [SerializeField] private GameObject _dirtParticlePrefab;

    private int _destroyHitCount;
    private int _breakHitCount;

    public event Action OnRockDestroyed;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "hand_pick"
            || collision.gameObject.tag == "chisel")
        {
            GetDamage(collision.gameObject);
        }
    }

    //못 정?으로 닿았을 때 이거 호출 아예 부술 때
    public void GetDamage(GameObject collisionObject)
    {
        if(collisionObject.tag == "hand_pick") //곡괭이
        {
            _breakHitCount++;
            if(_breakHitCount ==5)
            {
                CreateSoils();
                OnRockDestroyed?.Invoke();
                this.AddComponent<Rigidbody>();
            }
        }
        else if(collisionObject.tag == "chisel") //끌
        {
            if(_breakHitCount > 4)
            {
                _destroyHitCount++;
                if (_destroyHitCount == 5)
                {
                    CreateSoils();
                    Destroy(this.gameObject);
                }
            }
        }
    }

    private void CreateSoils()
    {
        GameObject clone = Instantiate(_dirtParticlePrefab);
        clone.transform.position = this.transform.position;

        int randomSoilCount = UnityEngine.Random.Range(5, 20);
        for (int i = 0; i < randomSoilCount; i++)
        {
            float randomScale = UnityEngine.Random.Range(0.003f, 0.008f);
            Vector3 randomPos = this.transform.position + new Vector3(UnityEngine.Random.Range(-0.003f, 0.003f), UnityEngine.Random.Range(-0.003f, 0.003f), UnityEngine.Random.Range(-0.003f, 0.003f));

            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            sphere.transform.position = randomPos;
            sphere.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            var rend = sphere.GetComponent<Renderer>();
            rend.material = new Material(rend.material);
            rend.material.color = new Color(153f / 255f, 102f / 255f, 51f / 255f);

            sphere.AddComponent<Rigidbody>();
            sphere.AddComponent<DirtModel>();
        }
    }
}
