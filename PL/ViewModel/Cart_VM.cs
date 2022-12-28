using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModel;

public class Cart_VM: INotifyPropertyChanged
{


    #region PropertyChanged
    private void Set<T>(ref T prop, T val, [CallerMemberName] string? name = "")
    {
        if (!prop.Equals(val))
        {
            prop = val;
            PropertyChanged?.Invoke(this, new(name));
        }
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    #endregion
}
