using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;

namespace AituConnectApp.Behaviors
{
    public class AutoSizingCollectionViewBehavior : Behavior<CollectionView>
    {
        public static readonly BindableProperty ItemHeightProperty =
            BindableProperty.Create(nameof(ItemHeight), typeof(double), typeof(AutoSizingCollectionViewBehavior), 80.0);

        public static readonly BindableProperty MaxHeightProperty =
            BindableProperty.Create(nameof(MaxHeight), typeof(double), typeof(AutoSizingCollectionViewBehavior), 400.0);

        public double ItemHeight
        {
            get => (double)GetValue(ItemHeightProperty);
            set => SetValue(ItemHeightProperty, value);
        }

        public double MaxHeight
        {
            get => (double)GetValue(MaxHeightProperty);
            set => SetValue(MaxHeightProperty, value);
        }

        CollectionView? _associated;

        protected override void OnAttachedTo(CollectionView bindable)
        {
            base.OnAttachedTo(bindable);
            _associated = bindable;

            // react to ItemsSource changes
            bindable.PropertyChanged += OnCollectionViewPropertyChanged;
            HookCollectionChanged(bindable.ItemsSource);
            UpdateHeight();
        }

        protected override void OnDetachingFrom(CollectionView bindable)
        {
            base.OnDetachingFrom(bindable);
            UnhookCollectionChanged(bindable.ItemsSource);
            bindable.PropertyChanged -= OnCollectionViewPropertyChanged;
            _associated = null;
        }

        void OnCollectionViewPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CollectionView.ItemsSource))
            {
                UnhookCollectionChanged((sender as CollectionView)?.ItemsSource);
                HookCollectionChanged((sender as CollectionView)?.ItemsSource);
                UpdateHeight();
            }
        }

        void HookCollectionChanged(object? itemsSource)
        {
            if (itemsSource is INotifyCollectionChanged notify)
            {
                notify.CollectionChanged += OnItemsCollectionChanged;
            }
        }

        void UnhookCollectionChanged(object? itemsSource)
        {
            if (itemsSource is INotifyCollectionChanged notify)
            {
                notify.CollectionChanged -= OnItemsCollectionChanged;
            }
        }

        void OnItemsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateHeight();
        }

        void UpdateHeight()
        {
            if (_associated == null) return;

            var items = _associated.ItemsSource;
            int count = 0;

            if (items is ICollection col)
            {
                count = col.Count;
            }
            else if (items != null)
            {
                // Fallback: enumerate (works for simple IEnumerable)
                try
                {
                    count = items.Cast<object>().Count();
                }
                catch
                {
                    count = 0;
                }
            }

            if (count <= 0)
            {
                _associated.HeightRequest = 0;
                return;
            }

            var desired = count * ItemHeight;
            // clamp to MaxHeight so huge lists do not explode UI
            var height = Math.Min(desired, MaxHeight);

            // give a little extra padding
            _associated.HeightRequest = Math.Ceiling(height) + 2;
        }
    }
}
