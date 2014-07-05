using Newtonsoft.Json;

namespace CounterWtf.Common
{
    public class ProjectSummary : Project
    {
        private int _wtfCount;

        [JsonProperty("wtfCount")]
        public int WtfCount
        {
            get
            {
                return _wtfCount;
            }
            set
            {
                _wtfCount = value;
                OnPropertyChanged();
            }
        }
    }
}
