namespace Bloomify.Repositories.Interfaces
{
    public interface IRepositoryWrapper
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IProviderRepository ProviderRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderItemRepository OrderItemRepository { get; }
        IShoppingCartRepository ShoppingCartRepository { get; }
        IShoppingCartItemRepository ShoppingCartItemRepository { get; }
        IWishlistRepository WishlistRepository { get; }
        IWishlistItemRepository WishlistItemRepository { get; }
        IUserRepository UserRepository { get; }
        IReviewRepository ReviewRepository { get; }
        void Save();
    }
}
