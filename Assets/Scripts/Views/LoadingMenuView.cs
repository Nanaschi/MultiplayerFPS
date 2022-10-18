using UnityEngine;

namespace Views
{
    public class LoadingMenuView: MonoBehaviour
    {
        [SerializeField] private RectTransform _loadingMenu;
        public RectTransform LoadingMenu => _loadingMenu;
    }
}