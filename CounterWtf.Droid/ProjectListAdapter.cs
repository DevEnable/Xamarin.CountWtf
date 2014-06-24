using System;
using System.Collections.Generic;
using Android.Views;
using Android.Widget;
using CounterWtf.PCL;

namespace CounterWtf.Droid
{
    /// <summary>
    /// Adapter for a list of projects.
    /// </summary>
    public class ProjectListAdapter : BaseAdapter<ProjectSummary>
    {
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
        /// Gets the view for a specific project.
        /// </summary>
        /// <param name="position">Index of the project in the list.</param>
        /// <param name="convertView">The existing view for the row to convert.</param>
        /// <param name="parent">The parent that the row belongs to</param>
        /// <returns>The view to render to the user.</returns>
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            throw new NotImplementedException();
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
        /// <param name="project"></param>
        public void Remove(ProjectSummary project)
        {
            _projects.Remove(project);
            NotifyDataSetChanged();
        }


    }
}