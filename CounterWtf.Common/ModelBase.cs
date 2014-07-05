using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace CounterWtf.Common
{
    /// <summary>
    /// Base class providing an implementation and helpers for <see cref="INotifyPropertyChanged"/>
    /// </summary>
    public class ModelBase : INotifyPropertyChanged
    {
        private string _id;

        [JsonProperty("id")]
	    public string Id
	    {
	        get
	        {
	            return _id;
	        }
	        set
	        {
	            _id = value;
                OnPropertyChanged();
	        }
	    }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
