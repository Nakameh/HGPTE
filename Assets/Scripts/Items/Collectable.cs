using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public ItemData item;
    private float radius = 3f;

    private void Start()
    {
        // D�termine les collisions avec d'autres objets dans une sph�re autour de cet objet au d�marrage
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        // Parcourir les colliders d�tect�s
        foreach (Collider collider in colliders)
        {
            // Faire quelque chose avec chaque objet en collision
            Debug.Log("Collision avec : " + collider.gameObject.name);
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = item.imageMap;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player && item != null && item.isCollectable)
        {
            player.inventory.Add(this);
            Destroy(gameObject);
        }
    }

    public void setItem(ItemData i)
    {
        this.item = i;
    }
}

