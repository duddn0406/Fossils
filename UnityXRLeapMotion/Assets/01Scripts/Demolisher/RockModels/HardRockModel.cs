using Unity.VisualScripting;
using UnityEngine;
using System;

public class HardRockModel : MonoBehaviour
{
    [SerializeField] private GameObject _dirtParticlePrefab;
    [SerializeField] private GameObject _dirtPrefab;

    private int _destroyHitCount;
    private int _breakHitCount;

    public event Action<int> OnRockDestroyed;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "hand_pick"
            || collision.gameObject.tag == "chisel"
            || collision.gameObject.tag == "trowel")
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
            _destroyHitCount++;
            if (_breakHitCount == 5)
            {
                CreateSoils();
                OnRockDestroyed?.Invoke(-1);
                this.AddComponent<Rigidbody>();
                SoundManager.Instance.PlaySFX("HitRock");
            }
            if (_breakHitCount > 4 && _destroyHitCount == 5)
            {
                Destroy(this.gameObject);
                CreateSoils();
                SoundManager.Instance.PlaySFX("RockDestroy");
            }
        }
        else if(collisionObject.tag == "chisel") //끌
        {
            if(_breakHitCount > 4)
            {
                _destroyHitCount++;
                if (_destroyHitCount == 5)
                {
                    Destroy(this.gameObject);
                    CreateSoils();
                    SoundManager.Instance.PlaySFX("RockDestroy");
                }
            }
        }
        else if(collisionObject.tag == "trowel") //모종삽
        {
            _breakHitCount++;
            if(_breakHitCount ==5)
            {
                CreateSoils();
                OnRockDestroyed?.Invoke(-1);
                this.AddComponent<Rigidbody>();
                SoundManager.Instance.PlaySFX("HitRock");
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
            GameObject dirtClone = Instantiate(_dirtPrefab);
            float randomScale = UnityEngine.Random.Range(0.003f, 0.008f);
            Vector3 randomPos = this.transform.position + new Vector3(UnityEngine.Random.Range(-0.003f, 0.003f), UnityEngine.Random.Range(-0.003f, 0.003f), UnityEngine.Random.Range(-0.003f, 0.003f));

            dirtClone.transform.position = randomPos;
            dirtClone.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        }
    }
}
