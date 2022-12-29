using UnityEngine;

namespace Utils
{
    public class Keep : MonoBehaviour
    {
        /*
                .Append(image.DOFade(0f, 0.5f).From(1f).SetEase(Ease.InCirc))
                .AppendCallback( () => image.sprite = ResourceManager.Instance.GetAsset<Sprite>("GameType_02/Object/02_manny_ready"))
                .Append(rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y + 420f, 0f))
                .Append(image.DOFade(1f, 0.5f).From(0f).SetEase(Ease.InCirc));
                */
        
        
        /*
            // 깜빡임 구현
            float t = (Mathf.Sin(Time.time - startTime) * colorChangeSpeed);
            GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, t);
        
            yield return new WaitForSeconds(t);
        
            // 깜빡임 종료 후 머테리얼 교체
            ChangeOwnColor();    
            */
    }
}