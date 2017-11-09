using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calendar
{
    public class RelayCommand : ICommand
    {
        private Action<object> action;
        private Predicate<object> predicate;

        public RelayCommand(Action<object> action)
        {
            this.action = action;
        }

        public RelayCommand(Action<object> action, Predicate<object> predicate) : this(action)
        {
            this.predicate = predicate;
        }

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return predicate != null ? predicate(parameter) : true;
        }

        public void Execute(object parameter)
        {
            action(parameter);
        }
        #endregion
        public void OnCanExecuteChanged()
        {
            CanExecuteChanged.Invoke(null, null);
        }
    }
}
