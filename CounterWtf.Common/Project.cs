using Newtonsoft.Json;

namespace CounterWtf.Common
{
	public class Project : ModelBase
	{
	    private string _name;
	    private string _createdBy;
	    
	    [JsonProperty("name")]
	    public string Name
	    {
	        get
	        {
	            return _name;
	        }
	        set
	        {
	            _name = value;
                OnPropertyChanged();
	        }
	    }

	    [JsonProperty("createdBy")]
	    public string CreatedBy
	    {
	        get
	        {
	            return _createdBy;
	        }
	        set
	        {
	            _createdBy = value;
                OnPropertyChanged();
	        }
	    }

	}
}

