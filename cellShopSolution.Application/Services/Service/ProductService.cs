using AutoMapper;
using cellShopSloution.ViewModel.Dtos.ProductImage;
using cellShopSloution.ViewModel.Dtos.Products;
using cellShopSloution.ViewModel.Dtos.Responses;
using cellShopSolution.Application.Services.IService;
using cellShopSolution.Application.UnitOfworks;
using cellShopSolution.Data.EF;
using cellShopSolution.Data.Entities;
using cellShopSolution.Utilities.Constants;
using cellShopSolution.Utilities.Exceptions;
using cellShopSolution.ViewModel.Dtos;
using cellShopSolution.ViewModel.Dtos.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace cellShopSolution.Application.Services.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        private readonly cellShopDbContext _dbContext;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService
                              , cellShopDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;
            _dbContext = dbContext;
        }

        public async Task AddViewCount(int productId)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            product.ViewCount += 1;
            await _unitOfWork.SavesChangeAsync();
        }

        public async Task<ApiResponse<bool>> CategoryAssignAsync(int id, CategoryAssignRequest categoryAssign)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
            {
                return new ApiErrorResponse<bool>("Sản phẩm không tồn tại");
            }
            foreach (var category in categoryAssign.Categories)
            {
                var productInCategory = await _dbContext.ProductCategories
                    .FirstOrDefaultAsync(x => x.CategoryId == int.Parse(category.Id)
                    && x.ProductId == id);

                if (productInCategory != null && category.Selected == false)
                {
                    _dbContext.ProductCategories.Remove(productInCategory);
                }
                else if (productInCategory == null && category.Selected)
                {
                    await _dbContext.ProductCategories.AddAsync(new ProductInCategory()
                    {
                        CategoryId = int.Parse(category.Id),
                        ProductId = id
                    });
                }
            }
            await _unitOfWork.SavesChangeAsync();
            return new ApiSuccessResponse<bool>();

        }

        public async Task<int> CreateImageAsync(int productId, ProductImageCreateRequest productImageCreateRequest)
        {
            var productImage = new ProductImage()
            {
                SortOrder = productImageCreateRequest.SortOrder,
                ProductId = productId,
                IsDefault = productImageCreateRequest.IsDefault,
                Caption = productImageCreateRequest.Caption,
                DateCreated = DateTime.Now
            };
            if (productImageCreateRequest.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(productImageCreateRequest.ImageFile);
                productImage.FileSize = productImageCreateRequest.ImageFile.Length;
            }
            await _unitOfWork.ProductImageRepository.CreateAsync(productImage);
            await _unitOfWork.SavesChangeAsync();
            return productImage.Id;
        }

        public async Task<int> CreateProductAsync(ProductCreateRequest createRequest)
        {
            var languages = _dbContext.Languages;
            var translations = new List<ProductTranslation>();
            foreach(var language in languages)
            {
                if(language.Id == createRequest.LanguageId)
                {
                    translations.Add(new ProductTranslation() {
                        Name = createRequest.Name,
                        Description = createRequest.Description,
                        Details = createRequest.Details,
                        SeoDescription = createRequest.SeoDescription,
                        SeoAlias = createRequest.SeoAlias,
                        SeoTitle = createRequest.SeoTitle,
                        LanguageId = createRequest.LanguageId
                    });
                }
                else
                {
                    translations.Add(new ProductTranslation()
                    {
                        Name = SystemConstant.ProductConstants.NA,
                        Description = SystemConstant.ProductConstants.NA,
                        SeoAlias = SystemConstant.ProductConstants.NA,
                        SeoDescription = SystemConstant.ProductConstants.NA,
                        SeoTitle = SystemConstant.ProductConstants.NA,
                        Details = SystemConstant.ProductConstants.NA,
                        LanguageId = language.Id
                    });
                }
            }
            var product = new Product()
            {
                Price = createRequest.Price,
                OriginaPrice = createRequest.OriginaPrice,
                Stock = createRequest.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                ProductTranslations = translations
            };
            if (createRequest.ProductslImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Product Image",
                        DateCreated = DateTime.Now,
                        FileSize = createRequest.ProductslImage.Length,
                        ImagePath = await this.SaveFile(createRequest.ProductslImage),
                        IsDefault = true,
                        SortOrder = 1,
                    }
                };
            }
            await _unitOfWork.ProductRepository.CreateAsync(product);
            await _unitOfWork.SavesChangeAsync();
            return product.Id;
        }

        public async Task<int> DeleteImageAsync(int imageId)
        {
            var productImage = await _unitOfWork.ProductImageRepository.GetByIdAsync(imageId);
            _unitOfWork.ProductImageRepository.Delete(productImage);
            return await _unitOfWork.SavesChangeAsync();
        }

        public async Task<int> DeleteProductAsync(int productId)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            if (product == null) throw new cellException("not null");
            var productImage = await _unitOfWork.ProductImageRepository.DeleteProductImageAsync(productId);
            foreach (var image in productImage)
            {
                await _imageService.DeleteFileAsync(image.ImagePath);
            }
            _unitOfWork.ProductRepository.Delete(product);
            return await _unitOfWork.SavesChangeAsync();
        }

        public Task<PageResult<ProductViewModel>> GetAllByCategoryId(GetProductPagingRequest productPagingRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductViewModel> GetByIdProductAsync(int productId, string languageId)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            var productTranslation = await _unitOfWork.ProductTranslationRepository.GetProductTranslationAsync(productId, languageId);
            var categories = await (from c in _dbContext.Categories
                                    join ct in _dbContext.CategoryTranslations on c.Id equals ct.CategoryId
                                    join pic in _dbContext.ProductCategories on ct.LanguageId equals languageId
                                    where pic.ProductId == productId && ct.LanguageId == languageId
                                    select ct.Name).ToListAsync();
            var productImage = await _unitOfWork.ProductImageRepository.GetByIdProductImamge(productId);
            var productViewModel = new ProductViewModel()
            {
                Id = product.Id,
                DateCreated = product.DateCreated,
                OriginaPrice = product.OriginaPrice,
                Price = product.Price,
                Stock = product.Stock,
                ViewCount = product.ViewCount,
                Description = productTranslation.Description,
                Details = productTranslation.Details,
                Name = productTranslation.Name,
                SeoAlias = productTranslation.SeoAlias,
                SeoDescription = productTranslation.Description,
                SeoTitle = productTranslation.SeoTitle,
                LanguageId = productTranslation.LanguageId,
                ProductslImage = productImage.ImagePath,
                Categories = categories,

            };
            return productViewModel;
        }

        public async Task<List<ProductViewModel>> GetListFeatured(string languageId, int take)
        {
            var query = from p in _dbContext.Products
                        join pt in _dbContext.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _dbContext.ProductCategories on p.Id equals pic.ProductId into ppic
                        from pic in ppic.DefaultIfEmpty()
                        join c in _dbContext.Categories on pic.CategoryId equals c.Id into picc
                        from c in picc.DefaultIfEmpty()
                        join pi in _dbContext.ProductImages on p.Id equals pi.ProductId into ppi
                        from pi in ppi.DefaultIfEmpty()
                        where pt.LanguageId == languageId && pi.IsDefault == true
                        select new { p, pt, pic, c, pi };

            var productData = await query.OrderByDescending(x => x.p.DateCreated).Take(take)
                                     .Select(x => new ProductViewModel()
                                     {
                                         Id = x.p.Id,
                                         Name = x.pt.Name,
                                         DateCreated = x.p.DateCreated,
                                         Description = x.pt.Description,
                                         Details = x.pt.Details,
                                         LanguageId = x.pt.LanguageId,
                                         OriginaPrice = x.p.OriginaPrice,
                                         Price = x.p.Price,
                                         SeoAlias = x.pt.SeoAlias,
                                         SeoDescription = x.pt.SeoDescription,
                                         SeoTitle = x.pt.SeoTitle,
                                         Stock = x.p.Stock,
                                         ViewCount = x.p.ViewCount,
                                         ProductslImage = x.pi.ImagePath
                                     }).ToListAsync();

            return productData;
        }

        public async Task<List<ProductImageViewModel>> GetListImage(int productId)
        {
            var productImage = await _unitOfWork.ProductImageRepository.GetProductImageAsync(productId);
            var productImageModel = _mapper.Map<List<ProductImageViewModel>>(productImage);
            return productImageModel;
        }

        public async Task<List<ProductViewModel>> GetListLatest(string languageId, int take)
        {
            var query = from p in _dbContext.Products
                        join pt in _dbContext.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _dbContext.ProductCategories on p.Id equals pic.ProductId into ppic
                        from pic in ppic.DefaultIfEmpty()
                        join c in _dbContext.Categories on pic.CategoryId equals c.Id into picc
                        from c in picc.DefaultIfEmpty()
                        join pi in _dbContext.ProductImages on p.Id equals pi.ProductId into ppi
                        from pi in ppi.DefaultIfEmpty()
                        where pt.LanguageId == languageId
                        select new { p, pt, pic, c, pi };

            var productData = await query.OrderByDescending(x => x.p.DateCreated).Take(take)
                                     .Select(x => new ProductViewModel()
                                     {
                                         Id = x.p.Id,
                                         Name = x.pt.Name,
                                         DateCreated = x.p.DateCreated,
                                         Description = x.pt.Description,
                                         Details = x.pt.Details,
                                         LanguageId = x.pt.LanguageId,
                                         OriginaPrice = x.p.OriginaPrice,
                                         Price = x.p.Price,
                                         SeoAlias = x.pt.SeoAlias,
                                         SeoDescription = x.pt.SeoDescription,
                                         SeoTitle = x.pt.SeoTitle,
                                         Stock = x.p.Stock,
                                         ViewCount = x.p.ViewCount,
                                         ProductslImage = x.pi.ImagePath
                                     }).ToListAsync();

            return productData;
        }

        public async Task<List<ProductViewModel>> GetListRelated(string languageId, int take)
        {
            var query = from p in _dbContext.Products
                        join pt in _dbContext.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _dbContext.ProductCategories on p.Id equals pic.ProductId into ppic
                        from pic in ppic.DefaultIfEmpty()
                        join c in _dbContext.Categories on pic.CategoryId equals c.Id into picc
                        from c in picc.DefaultIfEmpty()
                        join pi in _dbContext.ProductImages on p.Id equals pi.ProductId into ppi
                        from pi in ppi.DefaultIfEmpty()
                        where pt.LanguageId == languageId && pic.ProductId == p.Id
                        select new { p, pt, pic, c, pi };

            var productData = await query.OrderByDescending(x => x.p.DateCreated).Take(take)
                                     .Select(x => new ProductViewModel()
                                     {
                                         Id = x.p.Id,
                                         Name = x.pt.Name,
                                         DateCreated = x.p.DateCreated,
                                         Description = x.pt.Description,
                                         Details = x.pt.Details,
                                         LanguageId = x.pt.LanguageId,
                                         OriginaPrice = x.p.OriginaPrice,
                                         Price = x.p.Price,
                                         SeoAlias = x.pt.SeoAlias,
                                         SeoDescription = x.pt.SeoDescription,
                                         SeoTitle = x.pt.SeoTitle,
                                         Stock = x.p.Stock,
                                         ViewCount = x.p.ViewCount,
                                         ProductslImage = x.pi.ImagePath
                                     }).ToListAsync();

            return productData;

        }

        public async Task<PageResult<ProductViewModel>> GettAllPagingAsync(GetProductPagingRequest pagingRequest)
        {
            var query = from p in _dbContext.Products
                        join pt in _dbContext.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _dbContext.ProductCategories on p.Id equals pic.ProductId into ppic
                        from pic in ppic.DefaultIfEmpty()
                        join c in _dbContext.Categories on pic.CategoryId equals c.Id into picc
                        from c in picc.DefaultIfEmpty()
                        join pi in _dbContext.ProductImages on p.Id equals pi.ProductId into ppi
                        from pi in ppi.DefaultIfEmpty()
                        where pt.LanguageId == pagingRequest.LanguageId && pi.IsDefault == true
                        select new { p, pt, pic, c, pi };
            if (!string.IsNullOrEmpty(pagingRequest.Keyword))
                query = query.Where(x => x.pt.Name.Contains(pagingRequest.Keyword));
            if (pagingRequest.CategoryId != null && pagingRequest.CategoryId != 0)
            {
                query = query.Where(p => p.pic.CategoryId == pagingRequest.CategoryId);
            }

            int totalRow = await query.CountAsync();
            var productData = await query.Skip((pagingRequest.PageIndex - 1) * pagingRequest.PageSize)
                                    .Take(pagingRequest.PageSize)
                                    .Select(x => new ProductViewModel()
                                    {
                                        Id = x.p.Id,
                                        Name = x.pt.Name,
                                        DateCreated = x.p.DateCreated,
                                        Description = x.pt.Description,
                                        Details = x.pt.Details,
                                        LanguageId = x.pt.LanguageId,
                                        OriginaPrice = x.p.OriginaPrice,
                                        Price = x.p.Price,
                                        SeoAlias = x.pt.SeoAlias,
                                        SeoDescription = x.pt.SeoDescription,
                                        SeoTitle = x.pt.SeoTitle,
                                        Stock = x.p.Stock,
                                        ViewCount = x.p.ViewCount,
                                        ProductslImage = x.pi.ImagePath
                                    }).ToListAsync();
            var paged = new PageResult<ProductViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = pagingRequest.PageIndex,
                PageSize = pagingRequest.PageSize,
                Items = productData
            };
            return paged;
        }

        public async Task<int> UpdateImageAsync(int imageId, ProductImageUpdateRequest productImageUpdateRequest)
        {
            var productImage = await _unitOfWork.ProductImageRepository.GetByIdAsync(imageId);
            if (productImageUpdateRequest.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(productImageUpdateRequest.ImageFile);
                productImage.FileSize = productImageUpdateRequest.ImageFile.Length;
            }
            _unitOfWork.ProductImageRepository.Update(productImage);
            await _unitOfWork.SavesChangeAsync();
            return productImage.Id;
        }

        public async Task<bool> UpdatePriceAsync(int productId, decimal newPrice)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            if (product == null) throw new cellException("not null");
            product.Price = newPrice;
            return await _unitOfWork.SavesChangeAsync() > 0;
        }

        public async Task<ProductUpdateRequest> UpdateProductAsync(int productId, ProductUpdateRequest updateRequest)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            var productTranslation = await _unitOfWork.ProductTranslationRepository.UpdateProductTranslationAsync(updateRequest);
            if (product == null || productTranslation == null) throw new cellException("not null");
            productTranslation.Name = updateRequest.Name;
            productTranslation.SeoAlias = updateRequest.SeoAlias;
            productTranslation.SeoTitle = updateRequest.SeoTitle;
            productTranslation.SeoDescription = updateRequest.SeoDescription;
            productTranslation.Description = updateRequest.Description;
            productTranslation.Details = updateRequest.Details;
            _unitOfWork.ProductTranslationRepository.Update(productTranslation);
            if (updateRequest.ProductslImage != null)
            {
                var productImgae = await _unitOfWork.ProductImageRepository.UpdateProductImageAsync(updateRequest);
                if (productImgae != null)
                {
                    productImgae.FileSize = updateRequest.ProductslImage.Length;
                    productImgae.ImagePath = await this.SaveFile(updateRequest.ProductslImage);
                    _unitOfWork.ProductImageRepository.Update(productImgae);
                }
            }
            await _unitOfWork.SavesChangeAsync();
            return updateRequest;
        }

        public async Task<bool> UpdateStockAsync(int productId, int addQuantity)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            if (product == null) throw new cellException("not null");
            product.Stock += addQuantity;
            return await _unitOfWork.SavesChangeAsync() > 0;
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var orginalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileNameStar;
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(orginalFileName)}";
            await _imageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
