using System;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.IO;
using System.Net;
using System.Threading;
using Revenj;
using Revenj.DomainPatterns;
using Revenj.Extensibility;
using Revenj.Plugins.Server.Commands;
using Revenj.Processing;
using Revenj.Security;
using Revenj.Serialization;
using Sales;

namespace RevenjCommandExample
{
	[Export(typeof(IServerCommand))]
	[ExportMetadata(Metadata.ClassType, typeof(RevenjCommandExample))]
	public class RevenjCommandExample : IServerCommand
	{
		private readonly IRepository<Order> Orders;

		public RevenjCommandExample(IRepository<Order> orders)
		{
			Contract.Requires(orders != null);

			this.Orders = orders;
		}

		public ICommandResult<TOutput> Execute<TInput, TOutput>(ISerialization<TInput> input, ISerialization<TOutput> output, TInput data)
		{
			if (data == null)
				return CommandResult<TOutput>.Fail("No input provided.", null);

			var orderURIs = input.Deserialize<TInput, RevenjCommandExampleArguments>(data);
			if (orderURIs == null)
				return CommandResult<TOutput>.Fail("Can't deserialize input data.", null);

			var order1 = Orders.Find(orderURIs.OrderURI1);
			if (order1 == null)
				return CommandResult<TOutput>.Fail("Can't find Order with URI: " + orderURIs.OrderURI1, null);

			var order2 = Orders.Find(orderURIs.OrderURI2);
			if (order2 == null)
				return CommandResult<TOutput>.Fail("Can't find Order with URI: " + orderURIs.OrderURI2, null);

			var difference = order2.CreatedAt.Subtract(order1.CreatedAt);
			var result = output.Serialize(difference);

			return CommandResult<TOutput>.Return(HttpStatusCode.OK, result, null);
		}
	}
}
