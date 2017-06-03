// ***********************************************************************
// Assembly         : Invisionware.Net.GeoCoding.Google
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 11-04-2016
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 11-04-2016
// ***********************************************************************
// <copyright file="ModelMapper.cs" company="Personal">
//     Copyright (c) Personal. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Invisionware.Net.GeoCoding.Google.Model;
using System;
using XLabs.Ioc;

namespace Invisionware.Net.GeoCoding.Google
{
	/// <summary>
	/// IOC Model Mapper Helper Class.
	/// </summary>
	public static class ModelMapper
	{
		/// <summary>
		/// Registeres all interfaces within this library with the DI framework.
		/// </summary>
		/// <exception cref="System.InvalidOperationException">IOC Framework is not initialized</exception>
		public static void Map()
		{
			if (!Resolver.IsSet)
			{
				throw new InvalidOperationException("IOC Framework is not initialized");
			}

			var container = Resolver.Resolve<IDependencyContainer>();

			container.Register<IGeoCoderProvider>(t => new GoogleGeoCoderProvider());
			container.Register<IGeoLocation, GoogleLocation>();

			Invisionware.Net.GeoCoding.ModelMapper.Map();

		}
	}
}
