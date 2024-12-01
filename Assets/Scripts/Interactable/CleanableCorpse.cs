using System.Collections;
using UnityEngine;
public class CleanableCorpse : Cleanable
{
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