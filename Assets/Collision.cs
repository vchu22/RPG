using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D other) {
        switch (other.gameObject.tag) {
            case "chest":
                StartCoroutine(ShakeChest(other.gameObject, 1.5f, 0.05f));
                break;
            default:
                print("You can't go this way");
                break;
        }
    }

    public IEnumerator ShakeChest(GameObject obj, float duration, float magnitude)
    {
        Destroy(obj.GetComponent<BoxCollider2D>());
        Vector3 orignalPosition = obj.transform.position;
        float elapsed = 0f;
        bool back = false;
        
        while (elapsed < duration)
        {
            if (back) {
                obj.transform.position = orignalPosition;

                back = false;
                continue;
            }
            back = true;
            float dx = Random.Range(-1f, 1f) * magnitude;
            float dy = Random.Range(-1f, 1f) * magnitude;

            obj.transform.position += new Vector3(dx, dy, 0);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        Destroy(obj);
    }
}
