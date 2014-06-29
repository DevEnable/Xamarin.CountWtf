using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CounterWtf.Common;

namespace CounterWtf.Droid
{
    public class ProjectWtfListAdapter : BaseAdapter<Wtf>
    {
        private readonly Activity _activity;
        private readonly int _wtfRowLayoutResourceId;
        private readonly List<Wtf> _wtfs = new List<Wtf>();
        
        /// <summary>
        /// Gets the total number of WTFs represented by the adapter.
        /// </summary>
        public override int Count
        {
            get
            {
                return _wtfs.Count;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectWtfListAdapter"/> class.
        /// </summary>
        /// <param name="activity">The activity that this adapter is used in.</param>
        /// <param name="wtfRowLayoutResourceId">Internal ID for the layout of the WTF rows.</param>
        public ProjectWtfListAdapter(Activity activity, int wtfRowLayoutResourceId)
        {
            _activity = activity;
            _wtfRowLayoutResourceId = wtfRowLayoutResourceId;
        }

        /// <summary>
        /// Gets the WTF at the specified position.
        /// </summary>
        /// <param name="position">Position of the WTF to retrieve.</param>
        /// <returns>WTF at the specified position.</returns>
        public override Wtf this[int position]
        {
            get
            {
                return _wtfs[position];
            }
        }

        /// <summary>
        /// Gets the unique ID of the item.
        /// </summary>
        /// <param name="position">The position of the item in the list.</param>
        /// <returns>ID of the item.</returns>
        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView; // re-use an existing view, if one is available
            
            if (row == null)
            { // otherwise create a new one
                row = _activity.LayoutInflater.Inflate(_wtfRowLayoutResourceId, parent, false);
            }

            Wtf wtf = _wtfs[position];
            row.FindViewById<TextView>(Resource.Id.addedBy).Text = wtf.CreatedBy;
            row.FindViewById<TextView>(Resource.Id.addedDate).Text = wtf.CreatedAt.ToString();
            return row;
        }

        /// <summary>
        /// Adds an item to the WTFs to be displayed.
        /// </summary>
        /// <param name="wtf">WTF to add.</param>
        public void Add(Wtf wtf)
        {
            _wtfs.Add(wtf);
            NotifyDataSetChanged();
        }

        /// <summary>
        /// Clears the WTFs to be displayed.
        /// </summary>
        public void Clear()
        {
            _wtfs.Clear();
            NotifyDataSetChanged();
        }

        /// <summary>
        /// Removes a WTF from the list to be displayed.
        /// </summary>
        /// <param name="wtf">WTF to remove</param>
        public void Remove(Wtf wtf)
        {
            _wtfs.Remove(wtf);
            NotifyDataSetChanged();
        }
    }
}