using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace NoteTaker.Core.EF
{
    public static class DbContextValidator
    {
        public static void Validate()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var context = GetContext(assembly);

            if (context == null)
            {
                throw new Exception("You need to create a context for Entity Framework that inherits from DbContext.");
            }

            var classesWithTableAttribute = GetClassesWithTableAttribute(assembly);
            var contextTables = GetTablesInContext(context);

            foreach (var table in classesWithTableAttribute)
            {
                if (!contextTables.Contains(table))
                {
                    throw new Exception($"Oops, you need to add {table} to the DbContext.");
                }
            }
        }

        private static List<Type> GetTablesInContext(Type context)
        {
            var contextTables = new List<Type>();
            foreach (var property in context.GetProperties())
            {
                var type = property.PropertyType;
                if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(DbSet<>)) continue;
                var subType = type.GetGenericArguments()[0];
                contextTables.Add(subType);
            }
            return contextTables;
        }

        private static IEnumerable<Type> GetClassesWithTableAttribute(Assembly assembly)
        {
            var tables = new List<Type>();
            foreach (var clazz in assembly.ManifestModule.FindTypes(null, null))
            {
                var tableAttribute = clazz.GetCustomAttributes(typeof(TableAttribute), true)
                    .FirstOrDefault() as TableAttribute;
                if (tableAttribute != null)
                {
                    tables.Add(clazz);
                }
            }
            return tables;
        }

        private static Type GetContext(Assembly assembly)
        {
            Type context = null;
            foreach (var clazz in assembly.ManifestModule.FindTypes(null, null))
            {
                if (clazz.IsSubclassOf(typeof(DbContext)))
                {
                    context = clazz;
                }
            }
            return context;
        }
    }
}