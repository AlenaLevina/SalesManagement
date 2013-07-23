using SalesManagement.MvcApplication.ViewModels;
using SalesManagement.MvcApplication.ViewModels.Order;

namespace SalesManagement.MvcApplication.ViewModelBuilders.Order
{
    public class ConfirmViewModelBuilder
    {
        public static ConfirmViewModel Build(OrderPartialViewModel orderPartialViewModel, ActionType actionType)
        {
            return new ConfirmViewModel
                {
                    OrderPartialViewModel = orderPartialViewModel,
                    ActionType = actionType
                };
        }
    }
}