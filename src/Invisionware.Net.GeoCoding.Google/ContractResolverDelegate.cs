using System;
using Newtonsoft.Json.Serialization;

namespace Invisionware.Net.GeoCoding.Google
{
	internal class ContractResolverDelegate : DefaultContractResolver
	{
		private readonly Func<Type, bool> canCreate;
		private readonly Func<Type, object> creator;

		public ContractResolverDelegate(Func<Type, bool> canCreate, Func<Type, object> creator)
		{
			this.canCreate = canCreate;
			this.creator = creator;
		}

		protected override JsonObjectContract CreateObjectContract(Type objectType)
		{
			JsonObjectContract objectContract = base.CreateObjectContract(objectType);
			if (this.canCreate == null || this.canCreate(objectType))
				objectContract.DefaultCreator = (Func<object>) (() => this.creator(objectType));
			return objectContract;
		}
	}
}