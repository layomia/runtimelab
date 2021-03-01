﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;
using System.Text.Json.Serialization.Converters;

namespace System.Text.Json.Serialization.Metadata
{
    /// <summary>
    /// todo
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class KnownCollectionTypeInfos<T>
    {
        private static JsonTypeInfo<T[]>? s_array;
        /// <summary>
        /// todo
        /// </summary>
        // TODO: Should this return JsonCollectionTypeInfo<T>?
        public static JsonTypeInfo<T[]> GetArray(JsonClassInfo elementInfo, JsonSerializerContext context, JsonNumberHandling? numberHandling)
        {
            if (s_array == null)
            {
                s_array = new JsonCollectionTypeInfo<T[]>(CreateList, new ArrayConverter<T[], T>(), elementInfo, numberHandling, context._options);
            }

            return s_array;
        }

        private static JsonTypeInfo<List<T>>? s_list;
        /// <summary>
        /// todo
        /// </summary>
        public static JsonTypeInfo<List<T>> GetList(JsonClassInfo elementInfo, JsonSerializerContext context, JsonNumberHandling? numberHandling)
        {
            if (s_list == null)
            {
                s_list = new JsonCollectionTypeInfo<List<T>>(CreateList, new ListOfTConverter<List<T>, T>(), elementInfo, numberHandling, context._options);
            }

            return s_list;
        }

        private static JsonTypeInfo<IEnumerable<T>>? s_ienumerable;
        /// <summary>
        /// todo
        /// </summary>
        public static JsonTypeInfo<IEnumerable<T>> GetIEnumerable(JsonClassInfo elementInfo, JsonSerializerContext context, JsonNumberHandling? numberHandling)
        {
            if (s_ienumerable == null)
            {
                s_ienumerable = new JsonCollectionTypeInfo<IEnumerable<T>>(CreateList, new IEnumerableOfTConverter<IEnumerable<T>, T>(), elementInfo, numberHandling, context._options);
            }

            return s_ienumerable;
        }

        private static JsonTypeInfo<IList<T>>? s_ilist;
        /// <summary>
        /// todo
        /// </summary>
        public static JsonTypeInfo<IList<T>> GetIList(JsonClassInfo elementInfo, JsonSerializerContext context, JsonNumberHandling? numberHandling)
        {
            if (s_ilist == null)
            {
                s_ilist = new JsonCollectionTypeInfo<IList<T>>(CreateList, new IListOfTConverter<IList<T>, T>(), elementInfo, numberHandling, context._options);
            }

            return s_ilist;
        }

        private static List<T> CreateList()
        {
            return new List<T>();
        }

        // todo: duplicate the above code for each supported collection type (IEnumerable, IEnumerable<T>, array, etc)
    }
}
