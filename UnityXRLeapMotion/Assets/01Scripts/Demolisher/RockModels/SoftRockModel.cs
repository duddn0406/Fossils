using UnityEngine;

public class SoftRockModel : MonoBehaviour
{
    private int _hitCount;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Test")
        {
            _hitCount++;
            if (_hitCount == 5)
            {
                CreateSoils();
                Destroy(this.gameObject);
            }
        }
    }

    private void CreateSoils()
    {
        int randomSoilCount = Random.Range(5, 10);
        for(int i=0; i < randomSoilCount; i++)
        {
            float randomScale = Random.Range(0.003f, 0.008f);
            Vector3 randomPos = new Vector3(Random.Range(0.001f,0.003f), Random.Range(0.001f,0.003f),Random.Range(0.001f,0.003f));

            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            sphere.transform.position = this.transform.position;
            sphere.transform.localScale = new Vector3(randomScale,randomScale,randomScale);

            var rend = sphere.GetComponent<Renderer>();
            rend.material = new Material(rend.material);
            rend.material.color = new Color(153f / 255f, 102f / 255f, 51f / 255f);
        }
    }
}
