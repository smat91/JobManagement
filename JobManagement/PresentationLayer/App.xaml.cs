using Autofac.Features.ResolveAnything;
using Autofac;
using PresentationLayer.MVVM.ViewModel;
using System.Windows;
using System.Windows.Markup;
using System;
using BusinessLayer.DataAccessConnection;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.Helper;
using DataAccessLayer.Repositories;
using DataAccessLayer.Repositories.Helper;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Interfaces.Helper;
using PresentationLayer.Core;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var builder = new ContainerBuilder();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            builder.RegisterType<AddressConnection>().As<IAddressConnection>().SingleInstance();
            builder.RegisterType<CustomerConnection>().As<ICustomerConnection>().SingleInstance();
            builder.RegisterType<InvoiceConnection>().As<IInvoiceConnection>().SingleInstance();
            builder.RegisterType<ItemConnection>().As<IItemConnection>().SingleInstance();
            builder.RegisterType<ItemGroupConnection>().As<IItemGroupConnection>().SingleInstance();
            builder.RegisterType<OrderConnection>().As<IOrderConnection>().SingleInstance();
            builder.RegisterType<PositionConnection>().As<IPositionConnection>().SingleInstance();
            builder.RegisterType<StatisticsConnection>().As<IStatisticsConnection>().SingleInstance();
            builder.RegisterType<AddressRepository>().As<IAddressRepository>().SingleInstance();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>().SingleInstance();
            builder.RegisterType<InvoiceRepository>().As<IInvoiceRepository>().SingleInstance();
            builder.RegisterType<ItemRepository>().As<IItemRepository>().SingleInstance();
            builder.RegisterType<ItemGroupRepository>().As<IItemGroupRepository>().SingleInstance();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>().SingleInstance();
            builder.RegisterType<PositionRepository>().As<IPositionRepository>().SingleInstance();
            builder.RegisterType<StatisticsRepository>().As<IStatisticsRepository>().SingleInstance();

            IContainer container = builder.Build();

            DISource.Resolver = (type) => {
                return container.Resolve(type);
            };
        }
    }
}
