using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace CounterWtf.Common
{
    public class Wtf : ModelBase
    {
        private string _projectId;
        private string _createdBy;
        private DateTime _createdAt;

        [JsonProperty("projectId")]
        public string ProjectId
        {
            get
            {
                return _projectId;
            }
            set
            {
                _projectId = value;
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

        [CreatedAt]
        public DateTime CreatedAt
        {
            get
            {
                return _createdAt;
            }
            set
            {
                _createdAt = value;
                OnPropertyChanged();
            }
        }
    }
}
