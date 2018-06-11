using System.Linq;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Pulse.Infrastructure.MessageQueue;
using Pulse.Infrastructure.MessageQueue.Messages;
using RawRabbit;
using RawRabbit.vNext;

namespace Pulse.Web
{
    public static class Bootstrapper
    {
        public static IContainer SetupContainer(IServiceCollection services)
        {
            var container = new ContainerBuilder();

            container.Populate(services);

            return container.Build();
        }

        public static IBusClient SetupMessageSubscriptions(IServiceCollection services, IContainer container)
        {
            var bus = BusClientFactory.CreateDefault(services);
            var assembly = System.Reflection.Assembly
                .GetAssembly(typeof(Pulse.Infrastructure.StartupTask))
                .GetTypes();

            //var handlers = assembly.Where(t => t.IsClass && t.Namespace == "Pulse.Infrastructure.MessageQueue.Handlers");

            //foreach (var handler in handlers)
            //{
            //    var baseType = handler.BaseType;
            //    var message = baseType.GenericTypeArguments[0];
            //    //var mssg

            //    container
            //}

            bus.SubscribeAsync<ObservationCreated>((message, context) =>
            {
                var handler = container.Resolve<IMessageHandler<ObservationCreated>>();
                return handler.Handle(message);
            }, config => config.WithSubscriberId(string.Empty));

            bus.SubscribeAsync<CarePlanCreated>((plan, context) =>
            {
                var handler = container.Resolve<IMessageHandler<CarePlanCreated>>();
                return handler.Handle(plan);
            }, config => config.WithSubscriberId(string.Empty));

            bus.SubscribeAsync<EncounterCreated>((plan, context) =>
            {
                var handler = container.Resolve<IMessageHandler<EncounterCreated>>();
                return handler.Handle(plan);
            }, config => config.WithSubscriberId(string.Empty));

            bus.SubscribeAsync<PatientCreated>((plan, context) =>
            {
                var handler = container.Resolve<IMessageHandler<PatientCreated>>();
                return handler.Handle(plan);
            }, config => config.WithSubscriberId(string.Empty));

            return bus;
        }
    }
}