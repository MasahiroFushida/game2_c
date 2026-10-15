using UnityEngine;
using UnityEngine.InputSystem;   // 新しい Input System（このプロジェクトでは Input.GetKeyDown は使えない）

// スキャンしたモノをタップ（Mac ではクリック）すると、音を1回鳴らす
public class TapSound : MonoBehaviour
{
    public AudioClip tapClip;          // タップで鳴らす短い音

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 画面を押した瞬間（スマホはタップ、Mac はクリック）
        if (Pointer.current.press.wasPressedThisFrame)
        {
            // 押した点から、カメラの奥へまっすぐ「見えない線（Ray）」を飛ばす
            Vector2 point = Pointer.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(point);

            // 線が Collider に当たって、それがこのモノ（か、その中の子）なら
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.IsChildOf(transform))
            {
                audioSource.PlayOneShot(tapClip);   // 1回鳴らす（ループの音には重なる）
            }
        }
    }
}
