using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using CounterWtf.Common;

namespace CounterWtf.Droid
{
    /// <summary>
    /// Adapter for a list of projects.
    /// </summary>
    public class ProjectListAdapter : BaseAdapter<ProjectSummary>
    {
        private readonly Activity _activity;
        private readonly List<ProjectSummary> _projects = new List<ProjectSummary>();
        
        /// <summary>
        /// Gets the total number of projects represented by the adapter.
        /// </summary>
        public override int Count
        {
            get
            {
                return _projects.Count;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectListAdapter"/> class.
        /// </summary>
        /// <param name="activity">The activity that this adapter is used in.</param>
        public ProjectListAdapter(Activity activity)
        {
            _activity = activity;
        }

        /// <summary>
        /// Gets the project at the specified position.
        /// </summary>
        /// <param name="position">Position of the project to retrieve.</param>
        /// <returns>Project at the specified position.</returns>
        public override ProjectSummary this[int position]
        {
            get
            {
                return _projects[position];
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
            View view = convertView; // re-use an existing view, if one is available
            
            if (view == null)
            { // otherwise create a new one
                view = _activity.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
            }

            ProjectSummary summary = _projects[position];

            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = String.Format("{0} - {1} wtfs!", summary.Name, summary.WtfCount);
            return view;
        }

        /// <summary>
        /// Adds an item to the projects to be displayed.
        /// </summary>
        /// <param name="project">Project to add.</param>
        public void Add(ProjectSummary project)
        {
            _projects.Add(project);
            NotifyDataSetChanged();
        }

        /// <summary>
        /// Clears the projects to be displayed.
        /// </summary>
        public void Clear()
        {
            _projects.Clear();
            NotifyDataSetChanged();
        }

        /// <summary>
        /// Removes a project from the list to be displayed.
        /// </summary>
        /// <param name="project">Project to remove</param>
        public void Remove(ProjectSummary project)
        {
            _projects.Remove(project);
            NotifyDataSetChanged();
        }
    }
}