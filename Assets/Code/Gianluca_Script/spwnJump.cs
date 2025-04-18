using UnityEngine;

public class spwnJump : MonoBehaviour
{
    public GameObject jump;

    // Update is called once per frame
    void Update()
    {
        GameObject sup = null;
        if (sup == null)
            sup = Instantiate(jump, transform.position, Quaternion.identity);
        else
            return;
    }
}
