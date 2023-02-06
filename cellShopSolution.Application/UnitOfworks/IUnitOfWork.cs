using cellShopSolution.Application.Repositorys.IRepository;

namespace cellShopSolution.Application.UnitOfworks
{
    public interface IUnitOfWork:IDisposable
    {
        public IProductRepository ProductRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IProductImageRepository ProductImageRepository { get; }
        public IProductTranslationRepository ProductTranslationRepository { get; }
        public IProductInCategoryRepository ProductInCategoryRepository { get; }
        public IUserRepository UserRepository { get; }
        public ILanguageRepository LanguageRepository { get; }
        public ISlideRepository SlideRepository { get; }
        void Save();
        Task<int> SavesChangeAsync();
        void BeginTransaction();
        void Commit();
    }
}
