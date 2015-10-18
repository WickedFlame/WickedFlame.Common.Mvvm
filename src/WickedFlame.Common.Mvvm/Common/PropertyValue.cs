using System;
using System.Linq.Expressions;
using System.Reflection;

namespace WickedFlame.Common.Mvvm.Common
{
    class PropertyValue
    {
        internal static string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException("propertyExpression");

            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
                throw new ArgumentException("PropertySupport_NotMemberAccessExpression_Exception", "propertyExpression");

            var propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo == null)
                throw new ArgumentException("PropertySupport_ExpressionNotProperty_Exception", "propertyExpression");

            if (propertyInfo.GetGetMethod(true).IsStatic)
                throw new ArgumentException("PropertySupport_StaticExpression_Exception", "propertyExpression");

            return memberExpression.Member.Name;
        }
    }
}
