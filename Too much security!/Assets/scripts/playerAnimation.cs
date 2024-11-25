using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimation : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovement player;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        player = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(player.xInput) > 0.1 || Mathf.Abs(player.zInput) > 0.1) 
        {
            _animator.SetBool("Walking", true);
        }
        else
        {
            _animator.SetBool("Walking", false);
        }
    }
}
