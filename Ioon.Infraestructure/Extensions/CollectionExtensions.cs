using System.Collections;
using System.Data;
using System.Reflection;

namespace Ioon.Infrastructure.Extensions
{

    public static class CollectionExtensions
    {
        public static void AddRange<TSource>(this ICollection<TSource> Source, IEnumerable<TSource> Values)
        {
            foreach (TSource Value in Values)
            {
                Source.Add(Value);
            }
        }

        public static IEnumerable<TSource> DefaultIfNullOrEmpty<TSource>(this IEnumerable<TSource>? Source)
        {
            return Source ?? Enumerable.Empty<TSource>();
        }

        public static DataTable ToDataTable(this IEnumerable<object> ViewModels)
        {
            DataTable Table = new DataTable();

            Type ViewModelType = ViewModels.GetType().GetInterface($"{nameof(IEnumerable)}`1")!.GenericTypeArguments.First();
            PropertyInfo[] Properties = ViewModelType.GetProperties();

            foreach (PropertyInfo Property in Properties)
            {
                var ColumnType = Property.PropertyType.GetUnderlyingType();
                Table.Columns.Add(Property.Name, ColumnType);
            }

            foreach (var ViewModel in ViewModels)
            {
                Table.Rows.Add(Properties.Select(Property => Property.GetValue(ViewModel)).ToArray());
            }

            return Table;
        }
    }
}