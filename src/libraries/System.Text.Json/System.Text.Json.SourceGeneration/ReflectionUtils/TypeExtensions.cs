﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace System.Reflection
{
    internal static class TypeExtensions 
    {
        public static string GetUniqueCompilableTypeName(this Type type) => GetCompilableTypeName(type, type.FullName);

        public static string GetCompilableTypeName(this Type type) => GetCompilableTypeName(type, type.Name);

        private static string GetCompilableTypeName(Type type, string name)
        {
            if (!type.IsGenericType)
            {
                return name.Replace('+', '.');
            }

            // TODO: Guard upstream against open generics.
            Debug.Assert(!type.ContainsGenericParameters);

            int backTickIndex = name.IndexOf('`');
            string baseName = name.Substring(0, backTickIndex).Replace('+', '.');

            return $"{baseName}<{string.Join(",", type.GetGenericArguments().Select(arg => GetUniqueCompilableTypeName(arg)))}>";
        }

        public static string GetUniqueFriendlyTypeName(this Type type)
        {
            return GetFriendlyTypeName(type.GetUniqueCompilableTypeName());
        }

        public static string GetFriendlyTypeName(this Type type)
        {
            return GetFriendlyTypeName(type.GetCompilableTypeName());
        }

        private static string GetFriendlyTypeName(string compilableName)
        {
            return compilableName.Replace(".", "").Replace("<", "").Replace(">", "").Replace(",", "").Replace("[]", "Array");
        }

        public static Type NullableOfTType { get; set; }

        public static bool IsNullableValueType(this Type type, out Type? underlyingType)
        {
            Debug.Assert(NullableOfTType != null);

            // TODO: log bug because Nullable.GetUnderlyingType doesn't work due to
            // https://github.com/dotnet/runtimelab/blob/7472c863db6ec5ddab7f411ddb134a6e9f3c105f/src/libraries/System.Private.CoreLib/src/System/Nullable.cs#L124
            // i.e. type.GetGenericTypeDefinition() will never equal typeof(Nullable<>), as expected in that code segment.
            if (type.IsGenericType && type.GetGenericTypeDefinition() == NullableOfTType)
            {
                underlyingType = type.GetGenericArguments()[0];
                return true;
            }

            underlyingType = null;
            return false;
        }

        public static Type? GetCompatibleBaseClass(this Type type, string baseTypeFullName)
        {
            Type? baseTypeToCheck = type;

            while (baseTypeToCheck != null && baseTypeToCheck != typeof(object))
            {
                if (baseTypeToCheck.FullName == baseTypeFullName)
                {
                    return baseTypeToCheck;
                }

                baseTypeToCheck = baseTypeToCheck.BaseType;
            }

            return null;
        }
    }
}
