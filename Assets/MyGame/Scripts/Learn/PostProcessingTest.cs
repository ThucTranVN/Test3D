using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingTest : MonoBehaviour
{
    public Volume postProcessVolume;
    private float value = 0;

    int number = 10;

    private void Start()
    {
        print($"Before: {number}");
        // Gọi phương thức ModifyNumber và truyền tham chiếu đến biến number
        ModifyNumber(ref number);

        print($"After: {number}");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnDamage();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if(postProcessVolume.profile.TryGet(out Bloom bloom))
            {
                bloom.intensity.value = 10f;
            }
        }
    }

    private void OnDamage()
    {
        if (postProcessVolume.profile.TryGet(out Vignette vignette))
        {
            value += 0.2f;
            value = Mathf.Clamp(value, 0, 0.6f);
            vignette.intensity.value = value;
        }
    }


    void ModifyNumber(ref int num)
    {
        // Thay đổi giá trị của biến thông qua tham chiếu
        num = 20;
    }

}
