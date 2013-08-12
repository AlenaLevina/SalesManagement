using System;
using System.ComponentModel.DataAnnotations;
using Common.Dependency;
using Contracts;


namespace SalesManagement.MvcApplication.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property|AttributeTargets.Field,AllowMultiple = false)]
    public sealed class UniqueIdExistsAttribute : ValidationAttribute
    {
        //TODO inherit IClientValidatable for client validation
        //http://www.codeproject.com/Articles/301022/Creating-Custom-Validation-Attribute-in-MVC-3
        
        public override bool IsValid(object value)
        {
            bool result = true;
            var uniqueId = value as int?;
            if (uniqueId!=null&&uniqueId.Value>0)
            {
                if (!DependencyResolver.Current.Resolve<IOrderService>().UniqueIdExists(uniqueId.Value)) result = false;
            }
            return result;
        }
    }
}