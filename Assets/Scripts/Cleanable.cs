using NUnit.Framework;
using UnityEngine;

public class Cleanable : Interactable
{
    [SerializeField]
    private float _outlineThickness = 1.0f;
    private Material _material;

    public override void Interact()
    {
        //TODO Cleaning Mechanic
        Debug.Log("Cleaned!");
        base.Interact();
    }

    protected override void Start()
    {
        base.Start();
        _material = GetComponent<SpriteRenderer>().material;
    }

    public override void Highlight()
    {
        _material.SetFloat("_Thickness", _outlineThickness);
    }

    public override void UnHighlight()
    {
        _material.SetFloat("_Thickness", 0);
    }
}
