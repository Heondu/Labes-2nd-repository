using UnityEngine;

public interface IItem
{
    void Use();

    void Diffusion(int weight);

    void MoveToPlayer(Transform target);
}

