using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.ViewModel
{
    public abstract class ViewModelBase
    : INotifyPropertyChanged, IDisposable
    {
        #region Constructor
        protected ViewModelBase() { }
        #endregion // Constructor

        #region Debugging Aides
        /// <summary>
        /// metoda nie wystepuje w wersji reklease projektu
        /// </summary>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;
                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }
        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }
        #endregion // Debugging Aides

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        #endregion // INotifyPropertyChanged Members

        #region IDisposable Members
        public void Dispose()
        {
            this.OnDispose();
        }
        protected virtual void OnDispose()
        {
        }
        #endregion // IDisposable Members
    }

}
