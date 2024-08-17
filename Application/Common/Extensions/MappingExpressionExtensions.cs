using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Extensions
{
    
        public static class MappingExpressionExtensions
        {
            public static IMappingExpression<TSource, TDestination> IgnoreAllMembers<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expr)
            {
                var destinationType = typeof(TDestination);

                foreach (var property in destinationType.GetProperties())
                    expr.ForMember(property.Name, opt => opt.Ignore());

                return expr;
            }
        }
    
}
