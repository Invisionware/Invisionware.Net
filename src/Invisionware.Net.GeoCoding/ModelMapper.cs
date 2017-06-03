// ***********************************************************************
// Assembly         : Invisionware.Net.GeoCoding
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
using System;
using XLabs.Ioc;

namespace Invisionware.Net.GeoCoding
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

			if (!Resolver.IsRegistered<IGeoSearchRequest>()) container.Register<IGeoSearchRequest, GeoSearchRequest>();
			if (!Resolver.IsRegistered<IGeoLocation>()) container.Register<IGeoLocation, GeoLocation>();
			if (!Resolver.IsRegistered<IGeoAddress>()) container.Register<IGeoAddress, GeoAddress>();
		}
	}
}
