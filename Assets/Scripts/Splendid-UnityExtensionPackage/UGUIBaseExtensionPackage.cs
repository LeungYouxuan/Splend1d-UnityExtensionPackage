using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
namespace Splend1d.UIPackage
{
    #region UI接口
    public interface IHideMyself
    {
        public void HideMySelf(CanvasGroup canvasGroup)
        {
            //PanelObject obj = canvasGroup.GetComponent<PanelObject>();
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            //Debug.Log(canvasGroup.gameObject.name + "隐藏自己");
            //obj.currentPanelState = PanelState.隐藏;
        }
    }
    public interface IShowMyself
    {
        public void ShowMyself(CanvasGroup canvasGroup)
        {
            //PanelObject obj = canvasGroup.GetComponent<PanelObject>();
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            //Debug.Log(canvasGroup.gameObject.name + "显示自己");
            //obj.currentPanelState = PanelState.显示;
        }
    }
    public interface ICanLoadUIDynamic
    {
        public enum AnchorMode
        {
            中心归位,保持不变
        }
        public void LoadUIsDynamic(Transform parent,GameObject child,int loadCount,AnchorMode mode)
        {
            for (int i = 0; i < loadCount; i++)
            {
                var item=Object.Instantiate(child,parent);
                if (mode == AnchorMode.中心归位)
                {
                    item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                }
            }
        }
        public void LoadUIDynamic(Transform parent, GameObject child,AnchorMode mode)
        {
            var item=Object.Instantiate(child,parent);
            if (mode == AnchorMode.中心归位)
            {
                item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            }
        }
    }
    public interface ICanInsertUIElement
    {
        public void InsertUIElement(Transform rootObject,Transform insertObject,int insertIndex)
        {
            insertObject.SetParent(rootObject);
            insertIndex = Mathf.Clamp(insertIndex,0, rootObject.childCount-1);
            insertObject.SetSiblingIndex(insertIndex);
        }
    }
    public interface ICanRemoveUIElement
    {
        public void RemoveUIElementByIndex(Transform rootObject, int index)
        {
            if (index < rootObject.childCount)
            {
                Object.Destroy(rootObject.GetChild(index));
            }
            else
            {
                Debug.LogError("删除的位置不存在物体");
            }
        }
    }
    public interface ICanReverseUIElement
    {
        /// <summary>
        /// 翻转父物体下某个区间的UI的顺序
        /// </summary>
        /// <param name="rootObject">父物体</param>
        /// <param name="startIndex">翻转开始点</param>
        /// <param name="endIndex">翻转结束点</param>
        public void ReverseUIElementOrder(Transform rootObject,int startIndex,int endIndex)
        {
            if(endIndex>=rootObject.childCount)return;
            for (int i = startIndex; i <= endIndex; i++)
            {
                rootObject.GetChild(i).SetSiblingIndex(endIndex);
                rootObject.GetChild(endIndex).SetSiblingIndex(i);
                endIndex--;
            }
        }
        public void ReverseUIElementPosition(Transform rootObject,int startIndex,int endIndex)
        {
            if(endIndex>=rootObject.childCount)return;
            for (int i = startIndex; i <= endIndex; i++)
            {
                (rootObject.GetChild(i).position, rootObject.GetChild(endIndex).position) = (
                    rootObject.GetChild(endIndex).position, rootObject.GetChild(i).position);
                endIndex--;
            }
        }
    }

    public interface ICanSwapUIElementParent
    {
        public void SwapParent(Transform child1, Transform child2)
        {
            Transform temp = child1.parent;
            child1.SetParent(child2.parent);
            child2.SetParent(temp);
        }
    }

    public interface ICanMove
    {
        public async Task MoveByLine(RectTransform obj, Vector3 moveDir, float duration)
        {
            float totalTime = 0;
            Vector2 startPos = obj.anchoredPosition;
            Vector2 targetPos = startPos + new Vector2(moveDir.x, moveDir.y);
            while (totalTime < duration)
            {
                totalTime += Time.deltaTime;
                float t = totalTime / duration; // 计算时间比例
                obj.anchoredPosition = Vector2.Lerp(startPos, targetPos, t); // 线性插值位置
                await UniTask.Yield();
            }
            obj.anchoredPosition = targetPos; // 确保最终位置准确
            Debug.Log("移动完成");
        }
    }
    #endregion
}


