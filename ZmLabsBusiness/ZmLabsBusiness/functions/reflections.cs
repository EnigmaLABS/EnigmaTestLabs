using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ZmLabsBusiness.functions
{
    public static class reflections
    {
        public static DataTable CreateDataTable<T>(IEnumerable<T> list, List<string> ExcludedProp)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();

            int numElems = 0;

            foreach (PropertyInfo info in properties)
            {
                if (!ExcludedProp.Exists(pn => pn == info.Name))
                {
                    dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
                    numElems++;
                }
            }

            foreach (T entity in list)
            {
                object[] values = new object[numElems];

                for (int i = 0; i < numElems; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }
}
