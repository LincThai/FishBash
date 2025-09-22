using UnityEngine;

public class EnvironmentAnimation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Animator anim;

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        anim.SetTrigger("Rise");
    }
}
