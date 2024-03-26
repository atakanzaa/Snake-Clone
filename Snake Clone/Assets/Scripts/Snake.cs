using UnityEngine;
using System.Collections.Generic;
public class Snake : MonoBehaviour
{
    private Vector2 direction = Vector2.right;
    private List<Transform> segments;
    public Transform segmentPrefab;
    private void Start()
    {
        segments = new List<Transform>();//listemizi initialize ediyoruz.
        segments.Add(this.transform); //basini kuyruga eklemek icin.
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector2.right;
        }
    }
    private void FixedUpdate()//fizik islemlerinin daha tutarli olmasi icin
    {
        for (int i = segments.Count-1; i >0; i--)
        {
            segments[i].position = segments[i - 1].position; //ilk once kuyrugun ilerlemesi gerekiyor.
        }
        this.transform.position = new Vector3(Mathf.Round(transform.position.x) + direction.x,
            Mathf.Round(transform.position.y) + direction.y,//mathf round kullanmamizin nedeni snake belirli gridlerde ilerliyor ve
            //bunlarin tam sayi olmasi gerekiyor.
            0);
        //z ye ihtiyacimiz yok 2d oyun oldugu icin
    }

    private void Grow()
    {
        Transform _segment = Instantiate(this.segmentPrefab);
        _segment.position = segments[segments.Count - 1].position; //snakein buyumesi icin , yeni segmentin pozisyonu
        //mevcut son segmentin pozisyonuna esitlenir ve bu segment listeye eklenir.
        segments.Add(_segment);
    }
    private void ResetState()
    {
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }
        segments.Clear();
        segments.Add(this.transform);
        this.transform.position = Vector3.zero;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
        }
        else if (other.tag == "Obstacle")
        {
            ResetState();
        }
    }
}
