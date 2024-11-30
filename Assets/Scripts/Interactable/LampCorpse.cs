using System.Collections;
using UnityEngine;
public class LampCorpse : Cleanable
{
    protected override void Start()
    {
        base.Start();
    }

    public override void Interact()
    {
        base.Interact();
        StartCoroutine(DestroyCorpse(0.9f));
    }

    private IEnumerator DestroyCorpse(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}