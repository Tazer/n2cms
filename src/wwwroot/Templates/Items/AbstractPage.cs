using System.Collections.Generic;
using N2.Collections;
using N2.Details;
using N2.Web.UI;

namespace N2.Templates.Items
{
	/// <summary>
	/// A base class for page items in the template project. Provides automatic
	/// wiring to the default location of the aspx template and access to parts
	/// added to recursive zones of a parent.
	/// </summary>
	[WithEditableTitle("Title", 10, Focus = true, ContainerName = Tabs.Content)]
	[TabContainer(Tabs.Content, "Content", Tabs.ContentIndex)]
    [TabContainer(Tabs.Advanced, "Advanced", Tabs.AdvancedIndex)]
    public abstract class AbstractPage : ContentItem
	{
	    /// <summary>Defaults to ~/Templates/UI/Img/{IconName}.png. Override <see cref="IconName"/> to reference icon in same directory.</summary>
        public override string IconUrl
        {
            get { return "~/Templates/UI/Img/" + IconName + ".png"; }
        }

        /// <summary>The name without extension .png of an icon file located in /Templates/UI/Img/. Defaults to "page".</summary>
        protected virtual string IconName
	    {
            get { return "page"; }
	    }

		[EditableCheckBox("Show Title", 60, ContainerName = Tabs.Advanced)]
		public virtual bool ShowTitle
		{
			get { return (bool)(GetDetail("ShowTitle") ?? true); }
			set { SetDetail("ShowTitle", value, true); }
		}

		public virtual IList<T> GetChildren<T>() where T : ContentItem
		{
			return new ItemList<T>(Children,
			                       new AccessFilter(),
			                       new TypeFilter(typeof(T)));
		}

		public virtual IList<T> GetChildren<T>(string zoneName) where T : ContentItem
		{
			return new ItemList<T>(Children,
			                       new AccessFilter(),
			                       new TypeFilter(typeof(T)),
			                       new ZoneFilter(zoneName));
		}
	}
}