using Dapper;
using Ioon.Domain.Common.Interfaces.Base;
using System.Data;
using System.Reflection;

namespace Ioon.Infrastructure.Extensions
{
    public static class DapperExtensions
    {
        /// <summary>
        /// Convierte un objeto a DynamicParameters para su uso en consultas a bases de datos.
        /// </summary>
        /// <param name="model">Objeto a convertir.</param>
        /// <param name="IgnoreParams">Lista de propiedades a ignorar durante la conversión.</param>
        /// <returns>DynamicParameters que representa las propiedades del objeto.</returns>
        /// <exception cref="ArgumentNullException">Se produce si el objeto proporcionado es nulo.</exception>
        public static DynamicParameters ToDynamicParameters(this IEntity model, string[]? IgnoreParams = null)
        {
            // Comprobar si el objeto proporcionado es nulo
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            // Crear una instancia de DynamicParameters de Dapper
            DynamicParameters parameters = new DynamicParameters();

            // Obtener todas las propiedades públicas del objeto utilizando reflexión
            PropertyInfo[] properties = model.GetType().GetProperties();

            // Iterar sobre cada propiedad del objeto
            foreach (var property in properties)
            {
                // Comprobar si se proporciona una lista de propiedades a ignorar, o si la propiedad no está en la lista
                if (IgnoreParams == null || !IgnoreParams.Contains(property.Name, StringComparer.OrdinalIgnoreCase))
                {
                    // Llamar a un método para agregar la propiedad actual a los parámetros
                    AddPropertyToParameters(property, model, parameters);
                }
            }

            // Devolver los DynamicParameters que representan las propiedades del objeto
            return parameters;
        }

        /// <summary>
        /// Agrega las propiedades públicas de un objeto a un conjunto existente de DynamicParameters.
        /// </summary>
        /// <param name="model">Objeto del cual agregar las propiedades a los parámetros.</param>
        /// <param name="parameters">DynamicParameters existente al que se agregarán las propiedades.</param>
        /// <exception cref="ArgumentNullException">Se produce si el objeto proporcionado es nulo.</exception>
        public static void AddParams(this IEntity model, DynamicParameters parameters)
        {
            // Comprobar si el objeto proporcionado es nulo
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            // Obtener todas las propiedades públicas del objeto utilizando reflexión
            PropertyInfo[] properties = model.GetType().GetProperties();

            // Iterar sobre cada propiedad y agregarla a los parámetros
            foreach (var property in properties)
            {
                // Llamar a un método para agregar la propiedad actual a los parámetros
                AddPropertyToParameters(property, model, parameters);
            }
        }

        /// <summary>
        /// Agrega una propiedad al objeto DynamicParameters.
        /// </summary>
        /// <param name="property">Propiedad a agregar.</param>
        /// <param name="model">Objeto que contiene la propiedad.</param>
        /// <param name="parameters">Objeto DynamicParameters al que se agregarán los parámetros.</param>
        private static void AddPropertyToParameters(PropertyInfo property, object model, DynamicParameters parameters)
        {
            // Obtener el valor de la propiedad actual
            var value = property.GetValue(model);

            // Verificar si el valor no es nulo
            if (value != null)
            {
                // Verificar si el valor es una colección de objetos
                if (value is IEnumerable<object> values)
                {
                    // Si es una colección, convertirla a un DataTable y agregarlo como parámetro
                    parameters.Add(property.Name, values.ToDataTable(), DbType.Object, ParameterDirection.Input);
                }
                else
                {
                    // Si no es una colección, agregar el valor directamente como parámetro
                    parameters.Add(property.Name, value);
                }
            }
        }


    }
}
