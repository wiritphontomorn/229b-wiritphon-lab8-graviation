using System;
using System.Collections.Generic;
using UnityEngine;

public class GravityB : MonoBehaviour
{
    private Rigidbody rb;
    private const float G = 0.006674f;
    public static List<GravityB> planetLists;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (planetLists == null)
        {
            planetLists = new List<GravityB>();
        }
        
        planetLists.Add(this);
    }

    private void FixedUpdate()
    {
        foreach (var planet in planetLists)
        {
            if (planet != this)
                Attract(planet);
        }
    }

    void Attract(GravityB other)
    {
        Rigidbody otherRb = other.rb;
        Vector3 direction = rb.position - otherRb.position;
        float distance = direction.magnitude;
        float forceMagnitude = G * ((rb.mass * otherRb.mass) / Mathf.Pow(distance, 2));
        Vector3 finalForce = forceMagnitude * direction.normalized;
        
        otherRb.AddForce(finalForce);
    }
}
