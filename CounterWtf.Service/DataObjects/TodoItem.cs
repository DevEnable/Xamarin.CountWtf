using Microsoft.WindowsAzure.Mobile.Service;

namespace CounterWtf.Service.DataObjects
{
    public class TodoItem : EntityData
    {
        public string Text { get; set; }

        public bool Complete { get; set; }
    }
}