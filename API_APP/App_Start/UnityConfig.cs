using System.Web.Http;
using Unity;
using Unity.WebApi;
using Application.Entities;
using Application.DataAccess;

namespace API_APP
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IDataAccess<Department, int>, DepartmentDataAccess>();
            container.RegisterType<IDataAccess<Employee, int>, EmployeeDataAccess>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}