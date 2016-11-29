using System;
using System.Web;
using System.Web.Mvc;
using ClearMeasure.Bootcamp.DataAccess;
using ClearMeasure.Bootcamp.DataAccess.Mappings;
using StructureMap;
using UI.DependencyResolution;

namespace ClearMeasure.Bootcamp.UI.DependencyResolution
{
	public class DependencyRegistrarModule : IHttpModule
	{
		private static bool _dependenciesRegistered;
		private static readonly object Lock = new object();
	    internal static IContainer Container = null;

		public void Init(HttpApplication context)
		{
			context.BeginRequest += context_BeginRequest;
		}

		public void Dispose() {}

		private static void context_BeginRequest(object sender, EventArgs e)
		{
			EnsureDependenciesRegistered();
		}

		public static IContainer EnsureDependenciesRegistered()
		{
			if (!_dependenciesRegistered)
			{
				lock (Lock)
				{
					if (!_dependenciesRegistered)
					{
					    Initialize();
                        DataContext.EnsureStartup();
						_dependenciesRegistered = true;
					}
				}
			}

		    return Container;
		}

	    private static void Initialize()
	    {
	        var container = new Container(new StructureMapRegistry());
	        Container = container;
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
	    }

	}
}