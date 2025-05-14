using UnityEngine;

public class DirtModel : MonoBehaviour
{
    private int _hitCount;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Test"))
        {
            _hitCount++;
            if (_hitCount == 5)
            {
                //CreateSoils();
                Destroy(this.gameObject);
            }
        }
    }

    //흙 붓?으로 닿았을 때 이거 호출
    public void GetDamage()
    {
        _hitCount++;
        if (_hitCount == 5)
        {
            Destroy(this.gameObject);
        }
    }
}
