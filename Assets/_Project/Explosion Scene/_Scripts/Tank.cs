using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField] float explosionForce = 5f;
    [SerializeField] float explosionRadius = 5f;
    [SerializeField] LayerMask _explosionMask;
    public void Explode()
    {
        var affectedColliders = Physics.OverlapSphere(transform.position, explosionForce, _explosionMask);
        if (affectedColliders.Length > 0)
        {
            foreach (var collider in affectedColliders)
            {
                print(collider.name);
                var cube = collider.GetComponent<Cube>();
                cube.Throw((cube.transform.position - transform.position) * explosionForce);
            }
        }
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}