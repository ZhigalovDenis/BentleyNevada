using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BN.ViewModels.Base
{
    internal abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected virtual bool Set<T>(ref T field, T Value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, Value)) return false;
            field = Value;
            OnPropertyChanged(PropertyName);
            return true;
        }
    }
}
