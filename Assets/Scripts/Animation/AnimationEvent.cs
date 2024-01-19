using Manager;
using UnityEngine;

namespace Animation
{
    public class AnimationEvent : MonoBehaviour
    {
        private void PlaySound(string soundName)
        {
            GamePoolManager.MainInstance.TryGetPoolItem(soundName , transform.position , Quaternion.identity);
        }
    }
}