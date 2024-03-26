using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;

    private void Start()
    {
        RandomizePosition();
    }

    private void RandomizePosition()
    {
        Bounds bounds = gridArea.bounds; //sinirlarimizi belirledik.
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") //snake objesi food objesinin collideri ile etkilesime gecerse foodun positionu degisecek.
        {
            RandomizePosition();
        }
    }

    
}
