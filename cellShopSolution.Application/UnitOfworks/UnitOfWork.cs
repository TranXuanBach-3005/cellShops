using cellShopSolution.Application.Repositorys.IRepository;
using cellShopSolution.Data.EF;

namespace cellShopSolution.Application.UnitOfworks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly cellShopDbContext _cellcontext;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly IProductTranslationRepository _productTranslationRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly ISlideRepository _slideRepository;
        private readonly IProductInCategoryRepository _productInCategoryRepository;

        public UnitOfWork(cellShopDbContext context, IProductRepository productRepository,
                                                     IProductTranslationRepository productTranslationRepository,
                                                     IProductImageRepository productImageRepository,
                                                     IUserRepository userRepository,
                                                     ILanguageRepository languageRepository,
                                                     ICategoryRepository categoryRepository,
                                                     IProductInCategoryRepository productInCategoryRepository,
                                                     ISlideRepository slideRepository)
        {
            _cellcontext = context;
            _productRepository = productRepository;
            _productTranslationRepository = productTranslationRepository;
            _productImageRepository = productImageRepository;
            _userRepository = userRepository;
            _languageRepository = languageRepository;
            _categoryRepository = categoryRepository;
            _productInCategoryRepository = productInCategoryRepository;
            _slideRepository = slideRepository;
        }

        public IProductRepository ProductRepository { get => _productRepository; }
        public IProductImageRepository ProductImageRepository { get => _productImageRepository; }

        public IProductTranslationRepository ProductTranslationRepository { get => _productTranslationRepository; }

        public IUserRepository UserRepository { get => _userRepository; }

        public ILanguageRepository LanguageRepository { get => _languageRepository; }

        public ICategoryRepository CategoryRepository { get => _categoryRepository; }

        public IProductInCategoryRepository ProductInCategoryRepository { get => _productInCategoryRepository; }

        public ISlideRepository SlideRepository { get => _slideRepository; }
        public void BeginTransaction()
        {
            _cellcontext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _cellcontext.Database.CommitTransaction();
        }

        public void Dispose()
        {
            _cellcontext.Dispose();
        }

        public void Save()
        {
            _cellcontext.SaveChanges();
        }

        public async Task<int> SavesChangeAsync()
        {
            return await _cellcontext.SaveChangesAsync();
        }
    }
}
