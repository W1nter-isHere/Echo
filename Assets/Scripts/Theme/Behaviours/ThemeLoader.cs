using UnityEngine;

namespace Theme.Behaviours
{
    [ExecuteAlways]
    public abstract class ThemeLoader : MonoBehaviour
    {
        [SerializeField] private bool invert;
        
        protected int Depth;
        protected bool ShouldUseSecondary;

        private void Start()
        {
            Load(ThemeLoadHelper.GetLoadedTheme());
        }

        private void OnEnable()
        {
            Calc();
        }
        
        private void OnValidate()
        {
            Calc();
            Load(ThemeLoadHelper.GetLoadedTheme());
        }

        private int RecursiveSearch(Transform parent, int startingValue = 0)
        {
            if (parent == null)
            {
                return startingValue;
            }

            var isInObject = parent.TryGetComponent<ThemeLoader>(out var parentLoader);
            return RecursiveSearch(parent.parent, isInObject ? parentLoader.invert ? startingValue + 2 : startingValue + 1 : startingValue);
        }

        private void Calc()
        {
            Depth = RecursiveSearch(transform.parent);
            var b = Depth % 2 != 0;
            ShouldUseSecondary = invert ? !b : b;
        }

        public abstract void Load(ThemeSettings themeSettings);
    }
}