﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Tests;

// TODO: we shouldn't have to specify some of these explicitly.
// 1. Generate (linker-trimmable) metadata for some of these type by default.
// 2. Trace the T in JsonSerializer.(De)serialize calls and add those to input serializable types for the generator.

//[module: JsonSerializable(typeof(byte[]))]
//[module: JsonSerializable(typeof(int[]))]
//[module: JsonSerializable(typeof(byte[][]))]

//[module: JsonSerializable(typeof(TestClassWithObjectImmutableTypes))]
[module: JsonSerializable(typeof(SimpleTestClass[]))]
