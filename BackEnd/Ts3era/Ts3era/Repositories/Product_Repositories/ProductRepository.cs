using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ts3era.Controllers;
using Ts3era.Dto.ProductDto;
using Ts3era.Models;
using Ts3era.Models.Data;
using Ts3era.Repositories.SubCategory_Repositories;

namespace Ts3era.Repositories.Product_Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ISubCategoryRepository subCategoryRepository;
        private new List<string> allowextention = new List<string>() { ".jpg", ".png" };
        private long maxsizeimage = 109951163;//2MB

        public ProductRepository
            (
            ApplicationDbContext context,
            IMapper mapper,
            ISubCategoryRepository subCategoryRepository
            )
        {
            this.context = context;
            this.mapper = mapper;
            this.subCategoryRepository = subCategoryRepository;
        }

       

        public async Task<List<ProductDetailsDto>> GetAll()
        {
            var products=await context.Products.Include(c=>c.SubCategory).ToListAsync();
            var map =mapper.Map<List<ProductDetailsDto>>(products); 
            return map;
        }

        public async Task<ProductDetailsDto> GetById(int id)
        {
            var products = await context.Products.Include(c => c.SubCategory)
                .FirstOrDefaultAsync(p=>p.Id==id);
            var map = mapper.Map<ProductDetailsDto>(products);
            return map;
        }

        public async Task<ProductDetailsDto> GetName(string name)
        {
            var products = await context.Products.Include(c => c.SubCategory)
                .FirstOrDefaultAsync(c=>c.Name==name);
            var map = mapper.Map<ProductDetailsDto>(products);
            return map;
        }

        public async Task<List<ProductDetailsDto>> Search(string? name)
        {
            var product =await context.Products
                .Where(c=>c.Name.Trim().ToLower()
                .Contains(name.Trim().ToLower())).ToListAsync();
            var map =mapper.Map<List<ProductDetailsDto>>(product);
            return map;
        }

        public async Task<CreateProductDto> Create(CreateProductDto dto)
        {
            if (!allowextention.Contains(Path.GetExtension(dto.Image.FileName).ToLower()))
                throw new Exception("Must be Allawed .png or .jpg");
            if (dto.Image.Length > maxsizeimage)
                throw new Exception("Max Allawed Image Size 2MB ");
            var isvalid = await  subCategoryRepository.IsValidSubCategory(dto.SubCategory_ID);
            if (!isvalid)
                throw new Exception("Invalid SubCategory Id");

            using var datastream =new MemoryStream();
            await  dto.Image.CopyToAsync(datastream);
            var product = mapper.Map<Product>(dto);
            product.Image=datastream.ToArray();

            await context.AddAsync(product);
            await context.SaveChangesAsync();
            return dto;
            
            
        }

        public async Task<string> Update(int id,UpdateProductDto dto)
        {
            var product = await context.Products.FirstOrDefaultAsync(c => c.Id == id);
            if (product == null)
                throw new Exception("Not found Product ");
            
            if (dto.Image!= null)
            {
                if (!allowextention.Contains(Path.GetExtension(dto.Image.FileName).ToLower()))
                    throw new Exception("Must be Allawed .png or .jpg");
                if (dto.Image.Length > maxsizeimage)
                    throw new Exception("Max Allawed Image Size 2MB ");
                using var datastream =new MemoryStream();
                await dto.Image.CopyToAsync(datastream);
                 product.Image=datastream.ToArray() ;
            }

            
            product.IsAvailable = dto.IsAvailable;
           
            product.Name = dto.ProductName;
            product.Last_Update= dto.Last_Update;
            product.Price_From = dto.Price_From;
            product.Price_TO = dto.Price_To;
            product.SubCategory_Id = dto.SubCategory_ID;
         
            var isvalid = await subCategoryRepository.IsValidSubCategory(dto.SubCategory_ID);
            if (!isvalid)
                throw new Exception("Invalid SubCategory Id");


            context.Products.Update(product);
            context.SaveChanges();
            return "Updated Product";


        }

        public async Task<List<SubCategoriesDto>> GetAllSubCategories()
        {
            var subcategories=await context.SubCategories
                .OrderByDescending(c=>c.Name)
                .ToListAsync();
            var map =mapper.Map<List<SubCategoriesDto>>(subcategories);
            return map;
        }

        public async Task Delete(int id)
        {
            
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null||id!=product.Id)
            {
                throw new Exception($"Not Found Product With ID {id}");
            }
            else
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }

        public async Task<int> GetCountProduct()
        {
            var count = await context.Products.CountAsync();
            return count;
        }
    }
}
