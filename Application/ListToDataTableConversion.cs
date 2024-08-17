
using System.Data;
using System.Reflection;

namespace Application
{
    public class ListToDataTableConversion
    {
        public async Task<DataTable?> ToDataTableAsync<TList>(List<TList> items)
        {
            DataTable? dataTable = null;

            await Task.Run(() =>
            {
                dataTable = new DataTable(typeof(TList).Name);

                PropertyInfo[] lstProperties = typeof(TList).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in lstProperties)
                {
                    var type =
                        (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ?
                        Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType) ?? typeof(string);

                    dataTable.Columns.Add(prop.Name, type);
                }

                int propertiesLength = lstProperties.Length;
                foreach (TList model in items)
                {
                    var values = new object[propertiesLength];
                    for (int i = 0; i < propertiesLength; i++)
                    {
                        values[i] = lstProperties[i]?.GetValue(model, null);
                    }
                    dataTable.Rows.Add(values);
                }
            });

            return dataTable;
        }
    }
}
