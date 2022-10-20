using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpWorld : MonoBehaviour
{
    private BoxCollider2D[] colliders;
    private SpriteRenderer[] renderers;
    
    void Awake()
    {
        colliders = GetComponentsInChildren<BoxCollider2D>();
        renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Global.up_Pulling)
        {
            foreach (BoxCollider2D collider in colliders)
            {
                collider.enabled = true;
            }

            foreach (SpriteRenderer renderer in renderers)
            {
                renderer.enabled = true;
            }
        }

        else{
            foreach (BoxCollider2D collider in colliders)
            {
                collider.enabled = false;
            }
                
            foreach (SpriteRenderer renderer in renderers)
            {
                renderer.enabled = false;
            }
        }
    }
}
