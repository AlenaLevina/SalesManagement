using AutoMapper;
using Model;
using SalesManagement.MvcApplication.ViewModels.Account;
using SalesManagement.MvcApplication.ViewModels.Order;
using SalesManagement.MvcApplication.ViewModels.Product;
using Profile = Model.Profile;

namespace SalesManagement.MvcApplication
{
    public class SalesManagementProfile : AutoMapper.Profile
    {
        protected override void Configure()
        {

            #region Account View Models

            Mapper.CreateMap<Profile, EditProfileViewModel>();
            Mapper.CreateMap<EditProfileViewModel, Profile>();

            Mapper.CreateMap<Profile, ViewProfileViewModel>();

            #endregion


            #region Order View Models

            Mapper.CreateMap<Client, ClientViewModel>();
            Mapper.CreateMap<ClientViewModel, Client>().ForMember(client=>client.UniqueId,opt=>opt.MapFrom(model=>model.ClientId));

            Mapper.CreateMap<Order, OrderPartialViewModel>();

            Mapper.CreateMap<Order, OrderViewModel>();
            Mapper.CreateMap<OrderViewModel, Order>();

            #endregion

            #region Product View Models

            Mapper.CreateMap<Category, CategoryViewModel>()
                  .ForMember(model => model.Characteristics, opt => opt.Ignore());
            Mapper.CreateMap<CategoryViewModel, Category>().ForMember(category=>category.Characteristics,opt=>opt.Ignore());

            Mapper.CreateMap<Product, ProductViewModel>().ForMember(model=>model.CharacteristicValues,opt=>opt.Ignore());
            Mapper.CreateMap<ProductViewModel, Product>().ForMember(product => product.CharacteristicValues, opt => opt.Ignore());

            #endregion


        }


    }
}